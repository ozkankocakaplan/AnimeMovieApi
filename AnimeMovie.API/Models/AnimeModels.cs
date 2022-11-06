using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class AnimeModels
    {
        public Anime Anime { get; set; }
        public Manga Manga { get; set; }
        public Like Like { get; set; }
        public Ratings AnimeRating { get; set; }
        public ContentNotification ContentNotification { get; set; }
        public List<CategoryType> Categories { get; set; }
        public List<AnimeSeason> AnimeSeasons { get; set; }
        public List<AnimeSeasonMusic> AnimeSeasonMusics { get; set; }
        public List<AnimeEpisodes> AnimeEpisodes { get; set; }
        public List<Episodes> Episodes { get; set; }
        public List<AnimeList> animeLists { get; set; }
        public List<AnimeImages> AnimeImages { get; set; }
        public double Rating { get; set; }
        public int Arrangement { get; set; }
        public int LikeCount { get; set; }
        public int ViewsCount { get; set; }
        public int AnimeEpisodeCount { get; set; }

    }
}

