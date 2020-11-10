using System.Collections.Generic;
using BasicRestApi.API.Models.Domain;

namespace BasicRestApi.API.Repositories
{
    public interface IBouquetRepository
    {
        IEnumerable<Bouquet> GetAllBouquets(int shopId);
        Bouquet GetOneBouquetById(int shopId, int bouquetId);
        void Delete(int shopId, int bouquetId);
        Bouquet Insert(int shopId, string name, double price, string description);
        Bouquet Update(int shopId, int bouquetId, string name, double price, string description);
    }
}