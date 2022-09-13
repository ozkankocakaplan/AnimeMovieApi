using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class UserMessageRepository : GenericRepository<UserMessage>, IUserMessageRepository
    {
        public UserMessageRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

