namespace NzWalks.API.Models.DTO
{
    public class UserRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
