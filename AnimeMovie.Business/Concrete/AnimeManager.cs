using System;
using System.Linq.Expressions;
using AnimeMovie.Entites;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Business.Abstract;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using AnimeMovie.Business.Helper;

namespace AnimeMovie.Business.Concrete
{
    public class AnimeManager : IAnimeService
    {
        private readonly IAnimeRepository animeRepository;
        private readonly ISeoUrl seoUrl;
        public AnimeManager(IAnimeRepository anime, ISeoUrl seo)
        {
            seoUrl = seo;
            animeRepository = anime;
        }

        public ServiceResponse<Anime> add(Anime anime)
        {
            var response = new ServiceResponse<Anime>();
            try
            {
                anime.SeoUrl = seoUrl.createAnimeLink(anime);
                response.Entity = animeRepository.Create(anime);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Anime> delete(Expression<Func<Anime, bool>> expression)
        {
            var response = new ServiceResponse<Anime>();
            try
            {
                animeRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Anime> get(Expression<Func<Anime, bool>> expression)
        {
            var response = new ServiceResponse<Anime>();
            try
            {
                response.Entity = animeRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Anime> getList()
        {
            var response = new ServiceResponse<Anime>();
            try
            {
                response.List = animeRepository.GetAll().ToList();
                response.Count = animeRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Anime> getList(Expression<Func<Anime, bool>> expression)
        {
            var response = new ServiceResponse<Anime>();
            try
            {
                var list = animeRepository.Table.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Anime> getPaginatedAnime(int pageNo, int ShowCount)
        {
            var response = new ServiceResponse<Anime>();
            try
            {
                var list = animeRepository.GetAll();
                response.List = list.Skip((pageNo - 1) * ShowCount).Take(ShowCount).ToList();
                int page = 0;
                var totalAnime = list.Count();
                if(totalAnime % ShowCount > 0)
                {
                    page++;
                }
                response.Count = page;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Anime> update(Anime anime)
        {
            var response = new ServiceResponse<Anime>();
            try
            {
                anime.SeoUrl = seoUrl.createAnimeLink(anime);
                var _Anime = get(x => x.ID == anime.ID);
                response.Entity = animeRepository.Update(anime);
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

