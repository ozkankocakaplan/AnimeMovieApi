using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class EpisodesManager : IEpisodesService
    {
        private readonly IEpisodesRepository episodesRepository;
        public EpisodesManager(IEpisodesRepository episodes)
        {
            episodesRepository = episodes;
        }

        public ServiceResponse<Episodes> add(Episodes entity)
        {
            var response = new ServiceResponse<Episodes>();
            try
            {
                response.Entity = episodesRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Episodes> delete(Expression<Func<Episodes, bool>> expression)
        {
            var response = new ServiceResponse<Episodes>();
            try
            {
                episodesRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Episodes> get(Expression<Func<Episodes, bool>> expression)
        {
            var response = new ServiceResponse<Episodes>();
            try
            {
                response.Entity = episodesRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Episodes> getList()
        {
            var response = new ServiceResponse<Episodes>();
            try
            {
                response.List = episodesRepository.GetAll().ToList();
                response.Count = episodesRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Episodes> getList(Expression<Func<Episodes, bool>> expression)
        {
            var response = new ServiceResponse<Episodes>();
            try
            {
                var list = episodesRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<Episodes> update(Episodes entity)
        {
            var response = new ServiceResponse<Episodes>();
            try
            {
                response.Entity = episodesRepository.Update(entity);
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

