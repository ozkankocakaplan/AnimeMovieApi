using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Business.Helper;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class MangaManager : IMangaService
    {
        private readonly IMangaRepository mangaRepository;
        private readonly ISeoUrl seoUrl;
        public MangaManager(IMangaRepository manga, ISeoUrl seo)
        {
            mangaRepository = manga;
            seoUrl = seo;
        }

        public ServiceResponse<Manga> add(Manga entity)
        {
            var response = new ServiceResponse<Manga>();
            try
            {
                entity.SeoUrl = seoUrl.createMangaLink(entity);
                response.Entity = mangaRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Manga> delete(Expression<Func<Manga, bool>> expression)
        {
            var response = new ServiceResponse<Manga>();
            try
            {
                mangaRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Manga> get(Expression<Func<Manga, bool>> expression)
        {
            var response = new ServiceResponse<Manga>();
            try
            {
                response.Entity = mangaRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Manga> getList()
        {
            var response = new ServiceResponse<Manga>();
            try
            {
                response.List = mangaRepository.GetAll().ToList();
                response.Count = mangaRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Manga> getList(Expression<Func<Manga, bool>> expression)
        {
            var response = new ServiceResponse<Manga>();
            try
            {
                var list = mangaRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<Manga> getPaginatedManga(int pageNo, int ShowCount)
        {
            var response = new ServiceResponse<Manga>();
            try
            {
                var list = mangaRepository.GetAll();
                response.List = list.Skip((pageNo - 1) * ShowCount).Take(ShowCount).ToList();
                int page = 0;
                var totalAnime = mangaRepository.Count();
                if (totalAnime % ShowCount > 0)
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

        public ServiceResponse<Manga> update(Manga entity)
        {
            var response = new ServiceResponse<Manga>();
            try
            {
                entity.SeoUrl = seoUrl.createMangaLink(entity);
                response.Entity = mangaRepository.Update(entity);
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

