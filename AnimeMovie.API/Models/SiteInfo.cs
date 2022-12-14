using System;
namespace AnimeMovie.API.Models
{
	public class SiteInfo
	{
		public int OnlinePeopleCount { get; set; }
		public int PeopleCount { get; set; }
		public int MangaCount { get; set; }
		public int AnimeCount { get; set; }
		public int AnimeFanArtCount { get; set; }
		public int AnimeReviewCount { get; set; }
        public int MangaFanArtCount { get; set; }
        public int MangaReviewCount { get; set; }
        public int AnimeEpisodeCount { get; set; }
		public int MangaEpisodeCount { get; set; }
	}
}

