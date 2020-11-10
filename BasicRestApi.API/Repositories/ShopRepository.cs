using System.Collections.Generic;
using System.Linq;
using BasicRestApi.API.Database;
using BasicRestApi.API.Models;
using BasicRestApi.API.Models.Domain;

namespace BasicRestApi.API.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly flowershopContext _context;

        public ShopRepository(flowershopContext context)
        {
          _context = context;
        }

        public IEnumerable<Shop> GetAllShops()
        {
          return _context.Shops.ToList();
        }

        public Shop GetOneShopById(int id)
        {
          return _context.Shops.Find(id);
        }

        public void Delete(int id)
        {
          var shop = _context.Shops.Find(id);
          if(shop == null)
          {
            throw new NotFoundException();
          }

          _context.Shops.Remove(shop);
          _context.SaveChanges();
        }

        public Shop Insert(string name, string address, string region)
        {
          var shop = new Shop
          {
            Name = name,
            Address = address,
            Region = region
          };
          _context.Shops.Add(shop);
          _context.SaveChanges();
          return shop;
        }

        public Shop Update(int id, string name, string address, string region)
        {
          var shop = _context.Shops.Find(id);
          if(shop == null)
          {
            throw new NotFoundException();
          }

          shop.Name = name;
          shop.Address = address;
          shop.Region = region;
          
          _context.SaveChanges();
          return shop;
        }
    }
}