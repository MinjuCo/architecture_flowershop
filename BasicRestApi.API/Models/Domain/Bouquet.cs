using System.ComponentModel.DataAnnotations;

namespace BasicRestApi.API.Models.Domain
{
    public class Bouquet : BaseDatabaseClass
    {
        [Required]
        public Shop Shop { get; set; }

        public int ShopId { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Description { get; set; }
    }
}