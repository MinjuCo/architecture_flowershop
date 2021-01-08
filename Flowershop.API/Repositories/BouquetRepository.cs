using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flowershop.API.Database;
using Flowershop.API.Models;
using Flowershop.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Flowershop.API.Repositories
{
    public class BouquetRepository : IBouquetRepository
    {
        private readonly FlowershopContext _context;

        public BouquetRepository(FlowershopContext context)
        {
          _context = context;
        }

        public async Task<IEnumerable<Bouquet>> getAllBouquets(int shopId)
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

        public async Task<Bouquet> getOneBouquetById(int shopId, int bouquetId)
        {
          await checkShopExists(shopId);

          var bouquet = await _context.Bouquets.FirstOrDefaultAsync(x => x.ShopId == shopId && x.Id == bouquetId);
          if(bouquet == null)
          {
            throw new NotFoundException();
          }

          return bouquet;
        }

        public async Task delete(int shopId, int bouquetId)
        {
            var bouquet = await getOneBouquetById(shopId, bouquetId);
            _context.Bouquets.Remove(bouquet);
            _context.SaveChangesAsync();
        }

        public async Task<Bouquet> insert(int shopId, string name, double price, string description)
        {
            await checkShopExists(shopId);
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

        public async Task<Bouquet> update(int shopId, int bouquetId, string name, double price, string description)
        {
            var bouquet = await getOneBouquetById(shopId, bouquetId);
            bouquet.Name = name;
            bouquet.Price = price;
            bouquet.Description = description;
            _context.SaveChangesAsync();
            return bouquet;
        }

        private async Task checkShopExists(int shopId)
        {
            var shopCheck = await _context.Shops.FindAsync(shopId);
            if (shopCheck == null)
            {
                throw new NotFoundException();
            }
        }
    }
}