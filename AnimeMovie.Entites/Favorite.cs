using System;
namespace AnimeMovie.Entites
{
    public class Favorite : BaseEntity
    {
        public int UserID { get; set; }
        public int ContentID { get; set; }
        public Type Type { get; set; }
    }
}

