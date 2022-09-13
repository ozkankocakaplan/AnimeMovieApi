using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class UserLoginHistoryRepository : GenericRepository<UserLoginHistory>, IUserLoginHistoryRepository
    {
        public UserLoginHistoryRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

