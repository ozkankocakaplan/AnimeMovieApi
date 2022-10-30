using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class RosetteContent : BaseEntity
    {
        public int ContentID { get; set; }
        [ForeignKey("Rosette")]
        public int RosetteID { get; set; }
        public int EpisodesID { get; set; }
        public Type Type { get; set; }

        public virtual Rosette Rosette { get; set; }
    }
}

