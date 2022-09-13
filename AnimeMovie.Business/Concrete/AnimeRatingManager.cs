using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class AnimeRatingManager : IAnimeRatingService
    {
        private readonly IAnimeRatingRepository animeRatingRepository;
        public AnimeRatingManager(IAnimeRatingRepository animeRating)
        {
            animeRatingRepository = animeRating;
        }

        public ServiceResponse<AnimeRating> add(AnimeRating entity)
        {
            var response = new ServiceResponse<AnimeRating>();
            try
            {
                response.Entity = animeRatingRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeRating> delete(Expression<Func<AnimeRating, bool>> expression)
        {
            var response = new ServiceResponse<AnimeRating>();
            try
            {
                animeRatingRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeRating> get(Expression<Func<AnimeRating, bool>> expression)
        {
            var response = new ServiceResponse<AnimeRating>();
            try
            {
                response.Entity = animeRatingRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeRating> getList()
        {
            var response = new ServiceResponse<AnimeRating>();
            try
            {
                response.List = animeRatingRepository.GetAll().ToList();
                response.Count = animeRatingRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeRating> getList(Expression<Func<AnimeRating, bool>> expression)
        {
            var response = new ServiceResponse<AnimeRating>();
            try
            {
                var list = animeRatingRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<AnimeRating> update(AnimeRating entity)
        {
            var response = new ServiceResponse<AnimeRating>();
            try
            {
                response.Entity = animeRatingRepository.Update(entity);
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

