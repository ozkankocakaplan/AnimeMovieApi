using System;
namespace AnimeMovie.Entites
{
    public class AnimeOfTheWeek : BaseEntity
    {
        public int AnimeID { get; set; }
        public int UserID { get; set; } //Manager
        public string Description { get; set; }

    }
}

