using System;
namespace AnimeMovie.Entites
{
    public class Rosette : BaseEntity
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public int ContentID { get; set; }// Remove v1.2
        public Type Type { get; set; }
    }
}

