using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class UserListRepository : GenericRepository<UserList>, IUserListRepository
    {
        public UserListRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

