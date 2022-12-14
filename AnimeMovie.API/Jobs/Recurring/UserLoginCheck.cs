using System;
using AnimeMovie.Business.Abstract;
using Hangfire;

namespace AnimeMovie.API.Jobs.Recurring
{
    public class UserLoginCheck
    {
        private IUserLoginHistoryService userLoginHistoryService;
        private IUsersService usersService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserLoginCheck(IUserLoginHistoryService userLoginHistory,
            IUsersService users,
            IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
            usersService = users;
            userLoginHistoryService = userLoginHistory;
        }
        public void Run(IJobCancellationToken jobCancallationToken)
        {
            jobCancallationToken.ThrowIfCancellationRequested();
            runAtTimeOf(DateTime.Now);
        }
        public void runAtTimeOf(DateTime date)
        {
            DateTime nowDate = DateTime.Now;
            var userLogins = userLoginHistoryService.getList().List.ToList();
            foreach (var userLogin in userLogins)
            {
                TimeSpan ts = userLogin.LastSeen - nowDate;
                if(ts.Days > 31)
                {
                    var user = usersService.get(x => x.ID == userLogin.UserID).Entity;
                    if(user != null)
                    {
                        Handler.SendEmail(webHostEnvironment, user.Email, $"Merhaba {user.NameSurname} ", "Hesabınıza uzun süredir erişim sağlamadığınız için kapatılmıştır.", "Hesabınız Kapatıldı");
                        usersService.updateUserBanned(user.ID);
                    }
                }
            }
        }
    }
}

