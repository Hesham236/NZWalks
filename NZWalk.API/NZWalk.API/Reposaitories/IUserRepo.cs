using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public interface IUserRepo
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
