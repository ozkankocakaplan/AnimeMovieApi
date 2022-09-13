using System;
namespace AnimeMovie.Entites
{
    public class UserRosette : BaseEntity
    {
        public int UserID { get; set; }
        public int RosetteID { get; set; }
        public Status Status { get; set; } = Status.NotApproved;
    }
}

