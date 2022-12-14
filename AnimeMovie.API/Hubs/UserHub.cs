using System;
using Microsoft.AspNetCore.SignalR;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Authorization;
using AnimeMovie.Business.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class HubUserModel : Users
{
    public string connectionID { get; set; }
    public HubUserModel(Users users)
    {
        this.ID = users.ID;
        this.Image = users.Image;
        this.SeoUrl = users.SeoUrl;
        this.UserName = users.UserName;
        this.NameSurname = users.NameSurname;
    }
}
namespace AnimeMovie.API.Hubs
{
    [Authorize]
    public class UserHub : Hub
    {
        static List<HubUserModel> userList = new List<HubUserModel>();

        private readonly IUsersService userService;
        private readonly IUserMessageService userMessageService;
        public UserHub(IUsersService users, IUserMessageService userMessage)
        {
            userMessageService = userMessage;
            userService = users;
        }
        public override Task OnConnectedAsync()
        {
            var userID = Handler.UserID(Context.GetHttpContext());
            var user = userService.get(x => x.ID == userID).Entity;
            if (user != null)
            {
                HubUserModel userModel = new HubUserModel(user);
                userModel.connectionID = Context.ConnectionId;
                userList.Add(userModel);
                Clients.Client(Context.ConnectionId).SendAsync("getID", Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }
        public async Task sendUserMessage(UserMessage userMessage)
        {

            var getUsers = userList.Where((y) => y.ID == userMessage.ReceiverID);
            var message = userMessageService.add(userMessage).Entity;
            if (getUsers != null && getUsers.Count() != 0)
            {
                foreach (var user in getUsers)
                {
                    await Clients.Client(user.connectionID).SendAsync("messageSent", message);
                }

            }
            //await Clients.Client().SendAsync("messageSent",message);

        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var checkUser = userList.Where(x => x.connectionID == Context.ConnectionId).SingleOrDefault();
                if (checkUser != null)
                {
                    userList.Remove(checkUser);
                }
            }
            catch (Exception ex)
            {

            }

            return base.OnDisconnectedAsync(exception);
        }
    }
    public class User : Hub
    {
        public static List<string> onlineUsers = new List<string>();
        public override Task OnConnectedAsync()
        {
            var context = Context.GetHttpContext();
            var ip = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (!onlineUsers.Any(x => x == ip))
            {
                onlineUsers.Add(ip);
            }
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var context = Context.GetHttpContext();
                var ip = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
                if (onlineUsers.Any(x => x == ip))
                {
                    onlineUsers.Remove(ip);
                }
            }
            catch (Exception ex)
            {

            }
            
            return base.OnDisconnectedAsync(exception);
        }
    }

}


