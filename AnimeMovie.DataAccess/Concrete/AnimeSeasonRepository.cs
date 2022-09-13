using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class AnimeSeasonRepository : GenericRepository<AnimeSeason>, IAnimeSeasonRepository
    {
        public AnimeSeasonRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

