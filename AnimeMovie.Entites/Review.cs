using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class Review : BaseEntity
    {
        public int ContentID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public string Message { get; set; }
        public Type Type { get; set; }


        public virtual Users User { get; set; }
    }
}

