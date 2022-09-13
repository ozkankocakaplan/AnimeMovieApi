using System;
namespace AnimeMovie.Entites
{
    public class Notification : BaseEntity
    {
        public NotificationType NotificationType { get; set; }
        public int UserID { get; set; }
        public string NotificationMessage { get; set; }
        public bool isReadInfo { get; set; } = false;
    }
}

