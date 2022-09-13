using System;
using System.Linq.Expressions;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface ICommentsService : IService<Comments>
    {
        ServiceResponse<Comments> getPaginatedComments(Expression<Func<Comments, bool>> expression, int pageNo, int ShowCount);
    }
}

