using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class UserBlockListManager : IUserBlockListService
    {
        private readonly IUserBlockListRepository userBlockListRepository;
        public UserBlockListManager(IUserBlockListRepository userBlockList)
        {
            userBlockListRepository = userBlockList;
        }

        public ServiceResponse<UserBlockList> add(UserBlockList entity)
        {
            var response = new ServiceResponse<UserBlockList>();
            try
            {
                response.Entity = userBlockListRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserBlockList> delete(Expression<Func<UserBlockList, bool>> expression)
        {
            var response = new ServiceResponse<UserBlockList>();
            try
            {
                userBlockListRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserBlockList> get(Expression<Func<UserBlockList, bool>> expression)
        {
            var response = new ServiceResponse<UserBlockList>();
            try
            {
                response.Entity = userBlockListRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserBlockList> getList()
        {
            var response = new ServiceResponse<UserBlockList>();
            try
            {
                response.List = userBlockListRepository.GetAll().ToList();
                response.Count = userBlockListRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserBlockList> getList(Expression<Func<UserBlockList, bool>> expression)
        {
            var response = new ServiceResponse<UserBlockList>();
            try
            {
                var list = userBlockListRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<UserBlockList> update(UserBlockList entity)
        {
            var response = new ServiceResponse<UserBlockList>();
            try
            {
                response.Entity = userBlockListRepository.Update(entity);
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

