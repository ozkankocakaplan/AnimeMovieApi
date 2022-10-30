using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class RatingsRepository : GenericRepository<Ratings>, IRatingsRepository
    {
        public RatingsRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

