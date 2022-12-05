using NZWalk.API.Models.Domain;

namespace NZWalk.API.Reposaitories
{
    public class UserRepo : IUserRepo
    {
        private List<User> users = new List<User>()
        {
            //new User()
            //{
            //    firstname = "Read Only",lastname="User",email="readonly@gmail.com",
            //    id = Guid.NewGuid(),username="ReadOnly",password="readonly",
            //    roles=new List<string>{"reader"}
            //},
            //new User() 
            //{
            //    firstname = "Read Write",lastname="User",email="readWrite@gmail.com",
            //    id = Guid.NewGuid(),username="Readwrite",password="readWrite",
            //    roles=new List<string>{"reader","writer"}
            //}
        };
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = users.Find(x=>x.username.Equals(username,StringComparison.InvariantCultureIgnoreCase)&&
            x.password==password);

            if (user != null) return user;
            return user;
        }
    }
}
