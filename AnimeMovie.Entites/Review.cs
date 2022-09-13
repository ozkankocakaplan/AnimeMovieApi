using System;
namespace AnimeMovie.Entites
{
    public class Review : BaseEntity
    {
        public int AnimeID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
    }
}

