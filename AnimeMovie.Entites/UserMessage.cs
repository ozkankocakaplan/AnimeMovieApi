using System;
namespace AnimeMovie.Entites
{
    public class UserMessage : BaseEntity
    {
        public int ReceiverID { get; set; }
        public int SenderID { get; set; }
        public string Message { get; set; }
    }

}

