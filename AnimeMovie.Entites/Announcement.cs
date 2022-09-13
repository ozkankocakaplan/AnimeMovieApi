using System;
namespace AnimeMovie.Entites
{
    public class Announcement : BaseEntity
    {
        public string UpdateInformation { get; set; }
        public DateTime UpdateDate { get; set; }
        public string InnovationInformation { get; set; }
        public DateTime InnovationDate { get; set; }
        public string ComplaintsInformation { get; set; }
        public DateTime ComplaintsDate { get; set; }
        public string AddToInformation { get; set; }
        public DateTime AddDate { get; set; }
        public string WarningInformation { get; set; }
        public DateTime WarningDate { get; set; }
        public string ComingSoonInfo { get; set; }
        public DateTime ComingSoonDate { get; set; }
    }
}

