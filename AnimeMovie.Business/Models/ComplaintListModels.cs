using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Models
{
    public class ComplaintListModels : ComplaintList
    {
        public Users ComplainantUser { get; set; }
        public Users Users { get; set; }
        public ComplaintListModels(ComplaintList complaint)
        {
            this.ID = complaint.ID;
            this.ComplainantID = complaint.ComplainantID;
            this.UserID = complaint.UserID;
            this.Description = complaint.Description;
            this.CreateTime = complaint.CreateTime;

        }
    }
}

