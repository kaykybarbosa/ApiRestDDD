using System;

namespace RestApiDDD.Domain.Entities
{
    public class Client : Base
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
    }
}