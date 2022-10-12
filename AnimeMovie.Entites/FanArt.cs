using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class FanArt : BaseEntity
    {
        [ForeignKey("Users")]
        public int UserID { get; set; }
        public int ContentID { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }

        public Users Users { get; set; }
    }
}

