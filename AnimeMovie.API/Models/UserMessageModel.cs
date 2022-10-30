using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class UserMessageModel : Users
    {
        public List<UserMessage> userMessages  { get; set; }
        public UserMessageModel(Users user)
        {
            this.ID = user.ID;
            this.Image = user.Image;
            this.NameSurname = user.NameSurname;
            this.UserName = user.UserName;
            this.SeoUrl = user.SeoUrl;
            this.Email = user.Email;
            this.CreateTime = user.CreateTime;
        }
    }
}

