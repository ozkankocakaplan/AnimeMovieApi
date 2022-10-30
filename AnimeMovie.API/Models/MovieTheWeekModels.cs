using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
	public class MovieTheWeekModels:MovieTheWeek
	{
		public Anime Anime { get; set; }
		public Manga Manga { get; set; }
		public Users Users { get; set; }
		public MovieTheWeekModels(MovieTheWeek movieThe)
		{
			this.ID = movieThe.ID;
			this.ContentID = movieThe.ContentID;
			this.CreateTime = movieThe.CreateTime;
			this.Description = movieThe.Description;
			this.Type = movieThe.Type;
			this.UserID = movieThe.UserID;
		}
	}
}

