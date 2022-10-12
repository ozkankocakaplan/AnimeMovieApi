using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMovie.Business;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AnimeMovie.API.Controllers
{
    public class MangaController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMangaService mangaService;
        private readonly IMangaEpisodesService mangaEpisodesService;
        private readonly IMangaEpisodeContentService mangaEpisodeContentService;
        public MangaController(IMangaService manga, IWebHostEnvironment webHost,
            IMangaEpisodeContentService mangaEpisodeContent, IMangaEpisodesService mangaEpisodes)
        {
            webHostEnvironment = webHost;
            mangaService = manga;
            mangaEpisodeContentService = mangaEpisodeContent;
            mangaEpisodesService = mangaEpisodes;
        }
        #region Manga
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addManga")]
        public IActionResult addManga([FromForm] IFormFile img, Manga manga)
        {
            if (img != null && img.Length != 0)
            {
                var guid = Guid.NewGuid().ToString();
                var patch = webHostEnvironment.WebRootPath + "/image/";
                using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    manga.Image = "/image/" + guid + img.FileName;
                }
            }
            var response = mangaService.add(manga);
            return Ok(response);
        }

        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateManga")]
        public IActionResult updateManga([FromForm] IFormFile img, Manga manga)
        {
            if (img != null && img.Length != 0)
            {
                var guid = Guid.NewGuid().ToString();
                var patch = webHostEnvironment.WebRootPath + "/image/";
                using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    manga.Image = "/image/" + guid + img.FileName;
                }
            }
            var response = mangaService.update(manga);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteManga/{mangaID}")]
        public IActionResult deleteManga(int mangaID)
        {
            var mangaEpisodes = mangaEpisodesService.getList(x => x.MangaID == mangaID);
            if (mangaEpisodes != null && mangaEpisodes.Count != 0)
            {
                foreach (var mangaEpisode in mangaEpisodes.List)
                {
                    var episodeContent = mangaEpisodeContentService.getList(x => x.EpisodeID == mangaEpisode.ID);
                    if (episodeContent != null && episodeContent.Count != 0)
                    {
                        foreach (var content in episodeContent.List)
                        {
                            mangaEpisodeContentService.delete(x => x.ID == content.ID);
                        }
                    }
                    mangaEpisodesService.delete(x => x.ID == mangaEpisode.ID);
                }
            }
            mangaService.delete(x => x.ID == mangaID);
            return Ok();
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteMangas")]
        public IActionResult deleteMangas([FromBody] List<int> mangas)
        {
            foreach (var manga in mangas)
            {
                var mangaEpisodes = mangaEpisodesService.getList(x => x.MangaID == manga);
                if (mangaEpisodes != null && mangaEpisodes.Count != 0)
                {
                    foreach (var mangaEpisode in mangaEpisodes.List)
                    {
                        var episodeContent = mangaEpisodeContentService.getList(x => x.EpisodeID == mangaEpisode.ID);
                        if (episodeContent != null && episodeContent.Count != 0)
                        {
                            foreach (var content in episodeContent.List)
                            {
                                mangaEpisodeContentService.delete(x => x.ID == content.ID);
                            }
                        }
                        mangaEpisodesService.delete(x => x.ID == mangaEpisode.ID);
                    }
                }
                mangaService.delete(x => x.ID == manga);

            }
            return Ok();
        }
        [HttpGet]
        [Route("/getMangas")]
        public IActionResult getMangas()
        {
            var response = mangaService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getSearchDetailsMangas/{text}")]
        public IActionResult getSearchDetailsMangas(string text)
        {
            var response = mangaService.getList(x => x.Name.ToLower().Contains(text.ToLower()));
            return Ok(response);
        }
        [HttpGet]
        [Route("/getPaginatedManga/{pageNo}/{showCount}")]
        public IActionResult getPaginatedManga(int pageNo, int showCount)
        {
            var response = mangaService.getPaginatedManga(pageNo, showCount);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getManga/{mangaUrl}")]
        public IActionResult getManga(string mangaUrl)
        {
            var response = mangaService.getList(x => x.SeoUrl == mangaUrl);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getMangaByID/{id}")]
        public IActionResult getMangaByID(int id)
        {
            var response = mangaService.get(x => x.ID == id);
            return Ok(response);
        }
        #endregion
        #region MangaEpisodes
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addMangaEpisodes")]
        public IActionResult addMangaEpisodes([FromBody] MangaEpisodes mangaEpisodes)
        {
            if (mangaEpisodes.MangaID != 0 && mangaEpisodes.Name.Length != 0)
            {
                var response = mangaEpisodesService.add(mangaEpisodes);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateMangaEpisodes")]
        public IActionResult updateMangaEpisodes([FromBody] MangaEpisodes mangaEpisodes)
        {
            if (mangaEpisodes.MangaID != 0 && mangaEpisodes.Name.Length != 0 && mangaEpisodes.ID != 0)
            {
                var response = mangaEpisodesService.update(mangaEpisodes);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getMangaEpisodesByMangaID/{mangaID}")]
        public IActionResult getMangaEpisodesByMangaID(int mangaID)
        {
            var response = mangaEpisodesService.getList(x => x.MangaID == mangaID);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getMangaEpisodes")]
        public IActionResult getMangaEpisodes()
        {
            var response = mangaEpisodesService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getMangaEpisode/{id}")]
        public IActionResult getMangaEpisode(int id)
        {
            var response = mangaEpisodesService.get(x => x.ID == id);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteMangaEpisode/{id}")]
        public IActionResult deleteMangaEpisode(int id)
        {
            var list = mangaEpisodeContentService.getList(x => x.EpisodeID == id).List;
            foreach (var item in list)
            {
                mangaEpisodeContentService.delete(x => x.ID == item.ID);
            }
            var response = mangaEpisodesService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteMangaEpisodes")]
        public IActionResult deleteMangaEpisodes([FromBody] List<int> episodes)
        {
            foreach (var episode in episodes)
            {
                var list = mangaEpisodeContentService.getList(x => x.EpisodeID == episode).List;
                foreach (var item in list)
                {
                    mangaEpisodeContentService.delete(x => x.ID == item.ID);
                }
                var response = mangaEpisodesService.delete(x => x.ID == episode);
            }

            return Ok();
        }
        #endregion
        #region MangaEpisodeContent
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addMangaEpisodeContent")]
        public IActionResult addMangaEpisodeContent([FromForm] IFormFile img, MangaEpisodeContent episodeContent)
        {
            if (img != null && img.Length != 0 && episodeContent.EpisodeID != 0)
            {
                var patch = webHostEnvironment.WebRootPath + "/Manga/";
                using (FileStream fs = System.IO.File.Create(patch + episodeContent.EpisodeID + "-" + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    episodeContent.ContentImage = "/Manga/" + episodeContent.EpisodeID + "-" + img.FileName;
                }


            }
            var response = mangaEpisodeContentService.add(episodeContent);
            return Ok(response);

        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateMangaEpisodeContent")]
        public IActionResult updateMangaEpisodeContent([FromForm] IFormFile img, MangaEpisodeContent episodeContent)
        {
            if (img != null && img.Length != 0 && episodeContent.ID != 0 && episodeContent.EpisodeID != 0)
            {
                var patch = webHostEnvironment.WebRootPath + "/manga/";
                using (FileStream fs = System.IO.File.Create(patch + episodeContent.EpisodeID + "-" + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    episodeContent.ContentImage = "/manga/" + episodeContent.EpisodeID + "-" + img.FileName;
                }
            }
            var response = mangaEpisodeContentService.update(episodeContent);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getMangaEpisodeContents/{episodeID}")]
        public IActionResult getMangaEpisodeContents(int episodeID)
        {
            var response = mangaEpisodeContentService.getList(x => x.EpisodeID == episodeID);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getMangaEpisodeContent/{id}")]
        public IActionResult getMangaEpisodeContent(int id)
        {
            var response = mangaEpisodeContentService.get(x => x.ID == id);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteMangaEpisodeContent/{episodeID}")]
        public IActionResult deleteMangaEpisodeContent(int episodeID)
        {
            var response = new ServiceResponse<int>();
            List<int> episodoContentID = new List<int>();
            var list = mangaEpisodeContentService.getList(x => x.EpisodeID == episodeID).List;
            foreach (var item in list)
            {
                episodoContentID.Add(item.ID);
                mangaEpisodeContentService.delete(x => x.ID == item.ID);
            }
            response.List = episodoContentID;
            response.Count = episodoContentID.Count;
            return Ok(response);
        }
        #endregion
    }
}

