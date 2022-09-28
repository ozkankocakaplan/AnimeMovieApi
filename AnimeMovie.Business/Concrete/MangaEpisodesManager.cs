using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class MangaEpisodesManager : IMangaEpisodesService
    {
        private readonly IMangaEpisodesRepository mangaEpisodesRepository;
        public MangaEpisodesManager(IMangaEpisodesRepository mangaEpisodes)
        {
            mangaEpisodesRepository = mangaEpisodes;
        }

        public ServiceResponse<MangaEpisodes> add(MangaEpisodes entity)
        {
            var response = new ServiceResponse<MangaEpisodes>();
            try
            {
                response.Entity = mangaEpisodesRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaEpisodes> delete(Expression<Func<MangaEpisodes, bool>> expression)
        {
            var response = new ServiceResponse<MangaEpisodes>();
            try
            {
                response.IsSuccessful = mangaEpisodesRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaEpisodes> get(Expression<Func<MangaEpisodes, bool>> expression)
        {
            var response = new ServiceResponse<MangaEpisodes>();
            try
            {
                response.Entity = mangaEpisodesRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaEpisodes> getList()
        {
            var response = new ServiceResponse<MangaEpisodes>();
            try
            {
                response.List = mangaEpisodesRepository.GetAll().ToList();
                response.Count = mangaEpisodesRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaEpisodes> getList(Expression<Func<MangaEpisodes, bool>> expression)
        {
            var response = new ServiceResponse<MangaEpisodes>();
            try
            {
                var list = mangaEpisodesRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<MangaEpisodes> update(MangaEpisodes entity)
        {
            var response = new ServiceResponse<MangaEpisodes>();
            try
            {
                response.Entity = mangaEpisodesRepository.Update(entity);
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

