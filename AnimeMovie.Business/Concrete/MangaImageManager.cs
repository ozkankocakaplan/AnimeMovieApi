using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class MangaImageManager : IMangaImageService
    {
        IMangaImageRepository mangaImageRepository;
        public MangaImageManager(IMangaImageRepository mangaImage)
        {
            mangaImageRepository = mangaImage;
        }
        public ServiceResponse<MangaImages> add(MangaImages entity)
        {
            var response = new ServiceResponse<MangaImages>();
            try
            {
                response.Entity = mangaImageRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<MangaImages> delete(Expression<Func<MangaImages, bool>> expression)
        {
            var response = new ServiceResponse<MangaImages>();
            try
            {
                response.IsSuccessful = mangaImageRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<MangaImages> get(Expression<Func<MangaImages, bool>> expression)
        {
            var response = new ServiceResponse<MangaImages>();
            try
            {
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<MangaImages> getList()
        {
            var response = new ServiceResponse<MangaImages>();
            try
            {
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<MangaImages> getList(Expression<Func<MangaImages, bool>> expression)
        {
            var response = new ServiceResponse<MangaImages>();
            try
            {
                var list = mangaImageRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<MangaImages> update(MangaImages entity)
        {
            var response = new ServiceResponse<MangaImages>();
            try
            {
                response.Entity = mangaImageRepository.Update(entity);
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

