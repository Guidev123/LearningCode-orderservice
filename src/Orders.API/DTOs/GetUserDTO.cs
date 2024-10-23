namespace Orders.API.DTOs
{
    public class GetUserDTO(Guid id, string fullName, string phone, string email, string role, string token)
    {
        public Guid Id { get; private set; } = id;
        public string FullName { get; private set; } = fullName;
        public string Phone { get; private set; } = phone;
        public string Email { get; private set; } = email;
        public string Role { get; private set; } = role;
        public string Token { get; private set; } = token;
    }
}
