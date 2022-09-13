using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class UserBlockListRepository : GenericRepository<UserBlockList>, IUserBlockListRepository
    {
        public UserBlockListRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

