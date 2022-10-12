using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class AnimeList : BaseEntity
    {
        public int UserID { get; set; }
        public int AnimeID { get; set; }
        public int EpisodeID { get; set; }
        public AnimeStatus AnimeStatus { get; set; }

    }
}

