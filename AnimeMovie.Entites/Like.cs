using System;
namespace AnimeMovie.Entites
{
    public class Like : BaseEntity
    {
        public Type Type { get; set; }
        public int ContentID { get; set; }
        public int UserID { get; set; }
        public int EpisodeID { get; set; }
    }
}

