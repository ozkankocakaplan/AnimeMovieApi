using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class RosetteRepository : GenericRepository<Rosette>, IRosetteRepository
    {
        public RosetteRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

