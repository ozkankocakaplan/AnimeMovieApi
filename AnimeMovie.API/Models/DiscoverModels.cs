using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
	public class DiscoverModels
	{
		public List<FanArtModel> FanArts { get; set; }
		public List<ReviewsModels> Reviews { get; set; }
		public List<Anime> TopAnimes { get; set; }
		public List<Manga> TopMangas { get; set; }
		public List<MovieTheWeekModels> MovieTheWeeks { get; set; }
	}
}

