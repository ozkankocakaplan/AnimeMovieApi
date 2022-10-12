using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class UserListContents : BaseEntity
    {
        public int UserID { get; set; }
        public int ListID { get; set; }
        public int ContentID { get; set; }
        public int EpisodeID { get; set; }
        public Type Type { get; set; }

        [ForeignKey("ListID")]
        public virtual ICollection<UserList> UserList { get; set; }
    }
}

