using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AnimeMovie.API.Models;
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
        private readonly IMangaImageService mangaImageService;
        private readonly ILikeService likeService;
        private readonly ICategoryTypeService categoryTypeService;
        private readonly IRatingsService ratingsService;
        private readonly IMangaListService mangaListService;
        private readonly IAnimeService animeService;
        private readonly IContentNotificationService contentNotificationService;
        public MangaController(IMangaService manga, IWebHostEnvironment webHost,
            IContentNotificationService contentNotification,IAnimeService anime,
            ICategoryTypeService categoryType,IRatingsService ratings,IMangaListService mangaList,
            IMangaEpisodeContentService mangaEpisodeContent,ILikeService like, IMangaEpisodesService mangaEpisodes, IMangaImageService mangaImage)
        {
            animeService = anime;
            contentNotificationService = contentNotification;
            mangaListService = mangaList;
            ratingsService = ratings;
            categoryTypeService = categoryType;
            likeService = like;
            mangaImageService = mangaImage;
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
                var patch = webHostEnvironment.WebRootPath + "/manga/";
                using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    manga.Image = "/manga/" + guid + img.FileName;
                }
            }
            var response = mangaService.add(manga);
            return Ok(response);
        }
        [HttpPut]
        [Route("/updateMangaImage/{ID}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateMangaImage([FromForm] IFormFile mangaImg, int ID)
        {
            var manga = mangaService.get(x => x.ID == ID).Entity;
            if (mangaImg != null && mangaImg.Length != 0 && manga != null)
            {
                string guid = Guid.NewGuid().ToString();
                string patch = webHostEnvironment.WebRootPath + "/anime/";
                using (FileStream fs = System.IO.File.Create(patch + guid + mangaImg.FileName))
                {
                    mangaImg.CopyTo(fs);
                    fs.Flush();

                    var getManga = manga.Image;
                    manga.Image = "/anime/" + guid + mangaImg.FileName;
                    if (getManga != null && getManga.Length != 0)
                    {
                        System.IO.File.Delete(webHostEnvironment.WebRootPath + getManga);
                    }
                    var response = mangaService.update(manga);
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateManga")]
        public IActionResult updateManga([FromForm] IFormFile img, Manga manga)
        {
            if (img != null && img.Length != 0)
            {
                var guid = Guid.NewGuid().ToString();
                var patch = webHostEnvironment.WebRootPath + "/manga/";
                using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    manga.Image = "/manga/" + guid + img.FileName;
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
            var list = mangaService.getList();
            if(list.Count != 0)
            {
                var response = new ServiceResponse<MangaModels>();
                List<MangaModels> mangaModels = new List<MangaModels>();
                foreach (var manga in list.List)
                {
                    MangaModels mangaModel = new MangaModels();
                    mangaModel.Manga = manga;

                    mangaModel.Categories = categoryTypeService.getList(x => x.Type == Entites.Type.Manga && x.ContentID == manga.ID).List.ToList();
                    mangaModel.Arrangement = 1;
                    mangaModel.ContentNotification = contentNotificationService.get(x => x.ContentID == manga.ID && x.Type == Entites.Type.Manga).Entity;
                    mangaModel.LikeCount = likeService.getList(x => x.ContentID == manga.ID && x.Type == Entites.Type.Manga).List.ToList().Count;
                    mangaModel.ViewsCount = mangaListService.getList(x => x.MangaID == manga.ID && x.Status == MangaStatus.IRead).List.ToList().Count;
                    var ratingCount = ratingsService.getList(x => x.Type == Entites.Type.Manga && x.AnimeID == manga.ID).List.ToList().Count;
                    mangaModel.Rating = (ratingCount / 10) == 0 ? 1 : ratingCount / 10;
                    mangaModel.MangaEpisodeCount = mangaEpisodesService.getList(x=>x.MangaID == manga.ID).List.Count();
                    mangaModels.Add(mangaModel);
                }
                response.List = mangaModels;
                response.Count = mangaModels.Count;
                response.IsSuccessful = true;
                return Ok(response);
            }
            
            return BadRequest();
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
        [Roles(Roles = RolesAttribute.All)]
        [HttpGet]
        [Route("/getManga/{mangaUrl}")]
        public IActionResult getManga(string mangaUrl)
        {
            var userID = Handler.UserID(HttpContext);
            var manga = mangaService.get(x => x.SeoUrl == mangaUrl).Entity;
            var response = new ServiceResponse<MangaModels>();
            if(manga != null)
            {
               
                MangaModels mangaModel = new MangaModels();
                mangaModel.Manga = manga;
                mangaModel.MangaRating = ratingsService.get(x => x.UserID == userID).Entity;
                mangaModel.Like = likeService.get(x => x.UserID == userID && x.ContentID == manga.ID && x.Type == Entites.Type.Manga).Entity;
                mangaModel.Anime = animeService.get(x => x.ID == manga.AnimeID).Entity;
                mangaModel.Categories = categoryTypeService.getList(x => x.Type == Entites.Type.Manga && x.ContentID == manga.ID).List.ToList();
                mangaModel.Arrangement = 1;
                var mangaEpisodes = mangaEpisodesService.getList(x => x.MangaID == manga.ID).List.ToList();
                mangaModel.MangaEpisodes = mangaEpisodes;
                mangaModel.MangaLists = mangaListService.getList(x => x.MangaID == manga.ID && x.UserID == userID).List.ToList();
                mangaModel.ContentNotification = contentNotificationService.get(x => x.ContentID == manga.ID && x.Type == Entites.Type.Manga).Entity;
                mangaModel.LikeCount = likeService.getList(x => x.ContentID == manga.ID && x.Type == Entites.Type.Manga).List.ToList().Count;
                mangaModel.ViewsCount = mangaListService.getList(x => x.MangaID == manga.ID && x.Status == MangaStatus.IRead).List.ToList().Count;
                var ratingCount = ratingsService.getList(x => x.Type == Entites.Type.Manga && x.AnimeID == manga.ID).List.ToList().Count;
                mangaModel.Rating = (ratingCount / 10) == 0 ? 1 : ratingCount / 10;
                mangaModel.MangaEpisodeCount = mangaEpisodesService.getList(x => x.MangaID == manga.ID).List.Count();
                List<MangaEpisodeContent> episodeContents = new List<MangaEpisodeContent>();
                foreach (var episode in mangaEpisodes)
                {
                    foreach (var content in mangaEpisodeContentService.getList(x=>x.EpisodeID == episode.ID).List.ToList())
                    {
                        episodeContents.Add(content);
                    }
                }
                mangaModel.MangaEpisodeContents = episodeContents;
                response.Entity = mangaModel;
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ExceptionMessage = "Not Found";
                response.HasExceptionError = true;
            }
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
        #region Manga Image
        [HttpPost]
        [Route("/addMangaImage/{mangaID}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addMangaImage([FromForm] List<IFormFile> files, int mangaID)
        {
            if (files != null && files.Count != 0)
            {
                foreach (var file in files)
                {
                    string guid = Guid.NewGuid().ToString();
                    var patch = webHostEnvironment.WebRootPath + "/manga/";
                    using (FileStream fs = System.IO.File.Create(patch + guid + file.FileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    mangaImageService.add(new MangaImages()
                    {
                        MangaID = mangaID,
                        Img = "/manga/" + guid + file.FileName,
                    });
                }
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteMangaImage/{id}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteMangaImage(int id)
        {
            var get = mangaImageService.get(x => x.ID == id).Entity;
            if (get != null)
            {
                System.IO.File.Delete(webHostEnvironment.WebRootPath + get.Img);
                var response = mangaImageService.delete(x => x.ID == id);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getMangaImageList/{mangaID}")]
        public IActionResult getAnimeImageList(int mangaID)
        {
            var response = mangaImageService.getList(x => x.MangaID == mangaID);
            return Ok(response);
        }
        #endregion
    }
}

