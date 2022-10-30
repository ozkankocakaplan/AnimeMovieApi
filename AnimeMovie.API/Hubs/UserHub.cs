using System;
using Microsoft.AspNetCore.SignalR;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Authorization;
using AnimeMovie.Business.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AnimeMovie.API.Hubs
{
    [Authorize]
    public class UserHub : Hub
    {
        static Dictionary<string, Users> userList = new Dictionary<string, Users>();
       
        private readonly IUsersService userService;
        private readonly IUserMessageService userMessageService;
        public UserHub(IUsersService users,IUserMessageService userMessage)
        {
            userMessageService = userMessage;
            userService = users;
        }

        public void connect()
        {
            var userID = Handler.UserID(Context.GetHttpContext());
            var user = userService.get(x => x.ID == userID).Entity;
            if (user != null)
            {
                var checkUser = userList.Where(x => x.Value.ID == userID).FirstOrDefault();
                if(checkUser.Key != null)
                {
                    userList.Remove(checkUser.Key);
                }
              
                userList.Add(Context.ConnectionId, user);
            }
        }
        public async Task sendUserMessage(UserMessage userMessage)
        {
 
            var getUser = userList.Where((y) => y.Value.ID == userMessage.ReceiverID).FirstOrDefault();
            var message = userMessageService.add(userMessage).Entity;
            //if (getUser.Value != null)
            //{
            //    foreach (var item in userList.Where(y=>y.Value.ID != userMessage.SenderID))
            //    {
            //        await Clients.Client(getUser.Key).SendAsync("messageSent", message);
            //    }

            //}
            //foreach (var item in userList)
            //{
            //    await Clients.Client(item.Key).SendAsync("messageSent", message);
            //}
            await Clients.All.SendAsync("messageSent",message);

        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var checkUser = userList.Where(x => x.Key == Context.ConnectionId).SingleOrDefault();
            if (checkUser.Key != null)
            {
                userList.Remove(checkUser.Key);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}

