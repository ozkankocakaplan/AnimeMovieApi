using System;
namespace AnimeMovie.Entites
{
    public class AnimeEpisodes : BaseEntity
    {
        public int SeasonID { get; set; }
        public string EpisodeName { get; set; }
        public string EpisodeDescription { get; set; }
    }
}

