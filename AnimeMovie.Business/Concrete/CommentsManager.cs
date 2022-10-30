using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class CommentsManager : ICommentsService
    {
        private readonly ICommentsRepository commentsRepository;
        public CommentsManager(ICommentsRepository comments)
        {
            commentsRepository = comments;
        }

        public ServiceResponse<Comments> add(Comments entity)
        {
            var response = new ServiceResponse<Comments>();
            try
            {
                response.Entity = commentsRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Comments> delete(Expression<Func<Comments, bool>> expression)
        {
            var response = new ServiceResponse<Comments>();
            try
            {
                commentsRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Comments> get(Expression<Func<Comments, bool>> expression)
        {
            var response = new ServiceResponse<Comments>();
            try
            {
                response.Entity = commentsRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Comments> getList()
        {
            var response = new ServiceResponse<Comments>();
            try
            {
                response.List = commentsRepository.TableNoTracking.Include(x => x.Users).ToList();
                response.Count = commentsRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Comments> getList(Expression<Func<Comments, bool>> expression)
        {
            var response = new ServiceResponse<Comments>();
            try
            {
                var list = commentsRepository.TableNoTracking.Include(x=>x.Users).Where(expression).ToList();
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

        public ServiceResponse<Comments> getPaginatedComments(Expression<Func<Comments, bool>> expression, int pageNo, int ShowCount)
        {
            var response = new ServiceResponse<Comments>();
            try
            {
                var list = commentsRepository.Table.Where(expression).ToList();
                response.List = list.Skip((pageNo - 1) * ShowCount).Take(ShowCount).ToList();
                int page = 0;
                var totalComments = list.Count();
                if (totalComments % ShowCount > 0)
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

        public ServiceResponse<Comments> update(Comments entity)
        {
            var response = new ServiceResponse<Comments>();
            try
            {
                response.Entity = commentsRepository.Update(entity);
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

