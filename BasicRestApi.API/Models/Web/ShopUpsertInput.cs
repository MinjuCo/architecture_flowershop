using System.ComponentModel.DataAnnotations;

namespace BasicRestApi.API.Models.Web
{
    public class ShopUpsertInput
    {
        //Model Validation
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }

        [Required]
        [StringLength(500)]
        public string Region { get; set; }
    }
}