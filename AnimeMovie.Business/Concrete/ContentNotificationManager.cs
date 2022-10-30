using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class ContentNotificationManager : IContentNotificationService
    {
        private readonly IContentNotificationRepository contentNotificationRepository;
        public ContentNotificationManager(IContentNotificationRepository contentNotification)
        {
            contentNotificationRepository = contentNotification;
        }

        public ServiceResponse<ContentNotification> add(ContentNotification entity)
        {
            var response = new ServiceResponse<ContentNotification>();
            try
            {
                response.Entity = contentNotificationRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ContentNotification> delete(Expression<Func<ContentNotification, bool>> expression)
        {
            var response = new ServiceResponse<ContentNotification>();
            try
            {
                response.IsSuccessful = contentNotificationRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ContentNotification> get(Expression<Func<ContentNotification, bool>> expression)
        {
            var response = new ServiceResponse<ContentNotification>();
            try
            {
                response.Entity = contentNotificationRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ContentNotification> getList()
        {
            var response = new ServiceResponse<ContentNotification>();
            try
            {
                response.List = contentNotificationRepository.GetAll().ToList();
                response.Count = contentNotificationRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ContentNotification> getList(Expression<Func<ContentNotification, bool>> expression)
        {
            var response = new ServiceResponse<ContentNotification>();
            try
            {
                var list = contentNotificationRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<ContentNotification> update(ContentNotification entity)
        {
            var response = new ServiceResponse<ContentNotification>();
            try
            {
                response.Entity = contentNotificationRepository.Update(entity);
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

