using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class SiteDescriptionRepository : GenericRepository<SiteDescription>, ISiteDescriptionRepository
    {
        public SiteDescriptionRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

