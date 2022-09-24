using System;
namespace AnimeMovie.Entites
{
    public class CategoryType : BaseEntity
    {
        public int CategoryID { get; set; }
        public int ContentID { get; set; }
        public Type Type { get; set; }
    }
}

