using System.Text.Json.Serialization;

namespace RestApiDDD.Application.DTOs.Response
{
    public class ClientResponseDTO : BaseResponseDTO
    {
        public Guid Id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirstName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastName { get; set;}
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
    }
}