using System.Collections.Generic;
using System.Threading.Tasks;
using Flowershop.API.Models.Domain;

namespace Flowershop.API.Repositories
{
    public interface IBouquetRepository
    {
        Task<IEnumerable<Bouquet>> getAllBouquets(int shopId);
        Task<Bouquet> getOneBouquetById(int shopId, int bouquetId);
        Task delete(int shopId, int bouquetId);
        Task<Bouquet> insert(int shopId, string name, double price, string description);
        Task<Bouquet> update(int shopId, int bouquetId, string name, double price, string description);
    }
}