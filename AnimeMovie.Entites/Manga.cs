using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class Manga : BaseEntity
    {
        public int AnimeID { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public string AgeLimit { get; set; }
        public Status Status { get; set; }
        public string SeoUrl { get; set; }
        public string MalRating { get; set; }
        public string? fansub { get; set; }
    }
}

