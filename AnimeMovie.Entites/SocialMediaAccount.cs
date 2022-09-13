using System;
namespace AnimeMovie.Entites
{
    public class SocialMediaAccount : BaseEntity
    {
        public int UserID { get; set; }
        public string GmailUrl { get; set; }
        public string InstagramUrl { get; set; }
    }
}

