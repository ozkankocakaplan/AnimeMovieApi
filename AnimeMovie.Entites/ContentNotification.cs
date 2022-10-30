using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class ContentNotification : BaseEntity
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        public int ContentID { get; set; }
        public Type Type { get; set; }

        public virtual Users User { get; set; }
    }
}

