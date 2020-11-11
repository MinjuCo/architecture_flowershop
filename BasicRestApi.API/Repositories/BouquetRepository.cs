using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Bouquet>> GetAllBouquets(int shopId)
        {
          var shopWithBouquets = await _context.Shops
          .Include(x => x.Bouquets)
          .FirstOrDefaultAsync(x => x.Id == shopId);
          if (shopWithBouquets == null)
          {
            throw new NotFoundException();
          }

          return shopWithBouquets.Bouquets;
        }

        public async Task<Bouquet> GetOneBouquetById(int shopId, int bouquetId)
        {
          await CheckShopExists(shopId);

          var bouquet = await _context.Bouquets.FirstOrDefaultAsync(x => x.ShopId == shopId && x.Id == bouquetId);
          if(bouquet == null)
          {
            throw new NotFoundException();
          }

          return bouquet;
        }

        public async Task Delete(int shopId, int bouquetId)
        {
            var bouquet = await GetOneBouquetById(shopId, bouquetId);
            _context.Bouquets.Remove(bouquet);
            _context.SaveChangesAsync();
        }

        public async Task<Bouquet> Insert(int shopId, string name, double price, string description)
        {
            await CheckShopExists(shopId);
            var bouquet = new Bouquet()
            {
                Name = name,
                ShopId = shopId,
                Price = price,
                Description = description
            };
            _context.Bouquets.AddAsync(bouquet);
            _context.SaveChangesAsync();
            return bouquet;
        }

        public async Task<Bouquet> Update(int shopId, int bouquetId, string name, double price, string description)
        {
            var bouquet = await GetOneBouquetById(shopId, bouquetId);
            bouquet.Name = name;
            bouquet.Price = price;
            bouquet.Description = description;
            _context.SaveChangesAsync();
            return bouquet;
        }

        private async Task CheckShopExists(int shopId)
        {
            var shopCheck = await _context.Shops.FindAsync(shopId);
            if (shopCheck == null)
            {
                throw new NotFoundException();
            }
        }
    }
}