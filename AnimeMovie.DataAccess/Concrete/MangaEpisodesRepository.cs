using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class MangaEpisodesRepository : GenericRepository<MangaEpisodes>, IMangaEpisodesRepository
    {
        public MangaEpisodesRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

