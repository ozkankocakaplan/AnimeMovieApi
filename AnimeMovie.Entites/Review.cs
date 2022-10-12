using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class Review : BaseEntity
    {
        public int ContentID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public Type Type { get; set; }

        [ForeignKey("UserID")]
        public virtual ICollection<Users> User { get; set; }
    }
}

