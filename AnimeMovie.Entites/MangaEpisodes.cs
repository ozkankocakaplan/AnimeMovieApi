using System;
namespace AnimeMovie.Entites
{
    public class MangaEpisodes : BaseEntity
    {
        public int MangaID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

