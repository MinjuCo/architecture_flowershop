using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flowershop.API.Models.Domain
{
    public class Shop : BaseDatabaseClass
    {
        [Required, MaxLength(2048)]
        public string Name { get; set; }
        
        [Required, MaxLength(2048)]
        public string StreetName { get; set; }

        [Required, MaxLength(2048)]
        public string StreetNumber { get; set; }

        [Required, MaxLength(2048)]
        public string Region { get; set; }
        
        public IEnumerable<Bouquet> Bouquets { get; set; }
        
    }
}