using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class UserListContentsManager : IUserListContentsService
    {
        private readonly IUserListContentsRepository userListContentsRepository;
        public UserListContentsManager(IUserListContentsRepository userListContents)
        {
            userListContentsRepository = userListContents;
        }

        public ServiceResponse<UserListContents> add(UserListContents entity)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.Entity = userListContentsRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> delete(Expression<Func<UserListContents, bool>> expression)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.IsSuccessful = userListContentsRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> get(Expression<Func<UserListContents, bool>> expression)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.Entity = userListContentsRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> getList()
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.List = userListContentsRepository.GetAll().ToList();
                response.Count = userListContentsRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> getList(Expression<Func<UserListContents, bool>> expression)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                var list = userListContentsRepository.Table.Where(expression).ToList();
                response.Count = list.Count;
                response.List = list;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> update(UserListContents entity)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.Entity = userListContentsRepository.Update(entity);
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

