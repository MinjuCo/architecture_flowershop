using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flowershop.API.Database;
using Flowershop.API.Models;
using Flowershop.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Flowershop.API.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly FlowershopContext _context;

        public ShopRepository(FlowershopContext context)
        {
          _context = context;
        }

        public async Task<IEnumerable<Shop>> getAllShops()
        {
          return await _context.Shops.ToListAsync();
        }

        public async Task<Shop> getOneShopById(int id)
        {
          return await _context.Shops.FindAsync(id);
        }

        public async Task delete(int id)
        {
          var shop = await _context.Shops.FindAsync(id);
          if(shop == null)
          {
            throw new NotFoundException();
          }

          _context.Shops.Remove(shop);
          _context.SaveChangesAsync();
        }

        public async Task<Shop> insert(string name, string streetName, string streetNumber, string region)
        {
          var shop = new Shop
          {
            Name = name,
            StreetName = streetName,
            StreetNumber = streetNumber,
            Region = region
          };
          _context.Shops.AddAsync(shop);
          _context.SaveChangesAsync();
          return shop;
        }

        public async Task <Shop> update(int id, string name, string streetName, string streetNumber, string region)
        {
          var shop = await _context.Shops.FindAsync(id);
          if(shop == null)
          {
            throw new NotFoundException();
          }

          shop.Name = name;
          shop.StreetName = streetName;
          shop.StreetNumber = streetNumber;
          shop.Region = region;
          
          _context.SaveChangesAsync();
          return shop;
        }
    }
}