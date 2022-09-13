using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class FanArtRepository : GenericRepository<FanArt>, IFanArtRepository
    {
        public FanArtRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

