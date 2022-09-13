using System;
namespace AnimeMovie.Entites
{
    public class UserBlockList : BaseEntity
    {
        public int UserID { get; set; }
        public int BlockID { get; set; }
    }
}

