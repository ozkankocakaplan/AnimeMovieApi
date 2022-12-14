using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMovie.API.Models;
using AnimeMovie.Business;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimeMovie.API.Controllers
{
    [Route("api/[controller]")]
    public class ComplaintController : Controller
    {
        private readonly IComplaintListService complaintListService;
        private readonly IContentComplaintService contentComplaintService;
        private readonly IAnimeService animeService;
        private readonly IMangaService mangaService;
        private readonly IUsersService usersService;
        private readonly IAnimeEpisodesService animeEpisodesService;
        private readonly IMangaEpisodesService mangaEpisodesService;
        private readonly IFanArtService fanArtService;
        private readonly IReviewService reviewService;
        public ComplaintController(IComplaintListService complaintList, IContentComplaintService contentComplaint,
            IAnimeService anime, IMangaService manga, IUsersService users, IAnimeEpisodesService animeEpisodes, IMangaEpisodesService mangaEpisodes,
            IFanArtService fanArt, IReviewService review)
        {
            complaintListService = complaintList;
            contentComplaintService = contentComplaint;
            animeService = anime;
            mangaService = manga;
            usersService = users;
            animeEpisodesService = animeEpisodes;
            mangaEpisodesService = mangaEpisodes;
            fanArtService = fanArt;
            reviewService = review;
        }

        #region UserComplaint
        [HttpPost]
        [Route("/addComplaint")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addComplaint([FromBody] ComplaintList complaintList)
        {
            var userID = Handler.UserID(HttpContext);
            if (complaintList.ComplainantID == userID && complaintList.Description.Length != 0)
            {
                var response = complaintListService.add(complaintList);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getComplaints")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getComplaints()
        {
            var response = complaintListService.getComplaintListModels();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getComplaintsByUserID")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult getComplaintsByUserID()
        {
            var userID = Handler.UserID(HttpContext);
            var response = complaintListService.getList(x => x.ComplainantID == userID);
            return Ok(response);
        }
        [HttpDelete]
        [Route("/deleteComplaint")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteComplaint([FromBody] List<int> list)
        {
            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    complaintListService.delete(x => x.ID == item);
                }
                return Ok();
            }
            return BadRequest();
        }
        #endregion
        #region ContentComplaint
        [HttpPost]
        [Route("/addContentComplaint")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addContentComplaint([FromBody] ContentComplaint contentComplaint)
        {
            var userID = Handler.UserID(HttpContext);
            if (contentComplaint.UserID == userID)
            {
                var response = contentComplaintService.add(contentComplaint);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteContentComplaint/{id}")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult deleteContentComplaint(int id)
        {
            var response = contentComplaintService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getContentComplaint")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult getContentComplaint()
        {
            var response = new ServiceResponse<ComplaintContentModels>();
            var contentComplaints = contentComplaintService.getList();
            List<ComplaintContentModels> list = new List<ComplaintContentModels>();
            foreach (var content in contentComplaints.List)
            {
                ComplaintContentModels complaintContent = new ComplaintContentModels(content);
                complaintContent.User = usersService.get(x => x.ID == content.UserID).Entity;
                if (content.type == Entites.Type.Anime)
                {
                    var episode = complaintContent.AnimeEpisode = animeEpisodesService.get(x => x.ID == content.ContentID).Entity;
                    if (episode != null)
                    {
                        complaintContent.Anime = animeService.get(x => x.ID == episode.AnimeID).Entity;
                    }

                }
                else if (content.type == Entites.Type.Manga)
                {
                    var episode = complaintContent.MangaEpisode = mangaEpisodesService.get(x => x.ID == content.ContentID).Entity;
                    if (episode != null)
                    {
                        complaintContent.Manga = mangaService.get(x => x.ID == episode.MangaID).Entity;
                    }
                }
                else if (content.ComplaintType == ComplaintType.ContentAnime)
                {
                    var episode = complaintContent.AnimeEpisode = animeEpisodesService.get(x => x.ID == content.ContentID).Entity;
                    if (episode != null)
                    {
                        complaintContent.Anime = animeService.get(x => x.ID == episode.AnimeID).Entity;
                    }
                }
                else if (content.ComplaintType == ComplaintType.ContentManga)
                {
                    var episode = complaintContent.MangaEpisode = mangaEpisodesService.get(x => x.ID == content.ContentID).Entity;
                    if (episode != null)
                    {
                        complaintContent.Manga = mangaService.get(x => x.ID == episode.MangaID).Entity;
                    }
                }

                list.Add(complaintContent);
            }
            response.List = list;
            response.Count = list.Count;
            response.IsSuccessful = true;
            return Ok(response);
        }
        #endregion
    }
}

