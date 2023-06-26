using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiDDD.Application.DTOs.Request
{
    public class ProductRequestDTO
    {
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Post Name is required.")]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        [Required(ErrorMessage = "Post Price is required.")]
        public decimal Price { get; set; }
    }
}
