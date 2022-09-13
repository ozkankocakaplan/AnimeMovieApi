using System;
namespace AnimeMovie.Entites
{
    public class UserForgotPassword : BaseEntity
    {
        public int UserID { get; set; }
        public string ResetLink { get; set; }

    }
}

