using System;
namespace AnimeMovie.Entites
{
    public class ContentComplaint : BaseEntity
    {
        public int UserID { get; set; }
        public int ContentID { get; set; }
        public string Message { get; set; }
        public Type type { get; set; }
        public ComplaintType ComplaintType { get; set; }
    }
}

