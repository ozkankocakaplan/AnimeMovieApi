using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Models
{
    public class UserListModels : UserListContents
    {
        public Anime Anime { get; set; }
        public Manga Manga { get; set; }
        public List<AnimeEpisodes> AnimeEpisodes { get; set; }
        public List<MangaEpisodes> MangaEpisodes { get; set; }
        public List<AnimeSeason> AnimeSeasons { get; set; }
        public List<CategoryType> Categories { get; set; }
        public UserListModels(UserListContents listContents)
        {
            this.ID = listContents.ID;
            this.UserID = listContents.UserID;
            this.ListID = listContents.ListID;
            this.ContentID = listContents.ContentID;
            this.EpisodeID = listContents.EpisodeID;
            this.Type = listContents.Type;
            this.Users = listContents.Users;
            this.UserList = listContents.UserList;

        }
    }
}

