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
        private readonly IAnimeSeasonService animeSeasonService;
        private readonly IAnimeSeasonMusicService animeSeasonMusicService;
        private readonly IAnimeEpisodesService animeEpisodesService;
        private readonly IEpisodesService episodesService;
        private readonly ICategoryTypeService categoryTypeService;
        public AnimeController(IWebHostEnvironment webHost,
            IAnimeService anime, IAnimeRatingService animeRating, IAnimeSeasonService animeSeason,
            IAnimeSeasonMusicService seasonMusic, IAnimeEpisodesService animeEpisodes, IEpisodesService episodes,
            ICategoryTypeService categoryType)
        {
            animeEpisodesService = animeEpisodes;
            categoryTypeService = categoryType;
            animeSeasonMusicService = seasonMusic;
            webHostEnvironment = webHost;
            animeService = anime;
            animeRatingService = animeRating;
            animeSeasonService = animeSeason;
            episodesService = episodes;
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
        [HttpGet]
        [Route("/getAnimeByID/{animeID}")]
        public IActionResult getAnimeByID(int animeID)
        {
            var response = animeService.get(x => x.ID == animeID);
            return Ok(response);
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
                }
            }
            var response = animeService.delete(x => x.ID == id);
            return Ok(response);
        }
        #endregion

    }
}

