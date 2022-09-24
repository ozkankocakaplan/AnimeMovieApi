using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class MangaEpisodeContentManager : IMangaEpisodeContentService
    {
        private readonly IMangaEpisodeContentRepository mangaEpisodeContentRepository;
        public MangaEpisodeContentManager(IMangaEpisodeContentRepository mangaEpisodeContent)
        {
            mangaEpisodeContentRepository = mangaEpisodeContent;
        }

        public ServiceResponse<MangaEpisodeContent> add(MangaEpisodeContent entity)
        {
            var response = new ServiceResponse<MangaEpisodeContent>();
            try
            {
                response.Entity = mangaEpisodeContentRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaEpisodeContent> delete(Expression<Func<MangaEpisodeContent, bool>> expression)
        {
            var response = new ServiceResponse<MangaEpisodeContent>();
            try
            {
                response.IsSuccessful = mangaEpisodeContentRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaEpisodeContent> get(Expression<Func<MangaEpisodeContent, bool>> expression)
        {
            var response = new ServiceResponse<MangaEpisodeContent>();
            try
            {
                response.Entity = mangaEpisodeContentRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaEpisodeContent> getList()
        {
            var response = new ServiceResponse<MangaEpisodeContent>();
            try
            {
                response.List = mangaEpisodeContentRepository.GetAll().ToList();
                response.Count = mangaEpisodeContentRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaEpisodeContent> getList(Expression<Func<MangaEpisodeContent, bool>> expression)
        {
            var response = new ServiceResponse<MangaEpisodeContent>();
            try
            {
                var list = mangaEpisodeContentRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<MangaEpisodeContent> update(MangaEpisodeContent entity)
        {
            var response = new ServiceResponse<MangaEpisodeContent>();
            try
            {
                response.Entity = mangaEpisodeContentRepository.Update(entity);
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

