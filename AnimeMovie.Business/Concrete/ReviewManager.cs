using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class ReviewManager : IReviewService
    {
        private readonly IReviewRepository reviewRepository;
        public ReviewManager(IReviewRepository review)
        {
            reviewRepository = review;
        }

        public ServiceResponse<Review> add(Review entity)
        {
            var response = new ServiceResponse<Review>();
            try
            {
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Review> delete(Expression<Func<Review, bool>> expression)
        {
            var response = new ServiceResponse<Review>();
            try
            {
                reviewRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Review> get(Expression<Func<Review, bool>> expression)
        {
            var response = new ServiceResponse<Review>();
            try
            {
                response.Entity = reviewRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Review> getList()
        {
            var response = new ServiceResponse<Review>();
            try
            {
                response.List = reviewRepository.GetAll().ToList();
                response.Count = reviewRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Review> getList(Expression<Func<Review, bool>> expression)
        {
            var response = new ServiceResponse<Review>();
            try
            {
                var list = reviewRepository.Table.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Review> getPaginatedReviews(Expression<Func<Review, bool>> expression, int pageNo, int ShowCount)
        {
            var response = new ServiceResponse<Review>();
            try
            {
                var list = reviewRepository.Table.Where(expression).ToList();
                response.List = list.Skip((pageNo - 1) * ShowCount).Take(ShowCount).ToList();
                int page = 0;
                var totalReview = list.Count();
                if (totalReview % ShowCount > 0)
                {
                    page++;
                }
                response.Count = page;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Review> update(Review entity)
        {
            var response = new ServiceResponse<Review>();
            try
            {
                response.Entity = reviewRepository.Update(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }
    }
}

