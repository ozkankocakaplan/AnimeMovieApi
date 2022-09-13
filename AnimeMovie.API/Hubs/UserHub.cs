using System;
using Microsoft.AspNetCore.SignalR;
using AnimeMovie.Entites;
namespace AnimeMovie.API.Hubs
{
    public class UserHub : Hub
    {
        static Dictionary<string, Users> userList = new Dictionary<string, Users>();

        public UserHub()
        {
        }

        public void connectHub(Users user)
        {
            var userCheck = userList.Where(x => x.Value.ID == user.ID).SingleOrDefault();
            if (userCheck.Value != null)
            {
                userList.Remove(userCheck.Key);
            }
            userList.Add(Context.ConnectionId, user);
        }
    }
}

