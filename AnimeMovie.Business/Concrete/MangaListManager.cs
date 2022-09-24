using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class MangaListManager : IMangaListService
    {
        private readonly IMangaListRepository mangaListRepository;
        public MangaListManager(IMangaListRepository mangaList)
        {
            mangaListRepository = mangaList;
        }

        public ServiceResponse<MangaList> add(MangaList entity)
        {
            var response = new ServiceResponse<MangaList>();
            try
            {
                response.Entity = mangaListRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaList> delete(Expression<Func<MangaList, bool>> expression)
        {
            var response = new ServiceResponse<MangaList>();
            try
            {
                response.IsSuccessful = mangaListRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaList> get(Expression<Func<MangaList, bool>> expression)
        {
            var response = new ServiceResponse<MangaList>();
            try
            {
                response.Entity = mangaListRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaList> getList()
        {
            var response = new ServiceResponse<MangaList>();
            try
            {
                response.List = mangaListRepository.GetAll().ToList();
                response.Count = mangaListRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<MangaList> getList(Expression<Func<MangaList, bool>> expression)
        {
            var response = new ServiceResponse<MangaList>();
            try
            {
                var list = mangaListRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<MangaList> update(MangaList entity)
        {
            var response = new ServiceResponse<MangaList>();
            try
            {
                response.Entity = mangaListRepository.Update(entity);
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

