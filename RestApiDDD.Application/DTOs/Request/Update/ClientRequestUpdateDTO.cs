using System.ComponentModel.DataAnnotations;

namespace RestApiDDD.Application.DTOs.Request.Update
{
    public class ClientRequestUpdateDTO
    {
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Post FirstName is required.")]
        public string? FirstName { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Post LastName is required.")]
        public string? LastName { get; set; }

        [EmailAddress]
        [StringLength(30, MinimumLength = 12)]
        [Required(ErrorMessage = "Post Email is required.")]
        public string? Email { get; set; }

        public bool IsActive { get; set; }   
    }
}
