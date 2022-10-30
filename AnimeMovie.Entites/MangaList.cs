using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class MangaList : BaseEntity
    {
        public int UserID { get; set; }
        [ForeignKey("Manga")]
        public int MangaID { get; set; }
        [ForeignKey("MangaEpisode")]
        public int EpisodeID { get; set; }
        public MangaStatus Status { get; set; }

        public virtual Manga Manga { get; set; }
        public virtual MangaEpisodes MangaEpisode { get; set; }
    }
}

