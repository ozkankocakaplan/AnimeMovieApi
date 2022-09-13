using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class UserForgotPasswordManager : IUserForgotPasswordService
    {
        private readonly IUserForgotPasswordRepository userForgotPasswordRepository;
        public UserForgotPasswordManager(IUserForgotPasswordRepository userForgotPassword)
        {
            userForgotPasswordRepository = userForgotPassword;
        }

        public ServiceResponse<UserForgotPassword> add(UserForgotPassword entity)
        {
            var response = new ServiceResponse<UserForgotPassword>();
            try
            {
                response.Entity = userForgotPasswordRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserForgotPassword> delete(Expression<Func<UserForgotPassword, bool>> expression)
        {
            var response = new ServiceResponse<UserForgotPassword>();
            try
            {
                userForgotPasswordRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserForgotPassword> get(Expression<Func<UserForgotPassword, bool>> expression)
        {
            var response = new ServiceResponse<UserForgotPassword>();
            try
            {
                response.Entity = userForgotPasswordRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserForgotPassword> getList()
        {
            var response = new ServiceResponse<UserForgotPassword>();
            try
            {
                response.List = userForgotPasswordRepository.GetAll().ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserForgotPassword> getList(Expression<Func<UserForgotPassword, bool>> expression)
        {
            var response = new ServiceResponse<UserForgotPassword>();
            try
            {
                var list = userForgotPasswordRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<UserForgotPassword> update(UserForgotPassword entity)
        {
            var response = new ServiceResponse<UserForgotPassword>();
            try
            {
                response.Entity = userForgotPasswordRepository.Update(entity);
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

