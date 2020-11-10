using System.Collections.Generic;
using System.Linq;
using BasicRestApi.API.Database;
using BasicRestApi.API.Models;
using BasicRestApi.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BasicRestApi.API.Repositories
{
    public class BouquetRepository : IBouquetRepository
    {
        private readonly flowershopContext _context;

        public BouquetRepository(flowershopContext context)
        {
          _context = context;
        }

        public IEnumerable<Bouquet> GetAllBouquets(int shopId)
        {
          var shopWithBouquets = _context.Shops
          .Include(x => x.Bouquets)
          .FirstOrDefault(x => x.Id == shopId);
          if (shopWithBouquets == null)
          {
            throw new NotFoundException();
          }

          return shopWithBouquets.Bouquets;
        }

        public Bouquet GetOneBouquetById(int shopId, int bouquetId)
        {
          CheckShopExists(shopId);

          var bouquet = _context.Bouquets.FirstOrDefault(x => x.ShopId == shopId && x.Id == bouquetId);
          if(bouquet == null)
          {
            throw new NotFoundException();
          }

          return bouquet;
        }

        public void Delete(int shopId, int bouquetId)
        {
            var bouquet = GetOneBouquetById(shopId, bouquetId);
            _context.Bouquets.Remove(bouquet);
            _context.SaveChanges();
        }

        public Bouquet Insert(int shopId, string name, double price, string description)
        {
            CheckShopExists(shopId);
            var bouquet = new Bouquet()
            {
                Name = name,
                ShopId = shopId,
                Price = price,
                Description = description
            };
            _context.Bouquets.Add(bouquet);
            _context.SaveChanges();
            return bouquet;
        }

        public Bouquet Update(int shopId, int bouquetId, string name, double price, string description)
        {
            var bouquet = GetOneBouquetById(shopId, bouquetId);
            bouquet.Name = name;
            bouquet.Price = price;
            bouquet.Description = description;
            _context.SaveChanges();
            return bouquet;
        }

        private void CheckShopExists(int shopId)
        {
            var shopCheck = _context.Shops.Find(shopId);
            if (shopCheck == null)
            {
                throw new NotFoundException();
            }
        }
    }
}