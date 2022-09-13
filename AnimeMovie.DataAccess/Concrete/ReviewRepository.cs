using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;


namespace AnimeMovie.DataAccess.Concrete
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

