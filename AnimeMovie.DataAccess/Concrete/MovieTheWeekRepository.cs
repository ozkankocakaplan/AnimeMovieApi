using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class MovieTheWeekRepository : GenericRepository<MovieTheWeek>, IMovieTheWeekRepository
    {
        public MovieTheWeekRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

