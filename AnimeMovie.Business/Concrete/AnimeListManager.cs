using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore;

namespace AnimeMovie.Business.Concrete
{
    public class AnimeListManager : IAnimeListService
    {
        private readonly IAnimeListRepository animeListRepository;
        public AnimeListManager(IAnimeListRepository animeList)
        {
            animeListRepository = animeList;
        }

        public ServiceResponse<AnimeList> add(AnimeList entity)
        {
            var response = new ServiceResponse<AnimeList>();
            try
            {
                response.Entity = animeListRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeList> delete(Expression<Func<AnimeList, bool>> expression)
        {
            var response = new ServiceResponse<AnimeList>();
            try
            {
                animeListRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeList> get(Expression<Func<AnimeList, bool>> expression)
        {
            var response = new ServiceResponse<AnimeList>();
            try
            {
                response.Entity = animeListRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeList> getList()
        {
            var response = new ServiceResponse<AnimeList>();
            try
            {
                response.List = animeListRepository.GetAll().ToList();
                response.Count = animeListRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<AnimeList> getList(Expression<Func<AnimeList, bool>> expression)
        {
            var response = new ServiceResponse<AnimeList>();
            try
            {
                var list = animeListRepository.TableNoTracking.Include(x => x.Anime).Include(x => x.AnimeEpisode).Where(expression).ToList();
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

        public ServiceResponse<AnimeList> update(AnimeList entity)
        {
            var response = new ServiceResponse<AnimeList>();
            try
            {
                response.Entity = animeListRepository.Update(entity);
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

