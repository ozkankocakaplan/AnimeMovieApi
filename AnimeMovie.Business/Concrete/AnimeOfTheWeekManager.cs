using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class AnimeOfTheWeekManager : IAnimeOfTheWeekService
    {
        private readonly IAnimeOfTheWeekRepository animeOfTheWeekRepository;
        public AnimeOfTheWeekManager(IAnimeOfTheWeekRepository animeOfTheWeek)
        {
            animeOfTheWeekRepository = animeOfTheWeek;
        }

        public ServiceResponse<AnimeOfTheWeek> add(AnimeOfTheWeek entity)
        {
            var response = new ServiceResponse<AnimeOfTheWeek>();
            try
            {
                response.Entity = animeOfTheWeekRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeOfTheWeek> delete(Expression<Func<AnimeOfTheWeek, bool>> expression)
        {
            var response = new ServiceResponse<AnimeOfTheWeek>();
            try
            {
                animeOfTheWeekRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeOfTheWeek> get(Expression<Func<AnimeOfTheWeek, bool>> expression)
        {
            var response = new ServiceResponse<AnimeOfTheWeek>();
            try
            {
                response.Entity = animeOfTheWeekRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeOfTheWeek> getList()
        {
            var response = new ServiceResponse<AnimeOfTheWeek>();
            try
            {
                response.List = animeOfTheWeekRepository.GetAll().ToList();
                response.Count = animeOfTheWeekRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeOfTheWeek> getList(Expression<Func<AnimeOfTheWeek, bool>> expression)
        {
            var response = new ServiceResponse<AnimeOfTheWeek>();
            try
            {
                var list = animeOfTheWeekRepository.Table.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeOfTheWeek> update(AnimeOfTheWeek entity)
        {
            var response = new ServiceResponse<AnimeOfTheWeek>();
            try
            {
                response.Entity = animeOfTheWeekRepository.Update(entity);
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

