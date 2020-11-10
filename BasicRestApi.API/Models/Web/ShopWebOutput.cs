namespace BasicRestApi.API.Models.Web
{
    class ShopWebOutput
    {
        public ShopWebOutput(int id, string name)
        {
          Id = id;
          Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}