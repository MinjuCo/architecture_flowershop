using BasicRestApi.API.Models.Domain;

namespace BasicRestApi.API.Models.Web
{
    public class BouquetWebOutput
    {
      public BouquetWebOutput(int id, string name, double price, string description)
      {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
      }

      public int Id { get; set; }
      public string Name { get; set; }
      public double Price { get; set; }
      public string Description { get; set; }
    }
}