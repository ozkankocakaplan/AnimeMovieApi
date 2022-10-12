using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class UserListManager : IUserListService
    {
        private readonly IUserListRepository userListRepository;
        private readonly IUserListContentsRepository userListContentsRepository;
        public UserListManager(IUserListRepository userlist, IUserListContentsRepository userListContents)
        {
            userListRepository = userlist;
            userListContentsRepository = userListContents;
        }

        public ServiceResponse<UserList> add(UserList entity)
        {
            var response = new ServiceResponse<UserList>();
            try
            {
                response.Entity = userListRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserList> addUserList(UserList userList, List<UserListContents> userListContents)
        {
            var response = new ServiceResponse<UserList>();
            try
            {
                var entity = userListRepository.Create(userList); ;
                response.Entity = entity;
                foreach (var content in userListContents)
                {
                    content.ListID = entity.ID;
                    userListContentsRepository.Create(content);
                }

                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserList> delete(Expression<Func<UserList, bool>> expression)
        {
            var response = new ServiceResponse<UserList>();
            try
            {
                response.IsSuccessful = userListRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserList> get(Expression<Func<UserList, bool>> expression)
        {
            var response = new ServiceResponse<UserList>();
            try
            {
                response.Entity = userListRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserList> getList()
        {
            var response = new ServiceResponse<UserList>();
            try
            {
                response.Count = userListRepository.Count();
                response.List = userListRepository.GetAll().ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserList> getList(Expression<Func<UserList, bool>> expression)
        {
            var response = new ServiceResponse<UserList>();
            try
            {
                var list = userListRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<UserList> update(UserList entity)
        {
            var response = new ServiceResponse<UserList>();
            try
            {
                response.Entity = userListRepository.Update(entity);
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

