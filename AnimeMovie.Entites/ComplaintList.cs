using System;
namespace AnimeMovie.Entites
{
    public class ComplaintList : BaseEntity
    {
        public int ComplainantID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
    }
}

