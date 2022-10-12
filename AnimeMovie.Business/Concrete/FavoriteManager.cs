using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private readonly IFavoriteRepository favoriteRepository;
        public FavoriteManager(IFavoriteRepository favorite)
        {
            favoriteRepository = favorite;
        }

        public ServiceResponse<Favorite> add(Favorite entity)
        {
            var response = new ServiceResponse<Favorite>();
            try
            {
                response.Entity = favoriteRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Favorite> delete(Expression<Func<Favorite, bool>> expression)
        {
            var response = new ServiceResponse<Favorite>();
            try
            {

                response.IsSuccessful = favoriteRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Favorite> get(Expression<Func<Favorite, bool>> expression)
        {
            var response = new ServiceResponse<Favorite>();
            try
            {
                response.Entity = favoriteRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Favorite> getList()
        {
            var response = new ServiceResponse<Favorite>();
            try
            {
                response.List = favoriteRepository.GetAll().ToList();
                response.Count = favoriteRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Favorite> getList(Expression<Func<Favorite, bool>> expression)
        {
            var response = new ServiceResponse<Favorite>();
            try
            {
                var list = favoriteRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<Favorite> update(Favorite entity)
        {
            var response = new ServiceResponse<Favorite>();
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
    }
}

