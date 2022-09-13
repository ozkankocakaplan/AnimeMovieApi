using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class UserEmailVertificationRepository : GenericRepository<UserEmailVertification>, IUserEmailVertificationRepository
    {
        public UserEmailVertificationRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

