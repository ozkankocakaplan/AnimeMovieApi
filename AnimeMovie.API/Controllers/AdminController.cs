using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimeMovie.API.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IAnnouncementService announcementService;
        private readonly ISiteDescriptionService siteDescriptionService;
        private readonly ISocialMediaAccountService socialMediaAccountService;
        private readonly IUsersService usersService;
        public AdminController(IAnnouncementService announcement,
            ISiteDescriptionService siteDescription,
            ISocialMediaAccountService socialMediaAccount,
            IUsersService users)
        {
            usersService = users;
            announcementService = announcement;
            siteDescriptionService = siteDescription;
            socialMediaAccountService = socialMediaAccount;
        }
        #region User
        [HttpGet]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/getSearchDetailsUser/{search}")]
        public IActionResult getSearchDetailsUser(string search)
        {
            var response = usersService.getList(x => x.UserName.ToLower().Contains(search.ToLower()));
            return Ok(response);
        }
        #endregion
        #region Announcement
        [HttpPut]
        [Route("/updateAnnouncement")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateAnnouncement([FromBody] Announcement announcement)
        {
            var response = announcementService.update(announcement);
            return Ok(response);
        }
        [Route("/getAnnouncements")]
        [HttpGet]
        public IActionResult getAnnouncements()
        {
            var response = announcementService.getList();
            return Ok(response);
        }
        #endregion
        #region SiteDescription
        [HttpGet]
        [Route("/getSiteDescriptions")]
        public IActionResult getSiteDescriptions()
        {
            var response = siteDescriptionService.getList();
            return Ok(response);
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateSiteDescription")]
        public IActionResult updateSiteDescription([FromBody] SiteDescription site)
        {

            var response = siteDescriptionService.update(site);
            return Ok(response);
        }
        #endregion
        #region SocialMediaAccount
        [HttpGet]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/getSocialMediaAccount")]
        public IActionResult getSocialMediaAccount()
        {
            var id = Handler.UserID(HttpContext);
            var response = socialMediaAccountService.get(x => x.UserID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getSocialMediaAccounts")]
        public IActionResult getSocialMediaAccounts()
        {
            var response = socialMediaAccountService.getList();
            return Ok(response);
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateSocialMediaAccount")]
        public IActionResult updateSocialMediaAccount([FromBody] SocialMediaAccount socialMediaAccount)
        {
            var id = Handler.UserID(HttpContext);
            socialMediaAccount.UserID = id;
            var list = socialMediaAccountService.getList(x => x.UserID == id);
            if (list.Count == 0)
            {
                return Ok(socialMediaAccountService.add(socialMediaAccount));
            }
            else
            {
                var response = socialMediaAccountService.update(socialMediaAccount);
                return Ok(response);
            }

        }
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addSocialMediaAccount")]
        public IActionResult addSocialMediaAccount([FromBody] SocialMediaAccount socialMediaAccount)
        {
            var response = socialMediaAccountService.add(socialMediaAccount);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteSocialMediaAccount/{id}")]
        public IActionResult deleteSocialMediaAccount(int id)
        {
            var role = Handler.RolType(HttpContext);
            if (role == "Admin")
            {
                var response = socialMediaAccountService.delete(x => x.ID == id);
                return Ok(response);
            }
            if (role == "Moderator")
            {
                var response = socialMediaAccountService.delete(x => x.UserID == id);
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion
    }
}

