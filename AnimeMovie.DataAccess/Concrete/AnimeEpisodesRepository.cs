using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class AnimeEpisodesRepository : GenericRepository<AnimeEpisodes>, IAnimeEpisodesRepository
    {
        public AnimeEpisodesRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

