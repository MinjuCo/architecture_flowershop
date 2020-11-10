using System.ComponentModel.DataAnnotations;
using BasicRestApi.API.Models.Domain;

namespace BasicRestApi.API.Models.Web
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