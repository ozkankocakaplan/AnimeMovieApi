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
        public AdminController(IAnnouncementService announcement,
            ISiteDescriptionService siteDescription,
            ISocialMediaAccountService socialMediaAccount)
        {
            announcementService = announcement;
            siteDescriptionService = siteDescription;
            socialMediaAccountService = socialMediaAccount;
        }
        #region Announcement
        [HttpPut]
        [Route("/updateAnnouncement")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateAnnouncement([FromBody] Announcement announcement)
        {
            var response = announcementService.update(announcement);
            return Ok(response);
        }
        [Route("/getAnnouncement")]
        [HttpGet]
        public IActionResult getAnnouncement()
        {
            var response = announcementService.getList();
            return Ok(response);
        }
        #endregion
        #region SiteDescription
        [HttpGet]
        [Route("/getSiteDescription")]
        public IActionResult getSiteDescription()
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
        [Route("/getSocialMediaAccount")]
        public IActionResult getSocialMediaAccount()
        {
            var response = siteDescriptionService.getList();
            return Ok(response);
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateSocialMediaAccount")]
        public IActionResult updateSocialMediaAccount([FromBody] SocialMediaAccount socialMediaAccount)
        {
            var response = socialMediaAccountService.update(socialMediaAccount);
            return Ok(response);
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
            if ((RoleType)role == RoleType.Admin)
            {
                var response = socialMediaAccountService.delete(x => x.ID == id);
                return Ok(response);
            }
            if ((RoleType)role == RoleType.Moderator)
            {
                var response = socialMediaAccountService.delete(x => x.UserID == id);
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion
    }
}

