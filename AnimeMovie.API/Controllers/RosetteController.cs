using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMovie.API.Models;
using AnimeMovie.Business;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Mvc;
using Type = AnimeMovie.Entites.Type;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimeMovie.API.Controllers
{
    public class RosetteController : Controller
    {
        private readonly IRosetteService rosetteService;
        private readonly IUserRosetteService userRosetteService;
        private readonly IRosetteContentService rosetteContentService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAnimeEpisodesService animeEpisodesService;
        private readonly IMangaEpisodesService mangaEpisodesService;
        private readonly IAnimeService animeService;
        private readonly IMangaService mangaService;
        public RosetteController(IRosetteService rosette, IUserRosetteService userRosette, IWebHostEnvironment webHost,
            IRosetteContentService rosetteContent, IAnimeEpisodesService animeEpisodes, IMangaEpisodesService mangaEpisodes, IAnimeService anime,
            IMangaService manga)
        {
            animeService = anime;
            mangaService = manga;
            animeEpisodesService = animeEpisodes;
            mangaEpisodesService = mangaEpisodes;
            rosetteService = rosette;
            userRosetteService = userRosette;
            webHostEnvironment = webHost;
            rosetteContentService = rosetteContent;
        }
        #region Rosette
        [HttpPost]
        [Route("/addRosette")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addRosette([FromForm] IFormFile img, Rosette rosette)
        {
            if (rosette.Name.Length != 0)
            {
                rosette.Img = "";
                if (img != null && img.Length != 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var patch = webHostEnvironment.WebRootPath + "/image/";
                    using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                    {
                        img.CopyTo(fs);
                        fs.Flush();
                        rosette.Img = "/image/" + guid + img.FileName;
                    }
                }
                var response = rosetteService.add(rosette);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("/updateRosette")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateRosette([FromForm] IFormFile img, Rosette rosette)
        {
            if (rosette.Name.Length != 0 && rosette.ID != 0)
            {
                if (img != null && img.Length != 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var patch = webHostEnvironment.WebRootPath + "/image/";
                    using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                    {
                        img.CopyTo(fs);
                        fs.Flush();
                        rosette.Img = "/image/" + guid + img.FileName;
                    }
                }
                var response = rosetteService.update(rosette);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteRosette")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteRosette([FromBody] List<int> rosettes)
        {
            foreach (var rosette in rosettes)
            {
                var rosetteContents = rosetteContentService.getList(x => x.RosetteID == rosette);
                if (rosetteContents.Count != 0)
                {
                    foreach (var content in rosetteContents.List)
                    {
                        rosetteContentService.delete(x => x.ID == content.ID);
                    }
                }
                var response = rosetteService.delete(x => x.ID == rosette);
            }

            return Ok();
        }
        [HttpGet]
        [Route("/getRosettes")]
        public IActionResult getRosettes()
        {
            var response = rosetteService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getRosette/{id}")]
        public IActionResult getRosette(int id)
        {
            RosetteModels rosetteModels = new RosetteModels();

            var response = rosetteService.get(x => x.ID == id);
            if (response.Entity != null)
            {
                rosetteModels.Rosette = response.Entity;
                var rosetteContents = rosetteContentService.getList(X => X.RosetteID == id);
                rosetteModels.RosetteContents = rosetteContents.List.ToList();
                if (rosetteContents.Count != 0)
                {
                    var _response = new ServiceResponse<RosetteModels>();
                    List<MangaEpisodes> mangaEpisodes = new List<MangaEpisodes>();
                    List<AnimeEpisodes> animeEpisodes = new List<AnimeEpisodes>();
                    foreach (var content in rosetteContents.List)
                    {
                        if (content.Type == Type.Anime)
                        {
                            if (rosetteModels.Anime == null)
                            {
                                rosetteModels.Anime = animeService.get(x => x.ID == content.ContentID).Entity;
                            }

                            var animeEpisode = animeEpisodesService.get(x => x.ID == content.EpisodesID && x.AnimeID == rosetteModels.Anime.ID).Entity;

                            if (animeEpisode != null)
                            {
                                animeEpisodes.Add(animeEpisode);
                            }
                        }
                        else
                        {
                            if (rosetteModels.Manga == null)
                            {
                                rosetteModels.Manga = mangaService.get(x => x.ID == content.ContentID).Entity;
                            }
                            var mangaEpisode = mangaEpisodesService.get(x => x.ID == content.EpisodesID && x.MangaID == rosetteModels.Manga.ID).Entity;

                            if (mangaEpisode != null)
                            {
                                mangaEpisodes.Add(mangaEpisode);
                            }
                        }
                    }
                    rosetteModels.AnimeEpisodes = animeEpisodes;
                    rosetteModels.MangaEpisodes = mangaEpisodes;
                    _response.Entity = rosetteModels;
                    _response.IsSuccessful = true;
                    return Ok(_response);
                }
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("/updateRosetteImg/{rosetteID}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateRosetteImg([FromForm] IFormFile file, int rosetteID)
        {
            if (file != null && file.Length != 0)
            {
                var rosette = rosetteService.get(x => x.ID == rosetteID).Entity;
                var guid = Guid.NewGuid().ToString();
                var patch = webHostEnvironment.WebRootPath + "/image/";
                using (FileStream fs = System.IO.File.Create(patch + guid + file.FileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                rosette.Img = "/image/" + guid + file.FileName;
                var response = rosetteService.update(rosette);
                return Ok(response);
            }
            return BadRequest();
        }
        #endregion

        #region UserRosette
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addUserRosette")]
        public IActionResult addUserRosette([FromBody] UserRosette userRosette)
        {
            if (userRosette.UserID != 0 && userRosette.RosetteID != 0)
            {
                var response = userRosetteService.add(userRosette);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateUserRosette")]
        public IActionResult updateUserRosette([FromBody] UserRosette userRosette)
        {
            if (userRosette.UserID != 0 && userRosette.RosetteID != 0 && userRosette.ID != 0)
            {
                var response = userRosetteService.update(userRosette);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteUserRosette")]
        public IActionResult deleteUserRosette(int id)
        {
            if (id != 0)
            {
                var response = userRosetteService.delete(x => x.ID == id);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getUserRosettes")]
        public IActionResult getUserRosettes()
        {
            var response = userRosetteService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getUserRosetteByID/{id}")]
        public IActionResult getUserRosetteByID(int id)
        {
            var response = userRosetteService.get(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getUserRosetteByUserID/{userID}")]
        public IActionResult getUserRosetteByUserID(int userID)
        {
            var response = userRosetteService.get(x => x.UserID == userID);
            return Ok(response);
        }
        #endregion

        #region RosetteContent
        [Route("/addRosetteContent")]
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addRosetteContent([FromBody] List<RosetteContent> rosetteContents)
        {
            foreach (var item in rosetteContents)
            {
                var response = rosetteContentService.add(item);
            }
            return Ok();
        }
        [Route("/updateRosetteContent/{rosetteID}")]
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateRosetteContent([FromBody] List<RosetteContent> rosetteContents, int rosetteID)
        {

            if (rosetteContents.Count != 0)
            {
                var response = new ServiceResponse<RosetteContent>();
                var oldList = rosetteContentService.getList(x => x.RosetteID == rosetteID);
                if (oldList.Count != 0)
                {
                    foreach (var content in oldList.List)
                    {
                        rosetteContentService.delete(x => x.ID == content.ID);
                    }
                }
                if (rosetteContents != null && rosetteContents.Count != 0)
                {
                    foreach (var content in rosetteContents)
                    {
                        rosetteContentService.add(content);
                    }
                }
                response.List = rosetteContents;
                response.IsSuccessful = true;
                return Ok(response);
            }
            return BadRequest();
        }
        [Route("/getRosetteContent/{rosetteID}")]
        [HttpGet]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult getRosetteContent(int rosetteID)
        {
            var response = rosetteContentService.getList(x => x.RosetteID == rosetteID);
            return Ok(response);
        }
        #endregion
    }
}

