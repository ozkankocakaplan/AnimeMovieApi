using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMovie.API.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAnimeService animeService;
        private readonly IAnimeRatingService animeRatingService;
        public AnimeController(IWebHostEnvironment webHost, IAnimeService anime, IAnimeRatingService animeRating)
        {
            webHostEnvironment = webHost;
            animeService = anime;
            animeRatingService = animeRating;
        }
        #region Anime

        [HttpPost]
        [Route("/addAnime")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addAnime([FromForm] IFormFile animeImg, [FromQuery] Anime anime)
        {
            if (animeImg != null && animeImg.Length != 0)
            {
                string guid = Guid.NewGuid().ToString();
                string patch = webHostEnvironment.WebRootPath + "/anime/";
                using (FileStream fs = System.IO.File.Create(patch + guid + animeImg.FileName))
                {
                    animeImg.CopyTo(fs);
                    fs.Flush();
                    anime.Img = "/image/" + guid + animeImg.FileName;
                }
            }
            var response = animeService.add(anime);
            return Ok(response);
        }
        [HttpPut]
        [Route("/updateAnime")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateAnime([FromForm] IFormFile animeImg, [FromQuery] Anime anime)
        {
            if (animeImg != null && animeImg.Length != 0)
            {
                string guid = Guid.NewGuid().ToString();
                string patch = webHostEnvironment.WebRootPath + "/anime/";
                using (FileStream fs = System.IO.File.Create(patch + guid + animeImg.FileName))
                {
                    animeImg.CopyTo(fs);
                    fs.Flush();
                    anime.Img = "/image/" + guid + animeImg.FileName;
                    var getAnime = animeService.get(x => x.ID == anime.ID).Entity.Img;
                    if (getAnime != null && getAnime.Length != 0)
                    {
                        System.IO.File.Delete(webHostEnvironment.WebRootPath + getAnime);
                    }
                }
            }
            var response = animeService.add(anime);
            return Ok(response);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("/getAnimes")]
        public IActionResult getAnimes()
        {
            var response = animeService.getList();
            return Ok(response);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("/getPaginatedAnime/{pageNo}/{showCount}")]
        public IActionResult getPaginatedAnime(int pageNo = 1, int showCount = 10)
        {
            var response = animeService.getPaginatedAnime(pageNo, showCount);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnime/{animeUrl}")]
        public IActionResult getAnime(string animeUrl)
        {
            var response = animeService.get(x => x.SeoUrl == animeUrl);
            return Ok(response);
        }
        [HttpDelete]
        [Route("/deleteAnime/{id}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteAnime(int id)
        {
            var response = animeService.delete(x => x.ID == id);
            return Ok(response);
        }
        #endregion

    }
}

