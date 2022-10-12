using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

