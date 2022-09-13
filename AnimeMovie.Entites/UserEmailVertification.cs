using System;
namespace AnimeMovie.Entites
{
    public class UserEmailVertification : BaseEntity
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}

