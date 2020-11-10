using BasicRestApi.API.Models.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace BasicRestApi.API.Database
{
    public partial class flowershopContext : DbContext
    {
        public flowershopContext(DbContextOptions<flowershopContext> ctx)
            : base(ctx)
        {
        }

        //A DbSet can be used to add/query items. It maps to a table
        public DbSet<Shop> Shops { get; set; }
        public Dbset<Car> Cars { get; set; }
    }
}
