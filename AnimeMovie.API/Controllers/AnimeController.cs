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
    public class AnimeController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAnimeService animeService;
        private readonly IRatingsService ratingsService;
        private readonly IAnimeSeasonService animeSeasonService;
        private readonly IAnimeSeasonMusicService animeSeasonMusicService;
        private readonly IAnimeEpisodesService animeEpisodesService;
        private readonly IEpisodesService episodesService;
        private readonly ICategoryTypeService categoryTypeService;
        private readonly IAnimeImageService animeImageService;
        private readonly IContentNotificationService contentNotificationService;
        private readonly ILikeService likeService;
        private readonly IMangaService mangaService;
        private readonly IAnimeListService animeListService;
        private readonly ICommentsService commentsService;
        public AnimeController(IWebHostEnvironment webHost, IMangaService manga,
            IAnimeService anime, IRatingsService ratings, IAnimeSeasonService animeSeason,
            IAnimeSeasonMusicService seasonMusic, IAnimeEpisodesService animeEpisodes, IEpisodesService episodes,
            ILikeService like, IAnimeListService animeList, ICommentsService comments,
            ICategoryTypeService categoryType, IAnimeImageService animeImage, IContentNotificationService contentNotification)
        {
            commentsService = comments;
            mangaService = manga;
            animeListService = animeList;
            likeService = like;
            contentNotificationService = contentNotification;
            animeImageService = animeImage;
            animeEpisodesService = animeEpisodes;
            categoryTypeService = categoryType;
            animeSeasonMusicService = seasonMusic;
            webHostEnvironment = webHost;
            animeService = anime;
            ratingsService = ratings;
            animeSeasonService = animeSeason;
            episodesService = episodes;
        }
        #region Anime
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addAutoEpisodes/{start}/{end}/{animeID}/{seasonID}")]
        public IActionResult addAutoEpisode(int start, int end, int animeID, int seasonID)
        {
            List<AnimeEpisodes> animeEpisodes = new List<AnimeEpisodes>();
            var response = new ServiceResponse<AnimeEpisodes>();
            for (int i = start; i <= end; i++)
            {
                animeEpisodes.Add(animeEpisodesService.add(new AnimeEpisodes()
                {
                    AnimeID = animeID,
                    SeasonID = seasonID,
                    EpisodeName = i + ". Bölüm",
                    EpisodeDescription = "Bölüm açıklamasını giriniz"
                }).Entity);
            }
            response.List = animeEpisodes;
            response.Count = animeEpisodes.Count;
            response.IsSuccessful = true;
            return Ok(response);
        }

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
                    anime.Img = "/anime/" + guid + animeImg.FileName;
                }
            }
            var response = animeService.add(anime);
            if (anime.SeasonCount != 0 && response.IsSuccessful)
            {
                for (int i = 1; i <= anime.SeasonCount; i++)
                {
                    animeSeasonService.add(new AnimeSeason()
                    {
                        AnimeID = response.Entity.ID,
                        SeasonName = $"{i}.Sezon"
                    });
                }
            }

            return Ok(response);
        }
        [HttpPut]
        [Route("/updateAnimeImage/{ID}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateAnimeImage([FromForm] IFormFile animeImg, int ID)
        {
            var anime = animeService.get(x => x.ID == ID).Entity;
            if (animeImg != null && animeImg.Length != 0 && anime != null)
            {
                string guid = Guid.NewGuid().ToString();
                string patch = webHostEnvironment.WebRootPath + "/anime/";
                using (FileStream fs = System.IO.File.Create(patch + guid + animeImg.FileName))
                {
                    animeImg.CopyTo(fs);
                    fs.Flush();

                    var getAnime = anime.Img;
                    anime.Img = "/anime/" + guid + animeImg.FileName;
                    if (getAnime != null && getAnime.Length != 0)
                    {
                        System.IO.File.Delete(webHostEnvironment.WebRootPath + getAnime);
                    }
                    var response = animeService.update(anime);
                    return Ok(response);
                }
            }
            return BadRequest();
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
            var response = animeService.update(anime);
            return Ok(response);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("/getAdminAnimes")]
        public IActionResult getAnimes()
        {
            var list = animeService.getList();
            var response = new ServiceResponse<AnimeModels>();
            if (list.Count != 0)
            {
                List<AnimeModels> animeModels = new List<AnimeModels>();
                foreach (var anime in list.List)
                {
                    AnimeModels animeModel = new AnimeModels();

                    animeModel.Anime = anime;
                    var animeEpisodes = animeEpisodesService.getList(x => x.AnimeID == anime.ID).List.ToList();
                    animeModel.AnimeEpisodes = animeEpisodes;
                    animeModel.AnimeSeasons = animeSeasonService.getList(x => x.AnimeID == anime.ID).List.ToList();
                    animeModel.Categories = categoryTypeService.getList(x => x.Type == Entites.Type.Anime && x.ContentID == anime.ID).List.ToList();
                    animeModel.Arrangement = 1;
                    animeModel.Comments = commentsService.getList(x => x.Type == Entites.Type.Anime && x.ContentID == anime.ID).List.ToList();
                    animeModel.ContentNotification = contentNotificationService.get(x => x.ContentID == anime.ID && x.Type == Entites.Type.Anime).Entity;
                    animeModel.LikeCount = likeService.getList(x => x.ContentID == anime.ID && x.Type == Entites.Type.Anime).List.ToList().Count;
                    animeModel.ViewsCount = animeListService.getList(x => x.AnimeID == anime.ID && x.AnimeStatus == AnimeStatus.IWatched).List.ToList().Count;
                    animeModel.Manga = mangaService.get(x => x.AnimeID == anime.ID).Entity;
                    var ratingCount = ratingsService.getList(x => x.AnimeID == anime.ID).List.ToList().Count;
                    animeModel.Rating = (ratingCount / 10) == 0 ? 1 : ratingCount / 10;

                    animeModels.Add(animeModel);
                }
                response.List = animeModels;
                response.IsSuccessful = true;

            }
            response.Count = list.Count;
            return Ok(response);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("/getAnimes/{pageNo}/{showCount}")]
        public IActionResult getAnimes(int pageNo, int showCount)
        {
            var list = animeService.getPaginatedAnime(pageNo, showCount);
            var response = new ServiceResponse<AnimeModels>();
            if (list.Count != 0)
            {
                List<AnimeModels> animeModels = new List<AnimeModels>();
                foreach (var anime in list.List)
                {
                    AnimeModels animeModel = new AnimeModels();

                    animeModel.Anime = anime;
                    var animeEpisodes = animeEpisodesService.getList(x => x.AnimeID == anime.ID).List.ToList();
                    animeModel.AnimeEpisodes = animeEpisodes;
                    animeModel.AnimeSeasons = animeSeasonService.getList(x => x.AnimeID == anime.ID).List.ToList();
                    animeModel.Categories = categoryTypeService.getList(x => x.Type == Entites.Type.Anime && x.ContentID == anime.ID).List.ToList();
                    animeModel.Arrangement = 1;
                    animeModel.Comments = commentsService.getList(x => x.Type == Entites.Type.Anime && x.ContentID == anime.ID).List.ToList();
                    animeModel.ContentNotification = contentNotificationService.get(x => x.ContentID == anime.ID && x.Type == Entites.Type.Anime).Entity;
                    animeModel.LikeCount = likeService.getList(x => x.ContentID == anime.ID && x.Type == Entites.Type.Anime).List.ToList().Count;
                    animeModel.ViewsCount = animeListService.getList(x => x.AnimeID == anime.ID && x.AnimeStatus == AnimeStatus.IWatched).List.ToList().Count;
                    animeModel.Manga = mangaService.get(x => x.AnimeID == anime.ID).Entity;
                    var ratingCount = ratingsService.getList(x => x.AnimeID == anime.ID).List.ToList().Count;
                    animeModel.Rating = (ratingCount / 10) == 0 ? 1 : ratingCount / 10;

                    animeModels.Add(animeModel);
                }
                response.List = animeModels;
                response.IsSuccessful = true;

            }
            response.Count = list.Count;
            return Ok(response);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("/getSearchAnime/{search}")]
        public IActionResult getSearchAnimes(string search)
        {
            var response = animeService.getList(x => x.AnimeName.ToLower().Contains(search.ToLower()));
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
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getAnime/{animeUrl}")]
        public IActionResult getAnime(string animeUrl)
        {
            var userID = Handler.UserID(HttpContext);
            var getAnime = animeService.get(x => x.SeoUrl == animeUrl);
            if (getAnime.Entity != null)
            {
                AnimeModels animeModel = new AnimeModels();
                var response = new ServiceResponse<AnimeModels>();
                animeModel.AnimeRating = ratingsService.get(x => x.UserID == userID).Entity;
                animeModel.Anime = getAnime.Entity;
                var animeEpisodes = animeEpisodesService.getList(x => x.AnimeID == getAnime.Entity.ID).List.ToList();
                animeModel.Like = likeService.get(x => x.UserID == userID && x.ContentID == getAnime.Entity.ID && x.Type == Entites.Type.Anime).Entity;
                animeModel.AnimeEpisodes = animeEpisodes;
                animeModel.AnimeSeasonMusics = animeSeasonMusicService.getList(x => x.SeasonID == getAnime.Entity.ID).List.ToList();
                animeModel.AnimeSeasons = animeSeasonService.getList(x => x.AnimeID == getAnime.Entity.ID).List.ToList();
                animeModel.Categories = categoryTypeService.getList(x => x.Type == Entites.Type.Anime && x.ContentID == getAnime.Entity.ID).List.ToList();
                animeModel.Arrangement = 1;
                animeModel.ContentNotification = contentNotificationService.get(x => x.ContentID == getAnime.Entity.ID && x.Type == Entites.Type.Anime).Entity;
                animeModel.LikeCount = likeService.getList(x => x.ContentID == getAnime.Entity.ID && x.Type == Entites.Type.Anime).List.ToList().Count;
                animeModel.ViewsCount = animeListService.getList(x => x.AnimeID == getAnime.Entity.ID && x.AnimeStatus == AnimeStatus.IWatched).List.ToList().Count;
                animeModel.Manga = mangaService.get(x => x.AnimeID == getAnime.Entity.ID).Entity;
                animeModel.animeLists = animeListService.getList(x => x.AnimeID == getAnime.Entity.ID && x.UserID == userID).List.ToList();
                animeModel.AnimeImages = animeImageService.getList(x => x.AnimeID == getAnime.Entity.ID).List.ToList();
                List<Episodes> episodeList = new List<Episodes>();
                foreach (var episode in animeEpisodes)
                {
                    foreach (var content in episodesService.getList(x => x.EpisodeID == episode.ID).List)
                    {
                        episodeList.Add(content);
                    }
                }
                animeModel.Episodes = episodeList;
                var ratingCount = ratingsService.getList(x => x.AnimeID == getAnime.Entity.ID).List.ToList().Count;
                animeModel.Rating = ratingCount / 10;





                response.Entity = animeModel;
                response.IsSuccessful = true;

                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getAnimeByID/{animeID}")]
        public IActionResult getAnimeByID(int animeID)
        {
            var response = animeService.get(x => x.ID == animeID);
            return Ok(response);
        }
        [HttpDelete]
        [Route("/deleteAnimes")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteAnimes([FromBody] List<int> animes)
        {
            if (animes != null && animes.Count != 0)
            {
                foreach (var anime in animes)
                {
                    var seasons = animeSeasonService.getList(x => x.AnimeID == anime);
                    var categories = categoryTypeService.getList(x => x.ContentID == anime && x.Type == Entites.Type.Anime);
                    if (categories.Count != 0 && categories.List != null)
                    {
                        foreach (var category in categories.List)
                        {
                            categoryTypeService.delete(x => x.ID == category.ID);
                        }
                    }
                    if (seasons.List != null && seasons.Count != 0)
                    {
                        foreach (var season in seasons.List)
                        {
                            var seasonMusics = animeSeasonMusicService.getList(x => x.SeasonID == season.ID);
                            if (seasonMusics.List != null && seasonMusics.Count != 0)
                            {
                                foreach (var seasonMusic in seasonMusics.List)
                                {
                                    animeSeasonMusicService.delete(x => x.ID == seasonMusic.ID);
                                }
                            }

                            var animeEpisodes = animeEpisodesService.getList(x => x.SeasonID == season.ID);
                            if (animeEpisodes.Count != 0 && animeEpisodes.List != null)
                            {
                                foreach (var animeEpisode in animeEpisodes.List)
                                {
                                    var episodes = episodesService.getList(x => x.EpisodeID == animeEpisode.ID);
                                    if (episodes.List != null && episodes.Count != 0)
                                    {
                                        foreach (var episode in episodes.List)
                                        {
                                            episodesService.delete(x => x.ID == episode.ID);
                                        }
                                    }
                                    animeEpisodesService.delete(x => x.ID == animeEpisode.ID);
                                }
                            }
                            animeSeasonService.delete(x => x.ID == season.ID);
                        }
                    }
                    var response = animeService.delete(x => x.ID == anime);
                }
            }

            return Ok();
        }
        [HttpDelete]
        [Route("/deleteAnime/{id}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteAnime(int id)
        {

            var seasons = animeSeasonService.getList(x => x.AnimeID == id);
            var categories = categoryTypeService.getList(x => x.ContentID == id && x.Type == Entites.Type.Anime);
            if (categories.Count != 0 && categories.List != null)
            {
                foreach (var category in categories.List)
                {
                    categoryTypeService.delete(x => x.ID == category.ID);
                }
            }
            if (seasons.List != null && seasons.Count != 0)
            {
                foreach (var season in seasons.List)
                {
                    var seasonMusics = animeSeasonMusicService.getList(x => x.SeasonID == season.ID);
                    if (seasonMusics.List != null && seasonMusics.Count != 0)
                    {
                        foreach (var seasonMusic in seasonMusics.List)
                        {
                            animeSeasonMusicService.delete(x => x.ID == seasonMusic.ID);
                        }
                    }

                    var animeEpisodes = animeEpisodesService.getList(x => x.SeasonID == season.ID);
                    if (animeEpisodes.Count != 0 && animeEpisodes.List != null)
                    {
                        foreach (var animeEpisode in animeEpisodes.List)
                        {
                            var episodes = episodesService.getList(x => x.EpisodeID == animeEpisode.ID);
                            if (episodes.List != null && episodes.Count != 0)
                            {
                                foreach (var episode in episodes.List)
                                {
                                    episodesService.delete(x => x.ID == episode.ID);
                                }
                            }
                            animeEpisodesService.delete(x => x.ID == animeEpisode.ID);
                        }
                    }
                    animeSeasonService.delete(x => x.ID == season.ID);
                }
            }
            var response = animeService.delete(x => x.ID == id);
            return Ok(response);
        }
        #endregion
        #region Anime Image
        [HttpPost]
        [Route("/addAnimeImage/{animeID}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addAnimeImage([FromForm] List<IFormFile> files, int animeID)
        {
            List<AnimeImages> animeImages = new List<AnimeImages>();
            var response = new ServiceResponse<AnimeImages>();
            if (files != null && files.Count != 0)
            {
                foreach (var file in files)
                {
                    string guid = Guid.NewGuid().ToString();
                    var patch = webHostEnvironment.WebRootPath + "/anime/";
                    using (FileStream fs = System.IO.File.Create(patch + guid + file.FileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    var entity = animeImageService.add(new AnimeImages()
                    {
                        AnimeID = animeID,
                        Img = "/anime/" + guid + file.FileName,
                    }).Entity;
                    animeImages.Add(entity);
                }               
            }
            response.Count = animeImages.Count;
            response.IsSuccessful = true;
            response.List = animeImages;
            return Ok(response);
        }
        [HttpDelete]
        [Route("/deleteAnimeImage/{id}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteAnimeImage(int id)
        {
            var get = animeImageService.get(x => x.ID == id).Entity;
            if (get != null)
            {
                System.IO.File.Delete(webHostEnvironment.WebRootPath + get.Img);
                var response = animeImageService.delete(x => x.ID == id);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getAnimeImageList/{animeID}")]
        public IActionResult getAnimeImageList(int animeID)
        {
            var response = animeImageService.getList(x => x.AnimeID == animeID);
            return Ok(response);
        }
        #endregion
    }
}

