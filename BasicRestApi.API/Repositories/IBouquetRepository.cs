using System.Collections.Generic;
using System.Threading.Tasks;
using BasicRestApi.API.Models.Domain;

namespace BasicRestApi.API.Repositories
{
    public interface IBouquetRepository
    {
        Task<IEnumerable<Bouquet>> GetAllBouquets(int shopId);
        Task<Bouquet> GetOneBouquetById(int shopId, int bouquetId);
        Task Delete(int shopId, int bouquetId);
        Task<Bouquet> Insert(int shopId, string name, double price, string description);
        Task<Bouquet> Update(int shopId, int bouquetId, string name, double price, string description);
    }
}