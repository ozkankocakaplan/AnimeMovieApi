using System;
using System.Linq.Expressions;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface INotificationService : IService<Notification>
    {
        ServiceResponse<Notification> getPaginatedNotifications(Expression<Func<Notification, bool>> expression, int pageNo, int ShowCount);
    }
}

