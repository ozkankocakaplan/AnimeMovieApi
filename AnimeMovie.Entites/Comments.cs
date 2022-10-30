using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class Comments : BaseEntity
    {
        [ForeignKey("Users")]
        public int UserID { get; set; }
        public int ContentID { get; set; }
        public string Comment { get; set; }
        public bool isSpoiler { get; set; } = false;
        public Type Type { get; set; }

        public virtual Users Users { get; set; }
    }
}

