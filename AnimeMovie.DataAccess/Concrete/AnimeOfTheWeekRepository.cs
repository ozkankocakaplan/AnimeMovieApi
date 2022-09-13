using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class AnimeOfTheWeekRepository : GenericRepository<AnimeOfTheWeek>, IAnimeOfTheWeekRepository
    {
        public AnimeOfTheWeekRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

