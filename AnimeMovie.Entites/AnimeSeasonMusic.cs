using System;
namespace AnimeMovie.Entites
{
    public class AnimeSeasonMusic : BaseEntity
    {
        public int SeasonID { get; set; }
        public string MusicName { get; set; }
        public string MusicUrl { get; set; }
    }
}

