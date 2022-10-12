using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class UserRosette : BaseEntity
    {
        public int UserID { get; set; }
        [ForeignKey("Rosette")]
        public int RosetteID { get; set; }
        public Status Status { get; set; } = Status.NotApproved;
        public Rosette Rosette { get; set; }
    }
}

