using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Models
{
    public class UserModel : Users
    {
        public string Token { get; set; }
        public UserModel(Users users)
        {
            this.ID = users.ID;
            this.Image = users.Image;
            this.isBanned = users.isBanned;
            this.NameSurname = users.NameSurname;
            this.RoleType = users.RoleType;
            this.SeoUrl = users.SeoUrl;
            this.UserName = users.UserName;
            this.BirthDay = users.BirthDay;
            this.CreateTime = users.CreateTime;
            this.Email = users.Email;
            this.Gender = users.Gender;
        }
    }
}

