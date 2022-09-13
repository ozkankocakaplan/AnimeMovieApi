using System;
using System.Linq.Expressions;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface IFanArtService : IService<FanArt>
    {
        ServiceResponse<FanArt> getPaginatedFanArt(Expression<Func<FanArt,bool>> expression, int pageNo, int ShowCount);
    }
}

