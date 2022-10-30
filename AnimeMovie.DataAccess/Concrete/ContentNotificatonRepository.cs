using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class ContentNotificatonRepository : GenericRepository<ContentNotification>, IContentNotificationRepository
    {
        public ContentNotificatonRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

