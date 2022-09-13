using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class AnimeListRepository : GenericRepository<AnimeList>, IAnimeListRepository
    {
        public AnimeListRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

