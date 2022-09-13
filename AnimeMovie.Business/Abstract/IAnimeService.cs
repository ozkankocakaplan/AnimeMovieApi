using System;
using System.Linq.Expressions;
using AnimeMovie.Entites;
namespace AnimeMovie.Business.Abstract
{
    public interface IAnimeService : IService<Anime>
    {
        ServiceResponse<Anime> getPaginatedAnime(int pageNo, int ShowCount);
    }
}

