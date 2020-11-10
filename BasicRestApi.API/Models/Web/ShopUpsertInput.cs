using System.ComponentModel.DataAnnotations;

namespace BasicRestApi.API.Models.Web
{
    public class ShopUpsertInput
    {
        //Model Validation
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }
    }
}