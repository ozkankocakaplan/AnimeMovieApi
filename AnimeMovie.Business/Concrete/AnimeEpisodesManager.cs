using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class AnimeEpisodesManager : IAnimeEpisodesService
    {
        IAnimeEpisodesRepository animeEpisodesRepository;
        public AnimeEpisodesManager(IAnimeEpisodesRepository animeEpisodes)
        {
            animeEpisodesRepository = animeEpisodes;
        }

        public ServiceResponse<AnimeEpisodes> add(AnimeEpisodes entity)
        {
            var response = new ServiceResponse<AnimeEpisodes>();
            try
            {
                response.Entity = animeEpisodesRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeEpisodes> delete(Expression<Func<AnimeEpisodes, bool>> expression)
        {
            var response = new ServiceResponse<AnimeEpisodes>();
            try
            {
                animeEpisodesRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeEpisodes> get(Expression<Func<AnimeEpisodes, bool>> expression)
        {
            var response = new ServiceResponse<AnimeEpisodes>();
            try
            {
                response.Entity = animeEpisodesRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeEpisodes> getList()
        {
            var response = new ServiceResponse<AnimeEpisodes>();
            try
            {
                response.List = animeEpisodesRepository.GetAll().ToList();
                response.Count = animeEpisodesRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeEpisodes> getList(Expression<Func<AnimeEpisodes, bool>> expression)
        {
            var response = new ServiceResponse<AnimeEpisodes>();
            try
            {
                var list = animeEpisodesRepository.Table.Where(expression);
                response.Count = list.Count();
                response.List = list.ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeEpisodes> update(AnimeEpisodes entity)
        {
            var response = new ServiceResponse<AnimeEpisodes>();
            try
            {
                response.Entity = animeEpisodesRepository.Update(entity);
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

