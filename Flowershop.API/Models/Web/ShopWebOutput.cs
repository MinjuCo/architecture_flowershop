namespace Flowershop.API.Models.Web
{
    public class ShopWebOutput
    {
        public ShopWebOutput(int id, string name, string streetName, string streetNumber, string region)
        {
          Id = id;
          Name = name;
          StreetName = streetName;
          StreetNumber = streetNumber;
          Region = region;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber {get; set; }
        public string Region { get; set; }
    }
}