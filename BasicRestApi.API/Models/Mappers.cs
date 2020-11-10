using BasicRestApi.API.Models.Domain;
using BasicRestApi.API.Models.Web;

namespace BasicRestApi.API.Models
{
    public static class Mappers
    {
      public static ShopWebOutput Convert(this Shop input)
      {
        return new ShopWebOutput(input.Id, input.Name, input.Address, input.Region);
      }

      public static BouquetWebOutput Convert(this Bouquet input)
      {
        return new BouquetWebOutput(input.Id, input.Name, input.Price, input.Description);
      }
    }
}