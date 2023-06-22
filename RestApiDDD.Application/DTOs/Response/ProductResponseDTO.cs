namespace RestApiDDD.Application.DTOs.Response
{
    public class ProductResponseDTO
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvaiable { get; set; }
    }
}
