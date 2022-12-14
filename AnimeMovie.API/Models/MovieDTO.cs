using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
	public class MovieDTO
	{
		public List<AnimeModels> RatingAnimes { get; set; }
		public List<Anime> NewEpisodeAnimes { get; set; }
		public List<MangaModels> RatingMangas { get; set; }
		public List<Manga> NewEpisodeManga { get; set; }
	}
}

