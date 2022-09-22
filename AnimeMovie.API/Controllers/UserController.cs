using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMovie.Business.Abstract;
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
        private readonly IAnimeRatingService animeRatingService;
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
        public UserController(IWebHostEnvironment webHost, IUsersService users, IAnimeRatingService animeRating,
            IAnimeListService animeList,
            IUserEmailVertificationService userEmailVertification,
            IUserForgotPasswordService userForgotPassword, IUserBlockListService userBlockList,
            IReviewService review, ICommentsService comments, INotificationService notification, IMangaListService mangaList,
            ILikeService like, IFanArtService fanArt, IUserMessageService userMessage)
        {
            webHostEnvironment = webHost;
            usersService = users;
            animeRatingService = animeRating;
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
        }

        #region Users
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
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
            return Ok(response);
        }
        [AllowAnonymous]
        [Route("/addUser")]
        [HttpPost]
        public IActionResult addUser([FromBody] Users user)
        {
            user.isBanned = false;
            user.RoleType = RoleType.User;
            var response = usersService.add(user);
            return Ok(response);
        }
        [Roles(Roles = RolesAttribute.User)]
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
        [Roles(Roles = RolesAttribute.User)]
        [Route("/updateImage")]
        [HttpPut]
        public IActionResult updateImage([FromForm] IFormFile img)
        {
            if (img != null && img.Length != 0)
            {
                var guid = Guid.NewGuid().ToString();
                string patch = webHostEnvironment.WebRootPath + "/user/";
                using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    var response = usersService.updateImage("/user/" + guid + img.FileName, Handler.UserID(HttpContext));
                }

                return BadRequest();
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
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateIsBanned/{userID}")]
        [HttpPut]
        public IActionResult updateIsBanned(int userID)
        {
            var response = usersService.updateUserBanned(userID);
            return Ok(response);
        }
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteUser/{userID}")]
        [HttpPut]
        public IActionResult deleteUser(int userID)
        {
            var response = usersService.delete(x => x.ID == userID);
            return Ok(response);
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
                var getUserEmail = userEmailVertificationService.delete(x => x.Email == email);
                var response = userEmailVertificationService.add(new UserEmailVertification() { Email = email });
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion
        #region AnimeRating
        [HttpPost]
        [Roles(Roles = RolesAttribute.User)]
        [Route("/addAnimeRating")]
        public IActionResult addAnimeRating([FromBody] AnimeRating animeRating)
        {
            var id = Handler.UserID(HttpContext);
            if (animeRating.AnimeID != 0 && id == animeRating.UserID)
            {
                var response = animeRatingService.add(animeRating);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.User)]
        [Route("/updateAnimeRating")]
        public IActionResult updateAnimeRating([FromBody] AnimeRating animeRating)
        {
            var id = Handler.UserID(HttpContext);
            if (animeRating.AnimeID != 0 && animeRating.ID != 0 && animeRating.UserID == id)
            {
                var response = animeRatingService.update(animeRating);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/deleteAnimeRatings/{id}")]
        public IActionResult deleteAnimeRatings(int id)
        {
            var getAnimeRating = animeRatingService.get(x => x.ID == id && x.UserID == Handler.UserID(HttpContext));
            if (getAnimeRating.Entity != null)
            {
                var response = animeRatingService.delete(x => x.ID == id);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getAnimeRatings/{animeID}")]
        public IActionResult getAnimeRatings(int animeID)
        {
            var response = animeRatingService.getList(x => x.AnimeID == animeID);
            return Ok(response);
        }
        #endregion

        #region UserForgotPassword
        [HttpPost]
        [Route("/addUserForgotPassword")]
        public IActionResult addUserForgotPassword([FromBody] UserForgotPassword forgotPassword)
        {
            if (forgotPassword.UserID != 0)
            {
                var response = userForgotPasswordService.add(forgotPassword);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getUserForgotPassword/{userID}")]
        public IActionResult getUserForgotPassword(int userID)
        {
            if (userID != 0)
            {
                var response = userForgotPasswordService.get(x => x.UserID == userID);
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion
        #region UserBlockList
        [HttpPost]
        [Route("/addUserBlockList")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult addUserBlockList([FromBody] UserBlockList user)
        {
            var userID = Handler.UserID(HttpContext);
            if (user.BlockID != 0 && userID == user.UserID)
            {
                var response = userBlockListService.add(user);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteUserBlockList/{blockID}")]
        [Roles(Roles = RolesAttribute.User)]
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
        [Route("/getUserBlockList")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult getUserBlockList()
        {
            var userID = Handler.UserID(HttpContext);
            var response = userBlockListService.getList(x => x.UserID == userID);
            return Ok(response);
        }
        #endregion
        #region Comments
        [HttpPost]
        [Route("/addComment")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult addComment([FromBody] Comments comment)
        {
            var userID = Handler.UserID(HttpContext);
            if (comment.ContentID != 0 && comment.Comment.Length != 0 && comment.UserID == userID)
            {
                var response = commentsService.add(comment);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteComment/{commentID}/{type}")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult deleteComment(int commentID, int type)
        {
            var userID = Handler.UserID(HttpContext);
            var response = commentsService.delete(x => x.ID == commentID && x.UserID == userID && x.Type == (Type)type);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getComments/{contentID}/{pageNo}/{showCount}")]
        public IActionResult getComments(int contentID, int type, int pageNo = 1, int showCount = 10)
        {
            var response = commentsService.getPaginatedComments(x => x.ContentID == contentID && x.Type == (Type)type, pageNo, showCount);
            return Ok(response);
        }

        #endregion
        #region Reviews
        [HttpPost]
        [Route("/addReviews")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult addReview([FromBody] Review review)
        {
            var userID = Handler.UserID(HttpContext);
            if (review.UserID == userID && review.AnimeID != 0 && review.Message.Length != 0)
            {
                var response = reviewService.add(review);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("/deleteReview/{reviewID}")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult deleteReview(int reviewID)
        {
            var userID = Handler.UserID(HttpContext);
            var response = reviewService.delete(x => x.ID == reviewID && x.UserID == userID);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getReviews/{animeID}/{pageNo}/{showCount}")]
        public IActionResult getReviews(int animeID, int pageNo = 1, int showCount = 10)
        {
            var response = reviewService.getPaginatedReviews(x => x.AnimeID == animeID, pageNo, showCount);
            return Ok(response);
        }
        #endregion
        #region Notifications
        [HttpGet]
        [Route("/getNotifications/{pageNo}/{showCount}")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult getNotifications(int pageNo = 1, int showCount = 10)
        {
            var userID = Handler.UserID(HttpContext);
            var response = notificationService.getPaginatedNotifications(x => x.UserID == userID, pageNo, showCount);
            return Ok(response);
        }
        [HttpPut]
        [Route("/updateNotification/{notificationID}")]
        [Roles(Roles = RolesAttribute.User)]
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
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
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
        [Route("/addAnimeList")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult addAnimeList([FromBody] AnimeList animeList)
        {
            var id = Handler.UserID(HttpContext);
            if (animeList.AnimeID != 0 && animeList.UserID == id)
            {
                var response = animeListService.add(animeList);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteAnimeList/{id}")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult deleteAnimeList(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var response = animeListService.delete(x => x.UserID == userID && x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeList")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult getAnimeList()
        {
            var userID = Handler.UserID(HttpContext);
            var response = animeListService.getList(x => x.UserID == userID);
            return Ok(response);
        }
        #endregion
        #region MangaList
        [HttpPost]
        [Route("/addMangaList")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult addMangaList([FromBody] MangaList mangaList)
        {
            var id = Handler.UserID(HttpContext);
            if (mangaList.EpisodeID != 0 && mangaList.UserID == id)
            {
                var response = mangaListService.add(mangaList);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteMangaList/{id}")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult deleteMangaList(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var response = mangaListService.delete(x => x.UserID == userID && x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getMangaList")]
        [Roles(Roles = RolesAttribute.User)]
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
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult addLike([FromBody] Like like)
        {
            var userID = Handler.UserID(HttpContext);
            if (like.ContentID != 0 && like.UserID == userID)
            {
                var response = likeService.add(like);
                return Ok(response);
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
        [Roles(Roles = RolesAttribute.User)]
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
        [Roles(Roles = RolesAttribute.User)]
        [HttpPost]
        public IActionResult addFanArt([FromForm] IFormFile img, FanArt fanArt)
        {
            var userID = Handler.UserID(HttpContext);
            if (img != null && img.Length != 0 && fanArt.UserID == userID)
            {
                var patch = webHostEnvironment.WebRootPath + "/fanart/";
                using (FileStream fs = System.IO.File.Create(patch + fanArt.UserID + "-" + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    fanArt.Image = "/fanArt/" + fanArt.UserID + "-" + img.FileName;
                }
                var response = fanArtService.add(fanArt);
                return Ok(response);
            }
            return BadRequest();
        }
        [Roles(Roles = RolesAttribute.User)]
        [Route("/deleteFanArt/{id}")]
        [HttpDelete]
        public IActionResult deleteFanArt(int id)
        {
            var userID = Handler.UserID(HttpContext);
            var response = fanArtService.delete(x => x.ID == id && x.UserID == userID);
            return Ok(response);
        }
        [Route("/getFanArts/{animeID}/{pageNo}/{showCount}")]
        [HttpPost]
        public IActionResult getFanArts(int animeID, int pageNo = 1, int showCount = 10)
        {
            var response = fanArtService.getPaginatedFanArt(x => x.AnimeID == animeID, pageNo, showCount);
            return Ok(response);
        }
        #endregion

        #region Message
        [HttpGet]
        [Route("/getMessages")]
        [Roles(Roles = RolesAttribute.User)]
        public IActionResult getMessages()
        {
            var userID = Handler.UserID(HttpContext);
            var response = userMessageService.getList(x => x.SenderID == userID || x.ReceiverID == userID);
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
    }
}

