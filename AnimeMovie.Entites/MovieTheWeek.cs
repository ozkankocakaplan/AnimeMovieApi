using System;
namespace AnimeMovie.Entites
{
    public class MovieTheWeek : BaseEntity
    {
        public int ContentID { get; set; }
        public int UserID { get; set; } //Manager
        public string Description { get; set; }
        public Type Type { get; set; }

    }
}

