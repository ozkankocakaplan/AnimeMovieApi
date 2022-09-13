using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class HomeSliderRepository : GenericRepository<HomeSlider>, IHomeSliderRepository
    {
        public HomeSliderRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

