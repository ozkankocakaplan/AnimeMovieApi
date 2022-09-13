using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository

    {
        public NotificationRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

