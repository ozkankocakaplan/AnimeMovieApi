using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using static System.Net.Mime.MediaTypeNames;
namespace AnimeMovie.DataAccess.Concrete
{
    public class MangaRepository : GenericRepository<Manga>, IMangaRepository
    {
        public MangaRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

