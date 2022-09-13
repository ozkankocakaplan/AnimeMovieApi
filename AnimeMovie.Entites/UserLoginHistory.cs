using System;
namespace AnimeMovie.Entites
{
    public class UserLoginHistory : BaseEntity
    {
        public int UserID { get; set; }
        public DateTime LastSeen { get; set; }
    }
}

