using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Xml.Linq;
using AnimeMovie.API.Models;
using AnimeMovie.Business;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Business.Models;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Type = AnimeMovie.Entites.Type;

namespace AnimeMovie.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IRatingsService ratingsService;
        private readonly IAnimeListService animeListService;
        private readonly IMangaListService mangaListService;
        private readonly IUserEmailVertificationService userEmailVertificationService;
        private readonly IUserForgotPasswordService userForgotPasswordService;
        private readonly IUserBlockListService userBlockListService;
        private readonly IReviewService reviewService;
        private readonly ICommentsService commentsService;
        private readonly INotificationService notificationService;
        private readonly ILikeService likeService;
        private readonly IFanArtService fanArtService;
        private readonly IUserMessageService userMessageService;
        private readonly IUserLoginHistoryService userLoginHistoryService;
        private readonly IFavoriteService favoriteService;
        private readonly IRosetteContentService rosetteContentService;
        private readonly IUserRosetteService userRosetteService;
        private readonly IUserListService userListService;
        private readonly IUserListContentsService userListContentsService;
        private readonly IAnimeService animeService;
        private readonly IMangaService mangaService;
        private readonly ICategoryTypeService categoryTypeService;

        private readonly IMovieTheWeekService movieTheWeekService;

        private readonly IAnimeSeasonService animeSeasonService;
        private readonly IAnimeEpisodesService animeEpisodesService;
        private readonly IMangaEpisodesService mangaEpisodesService;

        private readonly IContentNotificationService contentNotificationService;

        public UserController(IWebHostEnvironment webHost, IUsersService users, IRatingsService ratings,
            IAnimeListService animeList, IMovieTheWeekService movieTheWeek,
            IUserEmailVertificationService userEmailVertification,
            IUserForgotPasswordService userForgotPassword, IUserBlockListService userBlockList,
            IReviewService review, ICommentsService comments, INotificationService notification, IMangaListService mangaList,
            ILikeService like, IFanArtService fanArt, IUserMessageService userMessage, IUserLoginHistoryService userLoginHistory,
            IFavoriteService favorite, IRosetteContentService rosetteContent, IUserRosetteService userRosette,
            IUserListService userList, IAnimeSeasonService animeSeason, IAnimeEpisodesService animeEpisodes, IMangaEpisodesService mangaEpisodes,
            IUserListContentsService userListContents, IAnimeService anime, IMangaService manga, ICategoryTypeService categoryType, IContentNotificationService contentNotification)
        {
            movieTheWeekService = movieTheWeek;
            contentNotificationService = contentNotification;
            animeSeasonService = animeSeason;
            animeEpisodesService = animeEpisodes;
            mangaEpisodesService = mangaEpisodes;
            categoryTypeService = categoryType;
            animeService = anime;
            mangaService = manga;
            userListService = userList;
            userListContentsService = userListContents;
            userRosetteService = userRosette;
            rosetteContentService = rosetteContent;
            webHostEnvironment = webHost;
            usersService = users;
            ratingsService = ratings;
            animeListService = animeList;
            userEmailVertificationService = userEmailVertification;
            userForgotPasswordService = userForgotPassword;
            userBlockListService = userBlockList;
            commentsService = comments;
            reviewService = review;
            notificationService = notification;
            mangaListService = mangaList;
            likeService = like;
            fanArtService = fanArt;
            userMessageService = userMessage;
            userLoginHistoryService = userLoginHistory;
            favoriteService = favorite;
        }

        #region Users
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getMe")]
        [HttpGet]
        public IActionResult getMe()
        {
            var id = Handler.UserID(HttpContext);
            var response = usersService.get(x => x.ID == id);
            return Ok(response);
        }
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getPaginatedUsers/{pageNo}/{showCount}")]
        [HttpGet]
        public IActionResult getUsers(int pageNo, int showCount)
        {
            var response = usersService.getPaginatedUsers(pageNo, showCount);
            return Ok(response);
        }
        [AllowAnonymous]
        [Route("/login/{userName}/{password}")]
        [HttpPost]
        public IActionResult login(string userName, string password)
        {
            var response = usersService.login(userName, password);
            if (response.Entity != null)
            {
                var userHistory = userLoginHistoryService.get(x => x.UserID == response.Entity.ID);
                if (userHistory.Entity != null)
                {
                    userHistory.Entity.LastSeen = DateTime.Now;
                    response.Entity.UserLoginHistory = userLoginHistoryService.update(userHistory.Entity).Entity;
                }
                else
                {
                    response.Entity.UserLoginHistory = userLoginHistoryService.add(new UserLoginHistory()
                    {
                        UserID = response.Entity.ID,
                        LastSeen = DateTime.Now
                    }).Entity;
                }

            }
            return Ok(response);
        }
        [AllowAnonymous]
        [Route("/addUser/{vertificationCode}")]
        [HttpPost]
        public IActionResult addUser([FromBody] Users user, string vertificationCode)
        {
            user.isBanned = false;
            user.RoleType = RoleType.User;
            var response = usersService.addUser(user, vertificationCode);
            return Ok(response);
        }
        [Roles(Roles = RolesAttribute.All)]
        [Route("/updatePassword/{currentPassword}/{newPassword}")]
        [HttpPut]
        public IActionResult updatePassword(string currentPassword, string newPassword)
        {
            if (currentPassword.Length != 0 && newPassword.Length != 0)
            {
                var response = usersService.updatePassword(currentPassword, newPassword, Handler.UserID(HttpContext));
                return Ok(response);
            }
            return BadRequest();
        }
        [Roles(Roles = RolesAttribute.All)]
        [Route("/updateUserInfo/{nameSurname}/{userName}/{about}")]
        [HttpPut]
        public IActionResult updateUserInfo(string nameSurname, string userName, string about)
        {
            if (nameSurname.Length != 0 && userName.Length != 0)
            {
                var response = usersService.updateUserInfo(nameSurname, userName, about, Handler.UserID(HttpContext));
                return Ok(response);
            }
            return BadRequest();
        }
        [Roles(Roles = RolesAttribute.All)]
        [Route("/updateRole/{role}")]
        [HttpPut]
        public IActionResult updateRole(RoleType role)
        {
            var response = usersService.updateRole(role, Handler.UserID(HttpContext));
            return Ok(response);
        }
        [Roles(Roles = RolesAttribute.All)]
        [Route("/updateEmailChange/{email}/{code}")]
        [HttpPut]
        public IActionResult updateEmailChange(string email, string code)
        {
            if (email.Length != 0 && code.Length != 0)
            {
                var checkCode = userEmailVertificationService.get(x => x.Code == code && x.Email == email);
                if (checkCode.Entity != null)
                {
                    var response = usersService.updateEmail(email, Handler.UserID(HttpContext));
                    return Ok(response);
                }
                else
                {
                    var response = new ServiceResponse<Users>();
                    response.IsSuccessful = false;
                    response.HasExceptionError = true;
                    response.ExceptionMessage = "Doğrulama kodu yanlış";
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        [Roles(Roles = RolesAttribute.All)]
        [Route("/updateImage")]
        [HttpPut]
        public IActionResult updateImage([FromForm] IFormFile file)
        {
            if (file != null && file.Length != 0)
            {
                var guid = Guid.NewGuid().ToString();
                string patch = webHostEnvironment.WebRootPath + "/user/";
                using (FileStream fs = System.IO.File.Create(patch + guid + file.FileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                    var response = usersService.updateImage("/user/" + guid + file.FileName, Handler.UserID(HttpContext));
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        [Roles(Roles = RolesAttribute.User)]
        [Route("/updateUserName/{username}")]
        [HttpPut]
        public IActionResult updateUserName(string userName)
        {
            if (userName.Length != 0)
            {
                var response = usersService.updateUserName(userName, Handler.UserID(HttpContext));
                return Ok(response);
            }
            return BadRequest();
        }
        [Roles(Roles = RolesAttribute.Admin)]
        [Route("/updateIsBanned/{userID}")]
        [HttpPut]
        public IActionResult updateIsBanned(int userID)
        {
            var response = usersService.updateUserBanned(userID);
            return Ok(response);
        }
        [Roles(Roles = RolesAttribute.All)]
        [Route("/deleteUser/{userID}")]
        [HttpPut]
        public IActionResult deleteUser(int userID)
        {
            var response = usersService.delete(x => x.ID == userID);
            return Ok(response);
        }

        [Route("/getUserByID/{userID}")]
        [HttpGet]
        public IActionResult getUserByID(int userID)
        {
            var response = usersService.get(x => x.ID == userID);
            if (response.Entity != null)
            {
                var userResponse = new ServiceResponse<UserModel>();
                UserModel userModel = new UserModel(response.Entity);
                userModel.UserLoginHistory = userLoginHistoryService.get(x => x.UserID == response.Entity.ID).Entity;
                userResponse.Entity = userModel;
                userResponse.IsSuccessful = true;
                return Ok(userResponse);
            }
            return Ok(response);
        }
        [Route("/getUserBySeoUrl/{seoUrl}")]
        [HttpGet]
        public IActionResult getUserByID(string seoUrl)
        {
            var response = usersService.get(x => x.SeoUrl == seoUrl);
            if (response.Entity != null)
            {
                var userResponse = new ServiceResponse<UserFullModels>();
                UserFullModels userModels = new UserFullModels();
                var getUser = usersService.get(x => x.SeoUrl == seoUrl).Entity;
                if (getUser != null)
                {
                    List<AnimeListModels> animeLists = new List<AnimeListModels>();
                    List<MangaListModels> mangaLists = new List<MangaListModels>();
                    userModels.User = getUser;
                    userModels.Rosettes = userRosetteService.getList(x => x.UserID == getUser.ID).List.ToList();

                    var animeList = animeListService.getList(x => x.UserID == getUser.ID).List.ToList();
                    userModels.AnimeLists = animeList.DistinctBy(x => x.AnimeID).ToList();

                    foreach (var item in animeList)
                    {
                        AnimeListModels anime = new AnimeListModels();
                        anime.Anime = item.Anime;
                        anime.AnimeList = item;
                        anime.AnimeSeasons = animeSeasonService.getList(x => x.AnimeID == item.AnimeID).List.ToList();
                        anime.Categories = categoryTypeService.getList(x => x.ContentID == item.AnimeID && x.Type == Type.Anime).List.ToList();
                        var episodes = animeList.Where(x => x.AnimeID == item.AnimeID).DistinctBy(x => x.EpisodeID).ToList();
                        anime.AnimeEpisodes = episodes.Select(x => x.AnimeEpisode).ToList();
                        animeLists.Add(anime);
                    }
                    userModels.AnimeListModels = animeLists;

                    var mangaList = mangaListService.getList(x => x.UserID == getUser.ID).List;
                    userModels.MangaLists = mangaList.ToList();
                    foreach (var item in mangaList.ToList())
                    {
                        MangaListModels manga = new MangaListModels();
                        manga.Manga = item.Manga;
                        manga.MangaList = item;
                        manga.Categories = categoryTypeService.getList(x => x.ContentID == item.MangaID && x.Type == Type.Manga).List.ToList();
                        var episodes = mangaList.Where(x => x.MangaID == item.MangaID).DistinctBy(x => x.EpisodeID).ToList();
                        manga.MangaEpisodes = episodes.Select(x => x.MangaEpisode).ToList();
                        mangaLists.Add(manga);
                    }
                    userModels.MangaListModels = mangaLists;

                    var fanArts = fanArtService.getList(x => x.UserID == getUser.ID).List.ToList();
                    List<FanArtModel> fanArtModels = new List<FanArtModel>();
                    foreach (var fanArt in fanArts)
                    {
                        FanArtModel fanArtModel = new FanArtModel(fanArt);
                        if (fanArt.Type == Type.Anime)
                        {
                            fanArtModel.Anime = animeService.get(x => x.ID == fanArt.ContentID).Entity;
                            fanArtModel.Likes = likeService.getList(x => x.ContentID == fanArt.ContentID && x.Type == Type.FanArt).List.ToList();
                        }
                        if (fanArt.Type == Type.Manga)
                        {
                            fanArtModel.Manga = mangaService.get(x => x.ID == fanArt.ContentID).Entity;
                            fanArtModel.Likes = likeService.getList(x => x.ContentID == fanArt.ContentID && x.Type == Type.FanArt).List.ToList();
                        }
                       
                        fanArtModels.Add(fanArtModel);
                    }
                    userModels.FanArts = fanArtModels;
                    var reviews = reviewService.getList(x => x.UserID == getUser.ID).List.ToList();
                    List<ReviewsModels> reviewsModels = new List<ReviewsModels>();
                    foreach (var review in reviews)
                    {
                        ReviewsModels models = new ReviewsModels(review);
                       
                        if (review.Type == Type.Manga)
                        {
                            models.Manga = mangaService.get(x => x.ID == review.ContentID).Entity;
                            models.Likes = likeService.getList(x => x.Type == Type.Reviews && x.ContentID == review.ContentID).List.ToList();
                        }
                        if (review.Type == Type.Anime)
                        {
                            models.Anime = animeService.get(x => x.ID == review.ContentID).Entity;
                            models.Likes = likeService.getList(x => x.Type == Type.Reviews && x.ContentID == review.ContentID).List.ToList();
                        }
                        reviewsModels.Add(models);
                    }
                    userModels.Reviews = reviewsModels;
                    userModels.UserListModels = userListContentsService.getUserListModels(x => x.UserID == getUser.ID).List.ToList();
                    userModels.UserLists = userListService.getList(x => x.UserID == getUser.ID).List.ToList();
                    userModels.UserListContents = userListContentsService.getList().List.ToList();
                    userResponse.Entity = userModels;
                    userResponse.IsSuccessful = true;
                    return Ok(userResponse);
                }

            }
            return BadRequest();
        }
        #endregion
        #region UserEmailVertification
        [HttpPost]
        [Route("/addUserEmailVertification")]
        public IActionResult addUserEmailVertification([FromBody] UserEmailVertification userEmail)
        {
            if (userEmail.Email.Length != 0)
            {
                var response = userEmailVertificationService.add(userEmail);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("/againUserEmailVertification/{email}")]
        public IActionResult againUserEmailVertification(string email)
        {
            if (email.Length != 0)
            {
                var emailCheck = usersService.get(x => x.Email == email);
                if (emailCheck.Entity == null)
                {
                    var getUserEmail = userEmailVertificationService.delete(x => x.Email == email);
                    var code = Handler.createData();
                    Handler.SendEmail(webHostEnvironment, email, "Merhaba,", "Doğrulama kodunuz: " + code, "Doğrulama Kodu");
                    var response = userEmailVertificationService.add(new UserEmailVertification() { Email = email, Code = code });
                    response.Entity.Code = "";
                    return Ok(response);
                }
                else
                {
                    var resp = new ServiceResponse<Users>();
                    resp.HasExceptionError = true;
                    resp.ExceptionMessage = "E-posta Kullanılıyor";
                    return Ok(resp);
                }

            }
            return BadRequest();
        }
        #endregion
        #region Ratings
        [HttpPost]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/addRating")]
        public IActionResult addRating([FromBody] Ratings animeRating)
        {
            var id = Handler.UserID(HttpContext);
            if (animeRating.AnimeID != 0 && id == animeRating.UserID)
            {
                var response = ratingsService.add(animeRating);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/updateRating")]
        public IActionResult updateAnimeRating([FromBody] Ratings animeRating)
        {
            var id = Handler.UserID(HttpContext);
            if (animeRating.AnimeID != 0 && animeRating.ID != 0 && animeRating.UserID == id)
            {
                var response = ratingsService.update(animeRating);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/deleteRatings/{id}")]
        public IActionResult deleteRatings(int id)
        {
            var getAnimeRating = ratingsService.get(x => x.ID == id && x.UserID == Handler.UserID(HttpContext));
            if (getAnimeRating.Entity != null)
            {
                var response = ratingsService.delete(x => x.ID == id);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getRatings/{animeID}")]
        public IActionResult getRatings(int animeID)
        {
            var response = ratingsService.getList(x => x.AnimeID == animeID);
            return Ok(response);
        }
        #endregion

        #region UserForgotPassword
        [HttpPost]
        [Route("/addUserForgotPassword/{email}")]
        public IActionResult addUserForgotPassword(string email)
        {
            if (email.Length != 0)
            {
                var getEmail = usersService.get(x => x.Email == email).Entity;
                var checkForgot = userForgotPasswordService.get(x => x.UserID == getEmail.ID).Entity;
                if (getEmail != null && checkForgot == null)
                {
                    string guid = Guid.NewGuid().ToString();
                    var createResetLink = userForgotPasswordService.add(new UserForgotPassword() { UserID = getEmail.ID, ResetLink = guid });
                    Handler.SendEmail(webHostEnvironment, email, $"Merhaba {getEmail.NameSurname} , ", "Bu işlem sana aitse aşağıda ki bağlantıya tıklayınız. Yeni şifreniz epostaya gönderilecek.<br/><br/> <a href='http://lycorisa.com/resetPassword/" + guid + "'>Yeni Şifremi Gönder</a>", "Şifremi unuttum");
                    var response = new ServiceResponse<UserForgotPassword>();
                    response.IsSuccessful = true;
                    return Ok(response);
                }

            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getUserForgotPassword/{ResetLink}")]
        public IActionResult getUserForgotPassword(string ResetLink)
        {
            var checkLink = userForgotPasswordService.get(x => x.ResetLink == ResetLink).Entity;
            if (checkLink != null)
            {
                var getUser = usersService.get(x => x.ID == checkLink.UserID).Entity;
                var createPassword = Handler.createData() + "&" + Handler.createData();
                usersService.updatePassword(getUser.Password, createPassword, getUser.ID);
                var response = new ServiceResponse<UserForgotPassword>();
                response.IsSuccessful = true;
                userForgotPasswordService.delete(x => x.UserID == getUser.ID);
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion
        #region UserBlockList
        [HttpPost]
        [Route("/addUserBlockList")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addUserBlockList([FromBody] UserBlockList user)
        {
            var userID = Handler.UserID(HttpContext);
            if (user.BlockID != 0 && userID == user.UserID)
            {
                user.isBlocked = true;
                var response = userBlockListService.add(user);
                userBlockListService.add(new UserBlockList()
                {
                    BlockID = user.UserID,
                    UserID = user.BlockID,
                    isBlocked = false,
                });
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteUserBlockList/{blockID}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteUserBlockList(int blockID)
        {
            var userID = Handler.UserID(HttpContext);
            if (blockID != 0)
            {
                var response = userBlockListService.delete(x => x.BlockID == blockID && x.UserID == userID);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getUserBlockList/{userID}/{blockID}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getUserBlockList(int blockID, int userID)
        {
            var response = userBlockListService.getList(x => x.UserID == blockID || x.BlockID == userID);
            return Ok(response);
        }
        #endregion
        #region Comments
        [HttpPost]
        [Route("/addComment")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addComment([FromBody] Comments comment)
        {
            var userID = Handler.UserID(HttpContext);
            if (comment.ContentID != 0 && comment.Comment.Length != 0 && comment.UserID == userID)
            {
                var response = commentsService.add(comment);
                var comm = response.Entity;
                comm.Users = usersService.get((x) => x.ID == comment.UserID).Entity;
                response.Entity = comm;
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteComment/{commentID}/{type}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteComment(int commentID, int type)
        {
            var userID = Handler.UserID(HttpContext);
            var response = commentsService.delete(x => x.ID == commentID && x.UserID == userID && x.Type == (Type)type);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getPaginatedComments/{contentID}/{pageNo}/{showCount}")]
        public IActionResult getPaginatedComments(int contentID, int type, int pageNo = 1, int showCount = 10)
        {
            var response = commentsService.getPaginatedComments(x => x.ContentID == contentID && x.Type == (Type)type, pageNo, showCount);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getComments/{contentID}/{type}")]
        public IActionResult getComments(int contentID, int type)
        {
            var response = commentsService.getList();
            response.List = response.List.Where(x => x.ContentID == contentID && x.Type == (Type)type).OrderByDescending(x => x.ID).ToList();
            return Ok(response);
        }
        #endregion
        #region Reviews
        [HttpPost]
        [Route("/addReviews")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addReview([FromBody] Review review)
        {
            var userID = Handler.UserID(HttpContext);
            if (review.UserID == userID && review.ContentID != 0 && review.Message.Length != 0)
            {
                var response = reviewService.add(review);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("/updateReviews")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult updateReviews([FromBody] Review review)
        {
            var userID = Handler.UserID(HttpContext);
            if (review.UserID == userID && review.ContentID != 0 && review.Message.Length != 0)
            {
                var response = reviewService.update(review);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteReview/{reviewID}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteReview(int reviewID)
        {
            var userID = Handler.UserID(HttpContext);
            var response = reviewService.delete(x => x.ID == reviewID && x.UserID == userID);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getReviews/{contentID}/{type}/{pageNo}/{showCount}")]
        public IActionResult getReviews(int contentID, int type, int pageNo = 1, int showCount = 10)
        {
            var response = reviewService.getPaginatedReviews(x => x.ContentID == contentID && x.Type == (Type)type, pageNo, showCount);
            return Ok(response);
        }
        #endregion
        #region Notifications
        [HttpGet]
        [Route("/getNotifications")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getNotifications()
        {
            var userID = Handler.UserID(HttpContext);
            var response = notificationService.getList(x => x.UserID == userID);
            return Ok(response);
        }
        [HttpPut]
        [Route("/updateNotification/{notificationID}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult updateNotification(int notificationID)
        {
            var userID = Handler.UserID(HttpContext);
            var getNotification = notificationService.get(x => x.ID == notificationID && x.isReadInfo == false);
            if (getNotification.Entity != null && getNotification.Entity.UserID == userID)
            {
                var notification = getNotification.Entity;
                notification.isReadInfo = true;
                var response = notificationService.update(notification);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("/addNotification")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addNotification([FromBody] Notification notification)
        {
            if (notification.NotificationMessage.Length != 0 && notification.NotificationType != 0)
            {
                var response = notificationService.add(notification);
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion

        #region AnimeList
        [HttpPost]
        [Route("/addAnimeLists")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addAnimeLists([FromBody] List<AnimeList> animeList)
        {
            var id = Handler.UserID(HttpContext);
            foreach (var item in animeList)
            {
                if (id == item.UserID)
                {
                    var check = animeListService.get(x => x.EpisodeID == item.EpisodeID && x.AnimeID == item.AnimeID).Entity;
                    if (check == null)
                    {
                        animeListService.add(item);
                    }
                }
            }

            return Ok(animeList);
        }
        [HttpPost]
        [Route("/addAnimeList")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addAnimeList([FromBody] AnimeList animeList)
        {
            var id = Handler.UserID(HttpContext);
            if (animeList.UserID == id)
            {
                var check = animeListService.get(x => x.EpisodeID == animeList.EpisodeID && x.AnimeID == animeList.AnimeID).Entity;
                if (check == null)
                {
                    var response = animeListService.add(animeList);
                    return Ok(response);
                }
                else
                {
                    animeListService.delete(x => x.EpisodeID == animeList.EpisodeID && x.AnimeID == animeList.AnimeID);
                    var response = animeListService.add(animeList);
                    return Ok(response);
                }

            }
            return BadRequest(animeList);
        }
        [HttpDelete]
        [Route("/deleteAnimeList/{id}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteAnimeList(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var response = animeListService.delete(x => x.UserID == userID && x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeList")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getAnimeList()
        {
            var userID = Handler.UserID(HttpContext);
            var response = animeListService.getList(x => x.UserID == userID);
            return Ok(response);
        }
        #endregion
        #region MangaList
        [HttpPost]
        [Route("/addMangaLists")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addMangaLists([FromBody] List<MangaList> mangaList)
        {
            var id = Handler.UserID(HttpContext);
            foreach (var item in mangaList)
            {
                if (id == item.UserID)
                {
                    var check = mangaListService.get(x => x.EpisodeID == item.EpisodeID && x.MangaID == item.MangaID).Entity;
                    if (check == null)
                    {
                        mangaListService.add(item);
                    }
                }
            }

            return Ok(mangaList);
        }
        [HttpPost]
        [Route("/addMangaList")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addMangaList([FromBody] MangaList mangaList)
        {
            var id = Handler.UserID(HttpContext);
            if (id == mangaList.UserID)
            {
                var check = mangaListService.get(x => x.EpisodeID == mangaList.EpisodeID && x.MangaID == mangaList.MangaID).Entity;
                if (check == null)
                {
                    var response = mangaListService.add(mangaList);
                    return Ok(response);
                }
            }

            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteMangaList/{id}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteMangaList(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var response = mangaListService.delete(x => x.UserID == userID && x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getMangaList")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getMangaList()
        {
            var userID = Handler.UserID(HttpContext);
            var response = mangaListService.getList(x => x.UserID == userID);
            return Ok(response);
        }
        #endregion

        #region Like
        [HttpPost]
        [Route("/addLike")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addLike([FromBody] Like like)
        {
            var userID = Handler.UserID(HttpContext);
            if (like.ContentID != 0 && like.UserID == userID)
            {
                var get = likeService.get(x => x.ContentID == like.ContentID && x.UserID == userID && x.Type == like.Type).Entity;
                if (get == null)
                {
                    var response = likeService.add(like);
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getLike/{contentID}/{type}")]
        public IActionResult getLikes(int contentID, int type)
        {
            var response = likeService.get(x => x.ContentID == contentID && x.Type == (Type)type);
            return Ok(response);
        }
        [HttpDelete]
        [Route("/deleteLike/{contentID}/{type}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteLike(int contentID, int type)
        {
            var userID = Handler.UserID(HttpContext);
            if (contentID != 0)
            {
                var response = likeService.delete(x => x.ContentID == contentID && x.UserID == userID && x.Type == (Type)type);
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion

        #region FanArt
        [Route("/addFanArt")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [HttpPost]
        public IActionResult addFanArt( [FromQuery] FanArt fanArt)
        {

            var response = fanArtService.add(fanArt);
            return Ok(response);
        }
        [Roles(Roles = RolesAttribute.All)]
        [Route("/deleteFanArt/{id}")]
        [HttpDelete]
        public IActionResult deleteFanArt(int id)
        {
            var role = Handler.RolType(HttpContext);
            if (role == "Admin" || role == "Moderator")
            {
                var response = fanArtService.delete(x => x.ID == id);
                return Ok(response);
            }
            else
            {
                var userID = Handler.UserID(HttpContext);
                var response = fanArtService.delete(x => x.ID == id && x.UserID == userID);
                return Ok(response);
            }
        }
        [Route("/getPaginatedFanArt/{animeID}/{pageNo}/{showCount}")]
        [HttpGet]
        public IActionResult getPaginatedFanArt(int animeID, int pageNo = 1, int showCount = 10)
        {
            var response = fanArtService.getPaginatedFanArt(x => x.ContentID == animeID, pageNo, showCount);
            return Ok(response);
        }
        [Route("/getPaginatedFanArtNoType/{pageNo}/{showCount}")]
        [HttpGet]
        public IActionResult getPaginatedFanArtNoType(int pageNo = 1, int showCount = 10)
        {
            var response = fanArtService.getPaginatedFanArt(x => true, pageNo, showCount);
            return Ok(response);
        }
        [Route("/getPaginatedReviewsNoType/{pageNo}/{showCount}")]
        [HttpGet]
        public IActionResult getPaginatedReviewsNoType(int pageNo = 1, int showCount = 10)
        {
            var lists = reviewService.getPaginatedReviews(x => true, pageNo, showCount);
            List<ReviewsModels> reviews = new List<ReviewsModels>();
            var response = new ServiceResponse<ReviewsModels>();
            if(lists.Count != 0)
            {
                foreach (var review in lists.List)
                {
                    ReviewsModels model = new ReviewsModels(review);
                    if(model.Type == Type.Anime)
                    {
                        model.Anime = animeService.get(x => x.ID == review.ContentID).Entity;
                    }
                    if (model.Type == Type.Manga)
                    {
                        model.Manga = mangaService.get(x => x.ID == review.ContentID).Entity;
                    }
                    model.User = usersService.get(x => x.ID == review.UserID).Entity;
                    reviews.Add(model);
                }
            }
            response.List = reviews;
            response.Count = reviews.Count;
            response.IsSuccessful = true;
            return Ok(response);
        }
        [Route("/getFanArts")]
        [HttpGet]
        public IActionResult getFanArts()
        {
            var response = fanArtService.getList();
            return Ok(response);
        }
        #endregion

        #region Message
        [HttpGet]
        [Route("/getSearchUserByID/{id}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getSearchUserByID(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var getUser = usersService.get(x => x.ID == id).Entity;
            var response = new ServiceResponse<UserMessageModel>();

            if (getUser != null)
            {
                var messageList = userMessageService.getList(x => x.SenderID == userID || x.ReceiverID == userID).List.Select(x => x.ReceiverID == userID ? x.SenderID : x.ReceiverID).Distinct().ToList();
                UserMessageModel userMessage = new UserMessageModel(getUser);
                if (messageList.Count != 0)
                {
                    foreach (var message in messageList)
                    {
                        userMessage.userMessages = userMessageService.getList((y) => y.SenderID == userID || y.ReceiverID == userID).List.ToList();

                    }
                }
                else
                {
                    userMessage.userMessages = new List<UserMessage>();

                }
                response.Count = userMessage.userMessages.Count;
                response.Entity = userMessage;
            }


            response.IsSuccessful = true;
            return Ok(response);
        }
        [HttpGet]
        [Route("/getSearchUser/{userName}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getSearchUser(string userName)
        {
            var userID = Handler.UserID(HttpContext);
            var getUserList = usersService.getList(x => x.UserName.Contains(userName));
            var response = new ServiceResponse<UserMessageModel>();
            List<UserMessageModel> userMessageModels = new List<UserMessageModel>();
            foreach (var item in getUserList.List)
            {
                if (item.ID != userID)
                {
                    var messageList = userMessageService.getList(x => x.SenderID == userID || x.ReceiverID == userID).List.Select(x => x.ReceiverID == userID ? x.SenderID : x.ReceiverID).Distinct().ToList();
                    UserMessageModel userMessage = new UserMessageModel(item);
                    if (messageList.Count != 0)
                    {
                        foreach (var message in messageList)
                        {
                            userMessage.userMessages = userMessageService.getList((y) => y.SenderID == userID || y.ReceiverID == userID).List.ToList();
                            userMessageModels.Add(userMessage);
                        }
                    }
                    else
                    {
                        userMessage.userMessages = new List<UserMessage>();
                        userMessageModels.Add(userMessage);
                    }

                }

            }
            response.List = userMessageModels;
            response.Count = userMessageModels.Count;
            response.IsSuccessful = true;
            return Ok(response);
        }
        [HttpGet]
        [Route("/getMessages")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getMessages()
        {
            var userID = Handler.UserID(HttpContext);
            var messageList = userMessageService.getList(x => x.SenderID == userID || x.ReceiverID == userID).List.Select(x => x.ReceiverID == userID ? x.SenderID : x.ReceiverID).Distinct().ToList();
            var response = new ServiceResponse<UserMessageModel>();


            List<UserMessageModel> userMessageModels = new List<UserMessageModel>();
            foreach (var message in messageList)
            {
                var user = usersService.get(x => x.ID == message).Entity;
                UserMessageModel userMessage = new UserMessageModel(user);
                userMessage.userMessages = userMessageService.getList((y) => y.SenderID == userID || y.ReceiverID == userID).List.ToList();
                userMessageModels.Add(userMessage);
            }
            response.List = userMessageModels;
            response.Count = userMessageModels.Count;
            response.IsSuccessful = true;
            return Ok(response);
        }
        [HttpDelete]
        [Route("/deleteMessage/")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult deleteMessage(int receiverID)
        {
            var userID = Handler.UserID(HttpContext);
            var response = userMessageService.delete(x => x.SenderID == userID);
            return Ok(response);
        }
        #endregion

        #region Favorite
        [Route("/addFavorite")]
        [Roles(Roles = RolesAttribute.User)]
        [HttpPost]
        public IActionResult addFavorite([FromBody] Favorite favorite)
        {
            favorite.UserID = Handler.UserID(HttpContext);
            var response = favoriteService.add(favorite);
            return Ok(response);
        }
        [Route("/deleteFavorite")]
        [Roles(Roles = RolesAttribute.User)]
        [HttpDelete]
        public IActionResult deleteFavorite([FromBody] Favorite favorite)
        {
            var id = Handler.UserID(HttpContext);
            if (id == favorite.UserID)
            {
                var response = favoriteService.delete(x => x.ID == favorite.ID);
                return Ok(response);
            }
            return BadRequest();
        }
        [Route("/getListFavorite")]
        [Roles(Roles = RolesAttribute.User)]
        [HttpGet]
        public IActionResult getListFavorite(int userID)
        {
            var response = favoriteService.getList(x => x.UserID == userID);
            return Ok(response);
        }

        #endregion

        #region UserList
        [HttpPost]
        [Route("/addUserList")]
        [Roles(RolesAttribute.All)]
        public IActionResult addUserList([FromBody] UserList userList)
        {
            var response = userListService.add(userList);
            return Ok(response);
        }
        [HttpPut]
        [Route("/updateUserList")]
        [Roles(RolesAttribute.All)]
        public IActionResult updateUserList([FromBody] UserList userList)
        {
            var response = userListService.update(userList);
            return Ok(response);
        }
        [HttpDelete]
        [Route("/deleteUserList/{id}")]
        [Roles(RolesAttribute.All)]
        public IActionResult deleteUserList(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var response = userListService.delete(x => x.ID == id && x.UserID == userID);
            return Ok(response);
        }
        #endregion
        #region UserList Content
        [HttpPost]
        [Route("/addUserListContents")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addUserListContents([FromBody] List<UserListContents> userListContents)
        {
            foreach (var content in userListContents)
            {
                var check = userListContentsService.get(x => x.ListID == content.ListID && x.ContentID == content.ContentID && x.EpisodeID == content.EpisodeID && x.Type == content.Type).Entity;
                if (check == null)
                {
                    userListContentsService.add(content);
                }
            }
            return Ok(userListContents);
        }
        [HttpDelete]
        [Route("/deleteUserListContent/{id}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteUserListContent(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var response = userListContentsService.delete(x => x.ID == id && x.UserID == userID);
            return Ok(response);
        }
        [HttpPost]
        [Route("/addUserListContent")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addUserListContent([FromBody] UserListContents userList)
        {
            var userID = Handler.UserID(HttpContext);
            if (userList.UserID == userID)
            {
                var response = userListContentsService.add(userList);
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion

        #region Anime and Manga
        [HttpGet]
        [Route("/getSearchAnimeAndManga/{text}")]
        public IActionResult getSearchAnimeAndManga(string text)
        {
            var response = new ServiceResponse<AnimeAndMangaModels>();
            List<AnimeAndMangaModels> list = new List<AnimeAndMangaModels>();
            var animeResponse = animeService.getList(x => x.AnimeName.ToLower().Contains(text)).List.ToList();
            var mangaResponse = mangaService.getList(x => x.Name.ToLower().Contains(text)).List.ToList();
            foreach (var anime in animeResponse)
            {
                list.Add(new AnimeAndMangaModels()
                {
                    ID = anime.ID,
                    CreateTime = anime.CreateTime,
                    name = anime.AnimeName,
                    img = anime.Img,
                    url = anime.SeoUrl,
                    arrangement = 1,
                    VideoType = anime.VideoType,
                    like = likeService.getList(x => x.Type == Type.Anime && x.ContentID == anime.ID).Count,
                    fanArtCount = fanArtService.getList(x => x.ContentID == anime.ID && x.Type == Type.Anime).Count,
                    reviewsCount = reviewService.getList(x => x.ContentID == anime.ID && x.Type == Type.Anime).Count,
                    Categories = categoryTypeService.getList(x => x.ContentID == anime.ID).List.ToList(),
                    AnimeSeasons = animeSeasonService.getList(x => x.AnimeID == anime.ID).List.ToList(),
                    AnimeEpisodes = animeEpisodesService.getList(x => x.AnimeID == anime.ID).List.ToList(),
                    Type = Type.Anime
                });
            }
            foreach (var manga in mangaResponse)
            {
                list.Add(new AnimeAndMangaModels()
                {
                    ID = manga.ID,
                    CreateTime = manga.CreateTime,
                    name = manga.Name,
                    img = manga.Image,
                    url = manga.SeoUrl,
                    arrangement = 1,

                    like = likeService.getList(x => x.Type == Type.Manga && x.ContentID == manga.ID).Count,
                    fanArtCount = fanArtService.getList(x => x.ContentID == manga.ID && x.Type == Type.Manga).Count,
                    reviewsCount = reviewService.getList(x => x.ContentID == manga.ID && x.Type == Type.Manga).Count,
                    MangaEpisodes = mangaEpisodesService.getList(x => x.MangaID == manga.ID).List.ToList(),
                    Categories = categoryTypeService.getList(x => x.ContentID == manga.ID).List.ToList(),
                    Type = Type.Manga
                });
            }
            response.List = list;
            response.Count = list.Count();
            return Ok(response);
        }
        #endregion

        #region Content Notification

        [HttpPost]
        [Route("/addContentNotification")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addContentNotification([FromBody] ContentNotification contentNotification)
        {
            var userID = Handler.UserID(HttpContext);
            if (contentNotification.UserID == userID)
            {
                var response = contentNotificationService.add(contentNotification);
                return Ok(response);
            }
            return BadRequest();

        }
        [Route("/deleteContentNotification/{id}")]
        [HttpDelete]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteContentNotification(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var response = contentNotificationService.delete(x => x.ID == id && x.UserID == userID);
            return Ok(response);
        }
        #endregion

        [HttpGet]
        [Route("/getDiscovers/{type}/{sno}/{showCount}")]
        public IActionResult getDiscovers(int type,int sno = 1,int showCount = 48)
        {
            var response = new ServiceResponse<DiscoverModels>();
            DiscoverModels discover = new DiscoverModels();
            if (type == 1)
            {
                var fanArts = fanArtService.getPaginatedFanArt(x => x.Type == Type.Anime,sno,showCount).List;
                List<FanArtModel> fanArtModels = new List<FanArtModel>();
                List<ReviewsModels> reviewsModels = new List<ReviewsModels>();
                foreach (var fanArt in fanArts)
                {
                    FanArtModel fanArtModel = new FanArtModel(fanArt);
                    if (fanArt.Type == Type.Manga)
                    {
                        fanArtModel.Manga = mangaService.get(x => x.ID == fanArt.ContentID).Entity;
                        fanArtModel.categories = categoryTypeService.getList(x => x.Type == Type.Manga && x.ContentID == fanArt.ContentID).List.ToList();
                    }
                    else
                    {
                        fanArtModel.Anime = animeService.get(x => x.ID == fanArt.ContentID).Entity;
                        fanArtModel.categories = categoryTypeService.getList(x => x.Type == Type.Anime && x.ContentID == fanArt.ContentID).List.ToList();
                    }
                    fanArtModel.Likes = likeService.getList((x) => x.Type == Type.FanArt).List.ToList();
                    fanArtModel.Comments = commentsService.getList(x => x.Type == Type.FanArt).List.ToList();
                    fanArtModels.Add(fanArtModel);
                }
                discover.FanArts = fanArtModels;
                var reviews = reviewService.getPaginatedReviews(x => x.Type == Type.Anime,sno,showCount).List;
                foreach (var review in reviews)
                {
                    ReviewsModels reviewsModel = new ReviewsModels(review);
                    if (review.Type == Type.Anime)
                    {
                        reviewsModel.Anime = animeService.get(x => x.ID == review.ContentID).Entity;
                        reviewsModel.categories = categoryTypeService.getList(x => x.Type == Type.Anime && x.ContentID == review.ContentID).List.ToList();
                    }
                    else
                    {
                        reviewsModel.Manga = mangaService.get(x => x.ID == review.ContentID).Entity;
                        reviewsModel.categories = categoryTypeService.getList(x => x.Type == Type.Manga && x.ContentID == review.ContentID).List.ToList();

                    }
                    reviewsModel.Likes = likeService.getList((x) => x.Type == Type.Reviews).List.ToList();
                    reviewsModel.Comments = commentsService.getList(x => x.Type == Type.Reviews).List.ToList();
                    reviewsModels.Add(reviewsModel);
                }
                discover.Reviews = reviewsModels;
                var movieTheWeekModels = movieTheWeekService.getList(x => x.Type == Type.Anime).List;
                List<MovieTheWeekModels> movieTheWeeks = new List<MovieTheWeekModels>();
                foreach (var item in movieTheWeekModels)
                {
                    MovieTheWeekModels movieTheWeek = new MovieTheWeekModels(item);
                    movieTheWeek.Anime = animeService.get(x => x.ID == item.ContentID).Entity;
                    movieTheWeek.Users = usersService.get(x => x.ID == item.UserID).Entity;
                    movieTheWeeks.Add(movieTheWeek);

                }
                discover.MovieTheWeeks = movieTheWeeks;
            }
            else
            {
                var fanArts = fanArtService.getPaginatedFanArt(x => x.Type == Type.Manga,sno,showCount).List;
                List<FanArtModel> fanArtModels = new List<FanArtModel>();
                List<ReviewsModels> reviewsModels = new List<ReviewsModels>();
                foreach (var fanArt in fanArts)
                {
                    FanArtModel fanArtModel = new FanArtModel(fanArt);
                    if (fanArt.Type == Type.Manga)
                    {
                        fanArtModel.categories = categoryTypeService.getList(x => x.Type == Type.Manga && x.ContentID == fanArt.ContentID).List.ToList();
                    }
                    else
                    {
                        fanArtModel.categories = categoryTypeService.getList(x => x.Type == Type.Anime && x.ContentID == fanArt.ContentID).List.ToList();
                    }
                    fanArtModel.Likes = likeService.getList((x) => x.Type == Type.FanArt).List.ToList();
                    fanArtModel.Comments = commentsService.getList(x => x.Type == Type.FanArt).List.ToList();
                    fanArtModels.Add(fanArtModel);
                }
                discover.FanArts = fanArtModels;
                var reviews = reviewService.getPaginatedReviews(x => x.Type == Type.Manga,sno,showCount).List;
                foreach (var review in reviews)
                {
                    ReviewsModels reviewsModel = new ReviewsModels(review);
                    if (review.Type == Type.Manga)
                    {
                        reviewsModel.Manga = mangaService.get(x => x.ID == review.ContentID).Entity;
                        reviewsModel.categories = categoryTypeService.getList(x => x.Type == Type.Manga && x.ContentID == review.ContentID).List.ToList();

                    }
                    else
                    {
                        reviewsModel.categories = categoryTypeService.getList(x => x.Type == Type.Anime && x.ContentID == review.ContentID).List.ToList();
                        reviewsModel.Anime = animeService.get(x => x.ID == review.ContentID).Entity;
                    }
                    reviewsModel.Likes = likeService.getList((x) => x.Type == Type.Reviews).List.ToList();
                    reviewsModel.Comments = commentsService.getList(x => x.Type == Type.Reviews).List.ToList();
                    reviewsModels.Add(reviewsModel);
                }
                discover.Reviews = reviewsModels;
                var movieTheWeekModels = movieTheWeekService.getList(x => x.Type == Type.Manga).List;
                List<MovieTheWeekModels> movieTheWeeks = new List<MovieTheWeekModels>();
                foreach (var item in movieTheWeekModels)
                {
                    MovieTheWeekModels movieTheWeek = new MovieTheWeekModels(item);
                    movieTheWeek.Manga = mangaService.get(x => x.ID == item.ContentID).Entity;
                    movieTheWeek.Users = usersService.get(x => x.ID == item.UserID).Entity;
                    movieTheWeeks.Add(movieTheWeek);

                }
                discover.MovieTheWeeks = movieTheWeeks;
            }
            response.Entity = discover;
            response.IsSuccessful = true;
            return Ok(response);
        }
        [HttpGet]
        [Route("/getHomePageMovie/{pageNo}/{showCount}")]
        public IActionResult getHomePageMovie(int pageNo = 1, int showCount = 20)
        {
            var response = new ServiceResponse<MovieDTO>();
            MovieDTO movieDTO = new MovieDTO();

           var animeEpisode = animeEpisodesService.getList().List.OrderByDescending(x=>x.ID).DistinctBy(x => x.AnimeID).ToList();
        
            var mangaEpisode = mangaEpisodesService.getList().List.OrderByDescending(x => x.ID).DistinctBy(x => x.MangaID).ToList();

            List<Anime> animeEpisodes = new List<Anime>();
            foreach (var anime in animeEpisode)
            {
                animeEpisodes.Add(animeService.get(x => x.ID == anime.AnimeID).Entity);
            }
            List<Manga> mangaEpisodes = new List<Manga>();
            foreach (var manga in mangaEpisode)
            {
                mangaEpisodes.Add(mangaService.get(x => x.ID == manga.MangaID).Entity);
            }

            List<AnimeModels> animeModels = new List<AnimeModels>();
            var animes = animeService.getList().List;
            foreach (var item in animes)
            {
                AnimeModels anime = new AnimeModels();
                anime.Anime = item;
                var _animeEpisodes = animeEpisodesService.getList(x => x.AnimeID == item.ID).List.ToList();
                anime.AnimeEpisodes = _animeEpisodes;
                anime.AnimeSeasons = animeSeasonService.getList(x => x.AnimeID == item.ID).List.ToList();
                anime.Categories = categoryTypeService.getList(x => x.Type == Entites.Type.Anime && x.ContentID == item.ID).List.ToList();
                anime.Arrangement = 1;
                anime.Categories = categoryTypeService.getList(x => x.ContentID == item.ID && x.Type == Type.Anime).List.ToList();
                anime.Comments = commentsService.getList(x => x.Type == Type.Comment && x.ContentID == item.ID).List.ToList();
                anime.Comments = commentsService.getList(x => x.Type == Entites.Type.Anime && x.ContentID == item.ID).List.ToList();
                anime.ContentNotification = contentNotificationService.get(x => x.ContentID == item.ID && x.Type == Entites.Type.Anime).Entity;
                anime.LikeCount = likeService.getList(x => x.ContentID == item.ID && x.Type == Entites.Type.Anime).List.ToList().Count;
                anime.ViewsCount = animeListService.getList(x => x.AnimeID == item.ID && x.AnimeStatus == AnimeStatus.IWatched).List.ToList().Count;
                var likes = likeService.getList(x => x.ContentID == item.ID && x.Type == Type.Anime).Count;
                anime.Rating = likes / 10;
                animeModels.Add(anime);
            }
            List<MangaModels> mangaModels = new List<MangaModels>();
            var mangas = mangaService.getList().List;
            foreach (var item in mangas)
            {
                MangaModels manga = new MangaModels();
                manga.Manga = item;
                var likes = likeService.getList(x => x.ContentID == item.ID && x.Type == Type.Manga).Count;
                manga.Comments = commentsService.getList(x => x.ContentID == item.ID && x.Type == Entites.Type.Manga).List.ToList();
                manga.Categories = categoryTypeService.getList(x => x.Type == Entites.Type.Manga && x.ContentID == item.ID).List.ToList();
                manga.Arrangement = 1;
                manga.ContentNotification = contentNotificationService.get(x => x.ContentID == item.ID && x.Type == Entites.Type.Manga).Entity;
                manga.LikeCount = likeService.getList(x => x.ContentID == item.ID && x.Type == Entites.Type.Manga).List.ToList().Count;
                manga.ViewsCount = mangaListService.getList(x => x.MangaID == item.ID && x.Status == MangaStatus.IRead).List.ToList().Count;
                manga.MangaEpisodeCount = mangaEpisodesService.getList(x => x.MangaID == item.ID).List.Count();
                manga.Rating = likes / 10;
                mangaModels.Add(manga);
            }
            movieDTO.NewEpisodeAnimes = animeEpisodes;
            movieDTO.NewEpisodeManga = mangaEpisodes;
            movieDTO.RatingMangas = mangaModels.OrderByDescending(x => x.Rating).Take(10).ToList();
            movieDTO.RatingAnimes = animeModels.OrderByDescending(x => x.Rating).Take(10).ToList();
            response.Entity = movieDTO;
            response.IsSuccessful = true;
            return Ok(response);
        }
    }
}

