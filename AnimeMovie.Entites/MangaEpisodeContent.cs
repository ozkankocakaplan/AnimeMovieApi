using System;
namespace AnimeMovie.Entites
{
	public class MangaEpisodeContent: BaseEntity
    {
		public int EpisodeID { get; set; }
		public string ContentImage { get; set; }
		public string Description { get; set; }
		public int ContentOrder { get; set; }
	}
}

