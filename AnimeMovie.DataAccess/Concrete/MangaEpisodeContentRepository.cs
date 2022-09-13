using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class MangaEpisodeContentRepository : GenericRepository<MangaEpisodeContent>, IMangaEpisodeContentRepository
    {
        public MangaEpisodeContentRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

