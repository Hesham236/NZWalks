namespace NZWalk.API.Models.Domain
{
    public class User_Role
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }
        public User user { get; set; }
        public Guid RoleId { get; set; }
        public Role role { get; set; }

    }
}
