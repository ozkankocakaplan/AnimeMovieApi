using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class AnimeSeasonMusicRepository : GenericRepository<AnimeSeasonMusic>, IAnimeSeasonMusicRepository
    {
        public AnimeSeasonMusicRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

