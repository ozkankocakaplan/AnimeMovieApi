using System;
namespace AnimeMovie.Entites
{
    public class Comments : BaseEntity
    {
        public int UserID { get; set; }
        public int ContentID { get; set; }
        public string Comment { get; set; }
        public bool isSpoiler { get; set; } = false;
        public Type Type { get; set; }
    }
}

