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
    public class RatingsManager : IRatingsService
    {
        private readonly IRatingsRepository RatingsRepository;
        public RatingsManager(IRatingsRepository Ratings)
        {
            RatingsRepository = Ratings;
        }

        public ServiceResponse<Ratings> add(Ratings entity)
        {
            var response = new ServiceResponse<Ratings>();
            try
            {
                response.Entity = RatingsRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Ratings> delete(Expression<Func<Ratings, bool>> expression)
        {
            var response = new ServiceResponse<Ratings>();
            try
            {
                RatingsRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Ratings> get(Expression<Func<Ratings, bool>> expression)
        {
            var response = new ServiceResponse<Ratings>();
            try
            {
                response.Entity = RatingsRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Ratings> getList()
        {
            var response = new ServiceResponse<Ratings>();
            try
            {
                response.List = RatingsRepository.GetAll().ToList();
                response.Count = RatingsRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Ratings> getList(Expression<Func<Ratings, bool>> expression)
        {
            var response = new ServiceResponse<Ratings>();
            try
            {
                var list = RatingsRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<Ratings> update(Ratings entity)
        {
            var response = new ServiceResponse<Ratings>();
            try
            {
                response.Entity = RatingsRepository.Update(entity);
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

