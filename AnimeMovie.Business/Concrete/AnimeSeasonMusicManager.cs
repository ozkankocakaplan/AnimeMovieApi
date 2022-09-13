using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class AnimeSeasonMusicManager : IAnimeSeasonMusicService
    {
        private readonly IAnimeSeasonMusicRepository animeSeasonMusicRepository;
        public AnimeSeasonMusicManager(IAnimeSeasonMusicRepository animeSeasonMusic)
        {
            animeSeasonMusicRepository = animeSeasonMusic;
        }

        public ServiceResponse<AnimeSeasonMusic> add(AnimeSeasonMusic entity)
        {
            var response = new ServiceResponse<AnimeSeasonMusic>();
            try
            {
                response.Entity = animeSeasonMusicRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeasonMusic> delete(Expression<Func<AnimeSeasonMusic, bool>> expression)
        {
            var response = new ServiceResponse<AnimeSeasonMusic>();
            try
            {
                animeSeasonMusicRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeasonMusic> get(Expression<Func<AnimeSeasonMusic, bool>> expression)
        {
            var response = new ServiceResponse<AnimeSeasonMusic>();
            try
            {
                response.Entity = animeSeasonMusicRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeasonMusic> getList()
        {
            var response = new ServiceResponse<AnimeSeasonMusic>();
            try
            {
                response.List = animeSeasonMusicRepository.GetAll().ToList();
                response.Count = animeSeasonMusicRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeasonMusic> getList(Expression<Func<AnimeSeasonMusic, bool>> expression)
        {
            var response = new ServiceResponse<AnimeSeasonMusic>();
            try
            {
                var list = animeSeasonMusicRepository.Table.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeasonMusic> update(AnimeSeasonMusic entity)
        {
            var response = new ServiceResponse<AnimeSeasonMusic>();
            try
            {
                response.Entity = animeSeasonMusicRepository.Update(entity);
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

