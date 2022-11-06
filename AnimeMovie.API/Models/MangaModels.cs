using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
	public class MangaModels
	{
        public Anime Anime { get; set; }
        public Manga Manga { get; set; }
        public Like Like { get; set; }
        public Ratings MangaRating { get; set; }
        public ContentNotification ContentNotification { get; set; }
        public List<CategoryType> Categories { get; set; }
        public List<MangaEpisodes> MangaEpisodes { get; set; }
        public List<MangaEpisodeContent> MangaEpisodeContents { get; set; }
        public List<MangaImages> MangaImages { get; set; }
        public List<MangaList> MangaLists { get; set; }
        public double Rating { get; set; }
        public int Arrangement { get; set; }
        public int LikeCount { get; set; }
        public int ViewsCount { get; set; }
        public int MangaEpisodeCount { get; set; }
    }
}

