using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class ContentComplaintManager : IContentComplaintService
    {
        private readonly IContentComplaintRepository contentComplaintRepository;
        public ContentComplaintManager(IContentComplaintRepository contentComplaint)
        {
            contentComplaintRepository = contentComplaint;
        }

        public ServiceResponse<ContentComplaint> add(ContentComplaint entity)
        {
            var response = new ServiceResponse<ContentComplaint>();
            try
            {
                response.Entity = contentComplaintRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<ContentComplaint> delete(Expression<Func<ContentComplaint, bool>> expression)
        {
            var response = new ServiceResponse<ContentComplaint>();
            try
            {
                contentComplaintRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<ContentComplaint> get(Expression<Func<ContentComplaint, bool>> expression)
        {
            var response = new ServiceResponse<ContentComplaint>();
            try
            {
                response.Entity = contentComplaintRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<ContentComplaint> getList()
        {
            var response = new ServiceResponse<ContentComplaint>();
            try
            {
                response.List = contentComplaintRepository.GetAll().ToList();
                response.Count = contentComplaintRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<ContentComplaint> getList(Expression<Func<ContentComplaint, bool>> expression)
        {
            var response = new ServiceResponse<ContentComplaint>();
            try
            {
                var list = contentComplaintRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<ContentComplaint> getListPagined(int pageNo, int showCount)
        {
            var response = new ServiceResponse<ContentComplaint>();
            try
            {
                var list = contentComplaintRepository.GetAll();
                response.List = list.Skip((pageNo - 1) * showCount).Take(showCount).ToList();
                int page = 0;
                var totalContent = list.Count();
                if (totalContent % showCount > 0)
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

        public ServiceResponse<ContentComplaint> update(ContentComplaint entity)
        {
            var response = new ServiceResponse<ContentComplaint>();
            try
            {
                response.Entity = contentComplaintRepository.Update(entity);
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

