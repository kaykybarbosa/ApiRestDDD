using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiDDD.Domain.Entities
{
    public class Product : Base
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        public bool IsAvaiable { get; set; }
    }
}
