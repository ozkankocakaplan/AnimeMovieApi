using System;
using System.Linq.Expressions;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface IReviewService : IService<Review>
    {
        ServiceResponse<Review> getPaginatedReviews(Expression<Func<Review, bool>> expression, int pageNo, int ShowCount);
    }
}

