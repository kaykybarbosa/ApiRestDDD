using System.ComponentModel.DataAnnotations;

namespace RestApiDDD.Application.DTOs.Request
{
    public class ClientRequestDTO
    {
        [Required(ErrorMessage = "Post FirstName is required.")]
        [StringLength(50, MinimumLength = 5)]
        public string? FirstName { get; set; }
        
        [Required(ErrorMessage = "Post LastName is required.")]
        [StringLength(50, MinimumLength = 5)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Post Email is required.")]
        [StringLength(30, MinimumLength = 12)]
        public string? Email { get; set; }
    }
}