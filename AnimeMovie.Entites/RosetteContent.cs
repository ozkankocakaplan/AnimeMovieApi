using System;
namespace AnimeMovie.Entites
{
    public class RosetteContent : BaseEntity
    {
        public int ContentID { get; set; }
        public int RosetteID { get; set; }
        public int EpisodesID { get; set; }
        public Type Type { get; set; }
    }
}

