using System;
namespace AnimeMovie.Entites
{
    public class Ratings : BaseEntity
    {
        public int UserID { get; set; }
        public int AnimeID { get; set; }
        public double Rating { get; set; }
        public Type Type { get; set; }
    }
}

