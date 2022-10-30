using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class CategoryType : BaseEntity
    {
        [ForeignKey("Categories")]
        public int CategoryID { get; set; }
        public int ContentID { get; set; }
        public Type Type { get; set; }

        public Categories Categories { get; set; }
    }
}

