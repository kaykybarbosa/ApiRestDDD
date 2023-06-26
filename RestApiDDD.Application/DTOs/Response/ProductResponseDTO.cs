using System.Text.Json.Serialization;

namespace RestApiDDD.Application.DTOs.Response
{
    public class ProductResponseDTO : BaseResponseDTO
    {
        public Guid Id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvaiable { get; set; }
    }
}
