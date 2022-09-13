using System;
namespace AnimeMovie.Entites
{
    public class AnimeRating : BaseEntity
    {
        public int UserID { get; set; }
        public int AnimeID { get; set; }
        public double Rating { get; set; }
    }
}

