using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public interface IBaseEntity
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class BaseEntity : IBaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}

