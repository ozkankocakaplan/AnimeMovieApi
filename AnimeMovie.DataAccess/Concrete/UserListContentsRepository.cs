using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class UserListContentsRepository : GenericRepository<UserListContents>, IUserListContentsRepository
    {
        public UserListContentsRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

