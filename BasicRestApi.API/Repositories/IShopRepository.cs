using System.Collections.Generic;
using BasicRestApi.API.Models.Domain;

namespace BasicRestApi.API.Repositories
{
    public interface IShopRepository
    {
        IEnumerable<Shop> GetAllShops();
        Shop GetOneShopById(int id);
        void Delete(int id);
        Shop Insert(string name, string address, string region);
        Shop Update(int id, string name, string address, string region);
    }
}