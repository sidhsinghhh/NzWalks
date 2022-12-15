using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>()
        {
            new User()
            {
                FirstName= "f1",LastName= "l1", Password="234", Username="user1", Roles = new List<string>{"reader"}
            },
            new User()
            {
                FirstName= "f2",LastName= "l2", Password="567", Username="user2", Roles = new List<string>{"reader", "writer"}
            }
        };

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password == password);
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}
