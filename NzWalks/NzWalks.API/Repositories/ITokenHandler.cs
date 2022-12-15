using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
