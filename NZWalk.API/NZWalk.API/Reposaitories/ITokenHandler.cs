using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
