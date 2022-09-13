using System;
using AnimeMovie.Entites;
using AnimeMovie.DataAccess.Abstract;
namespace AnimeMovie.DataAccess.Concrete
{
    public class AnimeRepository : GenericRepository<Anime>, IAnimeRepository
    {
        public AnimeRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

