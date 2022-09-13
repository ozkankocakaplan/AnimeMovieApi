using System;
namespace AnimeMovie.Entites
{
    public class Episodes : BaseEntity
    {
        public int EpisodeID { get; set; }
        public string AlternativeName { get; set; }
        public string AlternativeVideoUrl { get; set; }
        public string AlternativeVideoDownloadUrl { get; set; }
    }
}

