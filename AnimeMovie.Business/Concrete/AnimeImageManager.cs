using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class AnimeImageManager : IAnimeImageService
    {
        public IAnimeImageRepository animeImageRepository { get; set; }
        public AnimeImageManager(IAnimeImageRepository animeImage)
        {
            animeImageRepository = animeImage;
        }

        public ServiceResponse<AnimeImages> add(AnimeImages entity)
        {
            var response = new ServiceResponse<AnimeImages>();
            try
            {
                response.Entity = animeImageRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeImages> delete(Expression<Func<AnimeImages, bool>> expression)
        {
            var response = new ServiceResponse<AnimeImages>();
            try
            {
                response.IsSuccessful = animeImageRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeImages> get(Expression<Func<AnimeImages, bool>> expression)
        {
            var response = new ServiceResponse<AnimeImages>();
            try
            {
                response.Entity = animeImageRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeImages> getList()
        {
            var response = new ServiceResponse<AnimeImages>();
            try
            {
                response.List = animeImageRepository.GetAll().ToList();
                response.Count = animeImageRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeImages> getList(Expression<Func<AnimeImages, bool>> expression)
        {
            var response = new ServiceResponse<AnimeImages>();
            try
            {
                var list = animeImageRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<AnimeImages> update(AnimeImages entity)
        {
            var response = new ServiceResponse<AnimeImages>();
            try
            {
                response.Entity = animeImageRepository.Update(entity);
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

