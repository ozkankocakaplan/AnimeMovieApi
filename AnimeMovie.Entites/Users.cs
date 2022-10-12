using System;
namespace AnimeMovie.Entites
{
    public class Users : BaseEntity
    {
        string _password = "";
        public string? Image { get; set; }
        public string? NameSurname { get; set; }
        public string? UserName { get; set; }
        public string? BirthDay { get; set; }
        public string? Email { get; set; }
        public string? Password
        {
            get { return null; }
            set { _password = value; }
        }
        public string? Discover { get; set; }
        public string? Gender { get; set; }
        public bool isBanned { get; set; } = false;
        public string SeoUrl { get; set; }
        public RoleType RoleType { get; set; } = RoleType.User;
    }
}

