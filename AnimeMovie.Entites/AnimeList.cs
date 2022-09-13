using System;
namespace AnimeMovie.Entites
{
    public class AnimeList : BaseEntity
    {
        public int UserID { get; set; }
        public int AnimeID { get; set; }
        public AnimeStatus AnimeStatus { get; set; }
    }
}

