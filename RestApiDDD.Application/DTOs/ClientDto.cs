namespace RestApiDDD.Application.DTOs
{
    public class ClientDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}