using System.ComponentModel.DataAnnotations;

namespace Flowershop.API.Models.Domain
{
    public abstract class BaseDatabaseClass
    {
        [Key]
        public int Id { get; set; }
    }
}