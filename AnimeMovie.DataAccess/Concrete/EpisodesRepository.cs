using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class EpisodesRepository : GenericRepository<Episodes>, IEpisodesRepository
    {
        public EpisodesRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

