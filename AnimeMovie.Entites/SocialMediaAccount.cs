using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMovie.Entites
{
    public class SocialMediaAccount : BaseEntity
    {
        [ForeignKey("Users")]
        public int UserID { get; set; }
        public string GmailUrl { get; set; }
        public string InstagramUrl { get; set; }

        public virtual Users Users { get; set; }
    }
}

