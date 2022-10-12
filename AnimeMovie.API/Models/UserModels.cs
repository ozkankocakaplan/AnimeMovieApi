using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class UserModels
    {
        public Users User { get; set; }
        public List<UserRosette> Rosettes { get; set; }
        public List<FanArt> FanArts { get; set; }
        public List<Review> Reviews { get; set; }
        public List<AnimeList> AnimeLists { get; set; }
        public List<MangaList> MangaLists { get; set; }
        public List<UserList> UserLists { get; set; }
        public List<UserListContents> UserListContents { get; set; }
    }
}

