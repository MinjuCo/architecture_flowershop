using System.ComponentModel.DataAnnotations;
using Flowershop.API.Models.Domain;

namespace Flowershop.API.Models.Web
{
    public class BouquetUpsertInput
    {
      [Required]
      [StringLength(1000)]
      public string Name { get; set; }

      [Required]
      public double Price { get; set; }

      public string Description { get; set; }
    }
}