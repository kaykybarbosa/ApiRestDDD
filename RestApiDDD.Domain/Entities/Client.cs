using System.ComponentModel.DataAnnotations;

namespace RestApiDDD.Domain.Entities
{
    public class Client : Base
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
    }
}