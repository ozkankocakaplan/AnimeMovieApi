using System;
namespace AnimeMovie.Entites
{
    public class MangaList : BaseEntity
    {
        public int UserID { get; set; }
        public int EpisodeID { get; set; }
        public MangaStatus Status { get; set; }
    }
}

