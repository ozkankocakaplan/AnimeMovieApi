using System;
namespace AnimeMovie.Entites
{
    public class Rosette : BaseEntity
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public int ContentID { get; set; }
        public Type Type { get; set; }
    }
}

