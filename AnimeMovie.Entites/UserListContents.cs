using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class UserListContents : BaseEntity
    {
        [ForeignKey("Users")]
        public int UserID { get; set; }
        [ForeignKey("UserList")]
        public int ListID { get; set; }
        public int ContentID { get; set; }
        public int EpisodeID { get; set; }
        public Type Type { get; set; }

        public virtual UserList UserList { get; set; }
        public virtual Users Users { get; set; }
    }
}

