using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        public LikeRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

