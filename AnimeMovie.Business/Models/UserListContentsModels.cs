using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Models
{
    public class UserListContentsModels : UserListContents
    {
        public Anime Anime { get; set; }
        public Manga Manga { get; set; }
        public UserListContentsModels(UserListContents userList)
        {
            this.ID = userList.ID;
            this.UserID = userList.UserID;
            this.ListID = userList.ListID;
            this.ContentID = userList.ContentID;
            this.EpisodeID = userList.EpisodeID;
            this.Type = userList.Type;
        }
    }
}

