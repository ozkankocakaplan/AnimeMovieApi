using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class UserMessageManager : IUserMessageService
    {
        private readonly IUserMessageRepository userMessageRepository;
        public UserMessageManager(IUserMessageRepository userMesssage)
        {
            userMessageRepository = userMesssage;
        }

        public ServiceResponse<UserMessage> add(UserMessage entity)
        {
            var response = new ServiceResponse<UserMessage>();
            try
            {
                response.Entity = userMessageRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserMessage> delete(Expression<Func<UserMessage, bool>> expression)
        {
            var response = new ServiceResponse<UserMessage>();
            try
            {
                userMessageRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserMessage> get(Expression<Func<UserMessage, bool>> expression)
        {
            var response = new ServiceResponse<UserMessage>();
            try
            {
                response.Entity = userMessageRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserMessage> getList()
        {
            var response = new ServiceResponse<UserMessage>();
            try
            {
                response.List = userMessageRepository.GetAll().ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<UserMessage> getList(Expression<Func<UserMessage, bool>> expression)
        {
            var response = new ServiceResponse<UserMessage>();
            try
            {
                var list = userMessageRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<UserMessage> update(UserMessage entity)
        {
            var response = new ServiceResponse<UserMessage>();
            try
            {
                response.Entity = userMessageRepository.Update(entity);
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

