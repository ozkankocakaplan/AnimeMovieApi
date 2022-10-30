using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class FanArtModel : FanArt
    {
        public Anime Anime { get; set; }
        public Manga Manga { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comments> Comments { get; set; }
        public FanArtModel(FanArt fanArt)
        {
            this.ID = fanArt.ID;
            this.UserID = fanArt.UserID;
            this.ContentID = fanArt.ContentID;
            this.Image = fanArt.Image;
            this.Description = fanArt.Description;
            this.Type = fanArt.Type;
            if (fanArt.Users != null)
            {
                this.Users = fanArt.Users;
            }
            this.CreateTime = fanArt.CreateTime;
        }
    }
}

