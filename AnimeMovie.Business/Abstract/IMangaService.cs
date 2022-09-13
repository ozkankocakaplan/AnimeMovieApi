using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface IMangaService : IService<Manga>
    {
        ServiceResponse<Manga> getPaginatedManga(int pageNo, int ShowCount);
    }
}

