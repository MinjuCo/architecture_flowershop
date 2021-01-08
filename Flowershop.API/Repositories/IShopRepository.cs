using System.Collections.Generic;
using System.Threading.Tasks;
using Flowershop.API.Models.Domain;

namespace Flowershop.API.Repositories
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> getAllShops();
        Task<Shop> getOneShopById(int id);
        Task delete(int id);
        Task<Shop> insert(string name, string streetName, string streetNumber, string region);
        Task<Shop> update(int id, string name, string streetName, string streetNumber, string region);
    }
}