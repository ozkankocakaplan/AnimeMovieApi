using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class AnimeList : BaseEntity
    {
        public int UserID { get; set; }
        [ForeignKey("Anime")]
        public int AnimeID { get; set; }
        [ForeignKey("AnimeEpisode")]
        public int EpisodeID { get; set; }
        public AnimeStatus AnimeStatus { get; set; }

        public virtual Anime Anime { get; set; }
        public virtual AnimeEpisodes AnimeEpisode { get; set; }

    }
}

