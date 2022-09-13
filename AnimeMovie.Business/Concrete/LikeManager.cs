using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class LikeManager : ILikeService
    {
        private readonly ILikeRepository likeRepository;
        public LikeManager(ILikeRepository like)
        {
            likeRepository = like;
        }

        public ServiceResponse<Like> add(Like entity)
        {
            var response = new ServiceResponse<Like>();
            try
            {
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Like> delete(Expression<Func<Like, bool>> expression)
        {
            var response = new ServiceResponse<Like>();
            try
            {
                response.IsSuccessful = likeRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Like> get(Expression<Func<Like, bool>> expression)
        {
            var response = new ServiceResponse<Like>();
            try
            {
                response.Entity = likeRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Like> getList()
        {
            var response = new ServiceResponse<Like>();
            try
            {
                response.List = likeRepository.GetAll().ToList();
                response.Count = likeRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Like> getList(Expression<Func<Like, bool>> expression)
        {
            var response = new ServiceResponse<Like>();
            try
            {
                var list = likeRepository.Table.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Like> update(Like entity)
        {
            var response = new ServiceResponse<Like>();
            try
            {
                response.Entity = likeRepository.Update(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }
    }
}

