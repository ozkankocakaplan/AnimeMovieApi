using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class SocialMediaAccountRepository : GenericRepository<SocialMediaAccount>, ISocialMediaAccountRepository
    {
        public SocialMediaAccountRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

