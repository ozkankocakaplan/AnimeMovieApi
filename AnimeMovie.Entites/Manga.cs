using System;
namespace AnimeMovie.Entites
{
    public class Manga : BaseEntity
    {
        public string? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Arrangement { get; set; }
        public int Views { get; set; }
        public string AgeLimit { get; set; }
        public Status Status { get; set; }
        public string SeoUrl { get; set; }
    }
}

