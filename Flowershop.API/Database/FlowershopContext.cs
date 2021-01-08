using Flowershop.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Flowershop.API.Database
{
    public partial class FlowershopContext : DbContext
    {
        public FlowershopContext(DbContextOptions<FlowershopContext> ctx)
            : base(ctx)
        {
        }

        //A DbSet can be used to add/query items. It maps to a table
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Bouquet> Bouquets { get; set; }
    }
}
