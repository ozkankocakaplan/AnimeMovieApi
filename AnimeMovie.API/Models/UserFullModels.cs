using System;
using AnimeMovie.Business.Models;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class UserFullModels
    {
        public Users User { get; set; }
        public List<AnimeListModels> AnimeListModels { get; set; }
        public List<MangaListModels> MangaListModels { get; set; }
        public List<UserListModels> UserListModels { get; set; }
        public List<UserRosette> Rosettes { get; set; }
        public List<FanArtModel> FanArts { get; set; }
        public List<ReviewsModels> Reviews { get; set; }
        public List<AnimeList> AnimeLists { get; set; }
        public List<MangaList> MangaLists { get; set; }
        public List<UserList> UserLists { get; set; }
        public List<UserListContents> UserListContents { get; set; }
    }
}

