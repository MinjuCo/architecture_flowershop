using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicRestApi.API.Database;
using BasicRestApi.API.Models;
using BasicRestApi.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BasicRestApi.API.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly flowershopContext _context;

        public ShopRepository(flowershopContext context)
        {
          _context = context;
        }

        public async Task<IEnumerable<Shop>> GetAllShops()
        {
          return await _context.Shops.ToListAsync();
        }

        public async Task<Shop> GetOneShopById(int id)
        {
          return await _context.Shops.FindAsync(id);
        }

        public async Task Delete(int id)
        {
          var shop = await _context.Shops.FindAsync(id);
          if(shop == null)
          {
            throw new NotFoundException();
          }

          _context.Shops.Remove(shop);
          _context.SaveChangesAsync();
        }

        public async Task<Shop> Insert(string name, string address, string region)
        {
          var shop = new Shop
          {
            Name = name,
            Address = address,
            Region = region
          };
          _context.Shops.AddAsync(shop);
          _context.SaveChangesAsync();
          return shop;
        }

        public async Task <Shop> Update(int id, string name, string address, string region)
        {
          var shop = await _context.Shops.FindAsync(id);
          if(shop == null)
          {
            throw new NotFoundException();
          }

          shop.Name = name;
          shop.Address = address;
          shop.Region = region;
          
          _context.SaveChangesAsync();
          return shop;
        }
    }
}