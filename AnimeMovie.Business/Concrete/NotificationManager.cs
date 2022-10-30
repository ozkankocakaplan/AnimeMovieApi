using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationRepository notificationsRepository;
        public NotificationManager(INotificationRepository notifications)
        {
            notificationsRepository = notifications;
        }

        public ServiceResponse<Notification> add(Notification entity)
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                response.Entity = notificationsRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Notification> delete(Expression<Func<Notification, bool>> expression)
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                response.IsSuccessful = notificationsRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Notification> get(Expression<Func<Notification, bool>> expression)
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                response.Entity = notificationsRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Notification> getList()
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                response.List = notificationsRepository.GetAll().ToList();
                response.Count = notificationsRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Notification> getList(Expression<Func<Notification, bool>> expression)
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                var list = notificationsRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<Notification> getPaginatedNotifications(Expression<Func<Notification, bool>> expression, int pageNo, int ShowCount)
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                var list = notificationsRepository.Table.Where(expression).ToList();
                response.List = list.Skip((pageNo - 1) * ShowCount).Take(ShowCount).ToList();
                int page = 0;
                var totalNotifications = list.Count();
                if (totalNotifications % ShowCount > 0)
                {
                    page++;
                }
                response.Count = page;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Notification> update(Notification entity)
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                response.Entity = notificationsRepository.Update(entity);
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

