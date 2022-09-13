using System;
namespace AnimeMovie.Entites
{
    public class ContentComplaint : BaseEntity
    {
        public int UserID { get; set; }
        public string Message { get; set; }
        public ComplaintType ComplaintType { get; set; }
    }
}

