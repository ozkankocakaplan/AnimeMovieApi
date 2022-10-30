using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class MovieOfTheWeekManager : IMovieTheWeekService
    {
        private readonly IMovieTheWeekRepository animeOfTheWeekRepository;
        public MovieOfTheWeekManager(IMovieTheWeekRepository animeOfTheWeek)
        {
            animeOfTheWeekRepository = animeOfTheWeek;
        }

        public ServiceResponse<MovieTheWeek> add(MovieTheWeek entity)
        {
            var response = new ServiceResponse<MovieTheWeek>();
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

        public ServiceResponse<MovieTheWeek> delete(Expression<Func<MovieTheWeek, bool>> expression)
        {
            var response = new ServiceResponse<MovieTheWeek>();
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

        public ServiceResponse<MovieTheWeek> get(Expression<Func<MovieTheWeek, bool>> expression)
        {
            var response = new ServiceResponse<MovieTheWeek>();
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

        public ServiceResponse<MovieTheWeek> getList()
        {
            var response = new ServiceResponse<MovieTheWeek>();
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

        public ServiceResponse<MovieTheWeek> getList(Expression<Func<MovieTheWeek, bool>> expression)
        {
            var response = new ServiceResponse<MovieTheWeek>();
            try
            {
                var list = animeOfTheWeekRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<MovieTheWeek> update(MovieTheWeek entity)
        {
            var response = new ServiceResponse<MovieTheWeek>();
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

