using System;
namespace AnimeMovie.Entites
{
    public class Anime : BaseEntity
    {
        public string? Img { get; set; }
        public string? AnimeName { get; set; }
        public string AnimeDescription { get; set; }
        public string MalRating { get; set; }
        public string AgeLimit { get; set; }
        public int SeasonCount { get; set; }
        public string ShowTime { get; set; }
        public Status Status { get; set; }
        public VideoType VideoType { get; set; }
        public int Views { get; set; }
        public int Like { get; set; }
        public string Arrangement { get; set; }
        public string SeoUrl { get; set; }


    }
}

