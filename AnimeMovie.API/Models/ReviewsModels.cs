using System;
using AnimeMovie.Entites;

namespace AnimeMovie.API.Models
{
    public class ReviewsModels : Review
    {
        public Anime Anime { get; set; }
        public Manga Manga { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comments> Comments { get; set; }
        public ReviewsModels(Review review)
        {
            this.ID = review.ID;
            this.ContentID = review.ContentID;
            this.CreateTime = review.CreateTime;
            this.Message = review.Message;
            this.Type = review.Type;
            this.UserID = review.UserID;
            if (review.User != null)
            {
                this.User = review.User;
            }
        }
    }
}

