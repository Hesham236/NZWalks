using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalk.API.Models.Domain
{
    public class User
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; } 
        public string lastname { get; set; }
        public string email { get; set; }
        [NotMapped]
        public List<string> roles { get; set; }

        //navigation prop
        public List<User_Role> UserRoles { get; set; }

    }
}
