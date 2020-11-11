using System.Collections.Generic;
using System.Threading.Tasks;
using BasicRestApi.API.Models.Domain;

namespace BasicRestApi.API.Repositories
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllShops();
        Task<Shop> GetOneShopById(int id);
        Task Delete(int id);
        Task<Shop> Insert(string name, string address, string region);
        Task<Shop> Update(int id, string name, string address, string region);
    }
}