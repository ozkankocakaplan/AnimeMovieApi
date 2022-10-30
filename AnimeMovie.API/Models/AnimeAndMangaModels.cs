using System;
using AnimeMovie.Entites;
using Type = AnimeMovie.Entites.Type;

namespace AnimeMovie.API.Models
{
    public class AnimeAndMangaModels : BaseEntity
    {
        public string name { get; set; }
        public string img { get; set; }
        public string url { get; set; }
        public int reviewsCount { get; set; }
        public int fanArtCount { get; set; }
        public int like { get; set; }
        public int arrangement { get; set; }
        public Type Type { get; set; }
        public VideoType VideoType { get; set; }
        public List<CategoryType> Categories { get; set; }
        public List<AnimeSeason> AnimeSeasons { get; set; }
        public List<AnimeEpisodes> AnimeEpisodes { get; set; }
        public List<MangaEpisodes> MangaEpisodes { get; set; }
    }
}

