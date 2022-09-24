using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class MangaImageRepository : GenericRepository<MangaImages>, IMangaImageRepository
    {
        public MangaImageRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

