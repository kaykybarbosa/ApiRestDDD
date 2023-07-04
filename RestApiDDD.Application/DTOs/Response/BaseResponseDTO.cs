using System.Text.Json.Serialization;

namespace RestApiDDD.Application.DTOs.Response
{
    public class BaseResponseDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Success { get; set; } = true;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        public BaseResponseDTO() { }
        public BaseResponseDTO(string? message, bool success)
        {
            Message = message; 
            Success = success; 
        }
        public BaseResponseDTO(string? message, bool success, string? error)
        {
            Message = message; 
            Success = success; 
            Error = error;
        }

        public void Update (string? message, bool success, string? error)
        {
            Message = message;
            Success = success;
            Error = error;
        }
    }
}