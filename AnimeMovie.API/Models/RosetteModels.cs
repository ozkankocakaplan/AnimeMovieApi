using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
	public class RosetteModels
	{
		public Rosette Rosette { get; set; }
		public Manga Manga { get; set; }
		public Anime Anime { get; set; }
		public List<AnimeEpisodes> AnimeEpisodes { get; set; }
		public List<MangaEpisodes> MangaEpisodes { get; set; }
		public List<RosetteContent> RosetteContents { get; set; }

	}
}

