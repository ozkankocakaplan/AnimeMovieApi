using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class ComplaintContentModels : ContentComplaint
    {
        public Users User { get; set; }
        public Anime Anime { get; set; }
        public Manga Manga { get; set; }
        public AnimeEpisodes AnimeEpisode { get; set; }
        public MangaEpisodes MangaEpisode { get; set; }
        public ComplaintContentModels(ContentComplaint content)
        {
            this.ComplaintType = content.ComplaintType;
            this.ContentID = content.ContentID;
            this.CreateTime = content.CreateTime;
            this.ID = content.ID;
            this.Message = content.Message;
            this.type = content.type;
            this.UserID = content.UserID;
        }
    }
}

