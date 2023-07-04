using System.ComponentModel.DataAnnotations;

namespace RestApiDDD.Domain.Entities
{
    public class Base
    {
     
        [Key]
        public Guid Id { get; set; }
        public Base()
        {
            Id = Guid.NewGuid();
        }
    }
}