using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class AnimeRatingRepository : GenericRepository<AnimeRating>, IAnimeRatingRepository
    {
        public AnimeRatingRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

