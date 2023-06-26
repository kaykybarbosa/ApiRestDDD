using System.ComponentModel.DataAnnotations;

namespace RestApiDDD.Domain.Entities
{
    public class Client : Base
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 12)]
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
    }
}