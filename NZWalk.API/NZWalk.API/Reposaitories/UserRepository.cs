using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public class UserRepository : IUserRepo
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public UserRepository(NZWalksDBContext nZWalksDBContext) 
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await nZWalksDBContext.Users.
                FirstOrDefaultAsync(x=>x.username.ToLower()==username.ToLower() && x.password==password);

            if (user == null) return null;
            var userRoles = await nZWalksDBContext.Users_Roles.Where(x=>x.UserId==user.id).ToListAsync();
            if(userRoles.Any() ) 
            {
                user.roles = new List<string>();
                foreach(var userRole in userRoles) 
                {
                    var role =await nZWalksDBContext.Roles.FirstOrDefaultAsync(x=>x.Id==userRole.id);
                    if (role != null)
                    {
                        user.roles.Add(role.Name);
                    }
                }
            }
            user.password = null;
            return user;
        }
    }
}
