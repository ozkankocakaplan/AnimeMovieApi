using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class MangaListRepository : GenericRepository<MangaList>, IMangaListRepository
    {
        public MangaListRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

