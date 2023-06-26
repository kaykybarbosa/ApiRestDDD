using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestApiDDD.Application.DTOs.Request.Update
{
    public class ProductRequestUpdateDTO
    {
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Post Name is required.")]
        public string? Name { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        [Required(ErrorMessage = "Post Price is required.")]
        public decimal Price { get; set; }
        public bool IsAvaiable { get; set; }
    }
}
