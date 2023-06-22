using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiDDD.Domain.Entities
{
    public class Base
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
