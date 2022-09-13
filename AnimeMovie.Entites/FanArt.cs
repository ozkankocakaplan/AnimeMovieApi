using System;
namespace AnimeMovie.Entites
{
    public class FanArt : BaseEntity
    {
        public int UserID { get; set; }
        public int AnimeID { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}

