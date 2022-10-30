using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class AnimeListModels
    {
        public AnimeList AnimeList { get; set; }
        public Anime Anime { get; set; }
        public List<AnimeEpisodes> AnimeEpisodes { get; set; }
        public List<CategoryType> Categories { get; set; }
        public List<AnimeSeason> AnimeSeasons { get; set; }
    }
}

