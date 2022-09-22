using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class RosetteContentRepository : GenericRepository<RosetteContent>, IRosetteContentRepository
    {
        public RosetteContentRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

