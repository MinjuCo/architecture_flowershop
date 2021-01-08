using Flowershop.API.Models.Domain;
using Flowershop.API.Models.Web;

namespace Flowershop.API.Models
{
    public static class Mappers
    {
      public static ShopWebOutput Convert(this Shop input)
      {
        return new ShopWebOutput(input.Id, input.Name, input.StreetName, input.StreetNumber, input.Region);
      }

      public static BouquetWebOutput Convert(this Bouquet input)
      {
        return new BouquetWebOutput(input.Id, input.Name, input.Price, input.Description);
      }
    }
}