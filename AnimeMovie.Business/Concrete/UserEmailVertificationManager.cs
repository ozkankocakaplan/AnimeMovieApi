using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class UserEmailVertificationManager : IUserEmailVertificationService
    {
        private readonly IUserEmailVertificationRepository userEmailVertificationRepository;
        public UserEmailVertificationManager(IUserEmailVertificationRepository userEmailVertification)
        {
            userEmailVertificationRepository = userEmailVertification;
        }

        public ServiceResponse<UserEmailVertification> add(UserEmailVertification entity)
        {
            var response = new ServiceResponse<UserEmailVertification>();
            try
            {
                
                response.Entity = userEmailVertificationRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserEmailVertification> delete(Expression<Func<UserEmailVertification, bool>> expression)
        {
            var response = new ServiceResponse<UserEmailVertification>();
            try
            {
                userEmailVertificationRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserEmailVertification> get(Expression<Func<UserEmailVertification, bool>> expression)
        {
            var response = new ServiceResponse<UserEmailVertification>();
            try
            {
                response.Entity = userEmailVertificationRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserEmailVertification> getList()
        {
            var response = new ServiceResponse<UserEmailVertification>();
            try
            {
                response.List = userEmailVertificationRepository.GetAll().ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserEmailVertification> getList(Expression<Func<UserEmailVertification, bool>> expression)
        {
            var response = new ServiceResponse<UserEmailVertification>();
            try
            {
                var list = userEmailVertificationRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<UserEmailVertification> update(UserEmailVertification entity)
        {
            var response = new ServiceResponse<UserEmailVertification>();
            try
            {
                response.Entity = userEmailVertificationRepository.Update(entity);
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

