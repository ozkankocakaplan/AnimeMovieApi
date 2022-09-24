using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class UserLoginHistoryManager : IUserLoginHistoryService
    {
        private readonly IUserLoginHistoryRepository userLoginHistoryRepository;
        public UserLoginHistoryManager(IUserLoginHistoryRepository userLoginHistory)
        {
            userLoginHistoryRepository = userLoginHistory;
        }

        public ServiceResponse<UserLoginHistory> add(UserLoginHistory entity)
        {
            var response = new ServiceResponse<UserLoginHistory>();
            try
            {
                userLoginHistoryRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserLoginHistory> delete(Expression<Func<UserLoginHistory, bool>> expression)
        {
            var response = new ServiceResponse<UserLoginHistory>();
            try
            {
                userLoginHistoryRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }
        public ServiceResponse<UserLoginHistory> get(Expression<Func<UserLoginHistory, bool>> expression)
        {
            var response = new ServiceResponse<UserLoginHistory>();
            try
            {
                response.Entity = userLoginHistoryRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserLoginHistory> getList()
        {
            var response = new ServiceResponse<UserLoginHistory>();
            try
            {
                response.List = userLoginHistoryRepository.GetAll().ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserLoginHistory> getList(Expression<Func<UserLoginHistory, bool>> expression)
        {
            var response = new ServiceResponse<UserLoginHistory>();
            try
            {
                var list = userLoginHistoryRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<UserLoginHistory> update(UserLoginHistory entity)
        {
            var response = new ServiceResponse<UserLoginHistory>();
            try
            {
                response.Entity = userLoginHistoryRepository.Update(entity);
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

