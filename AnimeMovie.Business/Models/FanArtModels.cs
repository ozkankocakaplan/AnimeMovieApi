using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Models
{
    public class FanArtModels : FanArt
    {
        public Anime Anime { get; set; }
        public Manga Manga { get; set; }
        public FanArtModels(FanArt fanArt)
        {
            this.ID = fanArt.ID;
            this.Image = fanArt.Image;
            this.UserID = fanArt.UserID;
            this.Description = fanArt.Description;
            this.Type = fanArt.Type;
            this.Users = fanArt.Users;
            this.ContentID = fanArt.ContentID;
            this.CreateTime = fanArt.CreateTime;
        }
    }
}

