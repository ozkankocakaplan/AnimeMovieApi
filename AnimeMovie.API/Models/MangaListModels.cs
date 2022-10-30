using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class MangaListModels
    {
        public MangaList MangaList { get; set; }
        public Manga Manga { get; set; }
        public List<MangaEpisodes> MangaEpisodes { get; set; }
        public List<CategoryType> Categories { get; set; }
    }
}

