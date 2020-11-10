using System.ComponentModel.DataAnnotations;

namespace BasicRestApi.API.Models.Domain
{
    public abstract class BaseDatabaseClass
    {
        [Key]
        public int Id { get; set; }
    }
}