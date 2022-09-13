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

    public class AnimeSeasonController : Controller
    {
        private readonly IAnimeSeasonService animeSeasonService;
        private readonly IAnimeSeasonMusicService animeSeasonMusicService;
        private readonly IEpisodesService episodesService;
        private readonly IAnimeEpisodesService animeEpisodesService;
        public AnimeSeasonController(
            IAnimeSeasonService animeSeason,
            IAnimeSeasonMusicService animeSeasonMusic,
            IEpisodesService episodes,
            IAnimeEpisodesService animeEpisodes
            )
        {
            animeSeasonMusicService = animeSeasonMusic;
            animeSeasonService = animeSeason;
            episodesService = episodes;
            animeEpisodesService = animeEpisodes;
        }
        #region AnimeSeason
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addAnimeSeason")]
        public IActionResult addAnimeSeason([FromBody] AnimeSeason animeSeason)
        {
            if (animeSeason.AnimeID != 0 && animeSeason.SeasonName.Length != 0)
            {
                var response = animeSeasonService.add(animeSeason);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateAnimeSeason")]
        public IActionResult updateAnimeSeason([FromBody] AnimeSeason animeSeason)
        {
            if (animeSeason.AnimeID != 0 && animeSeason.SeasonName.Length != 0 && animeSeason.ID != 0)
            {
                var response = animeSeasonService.update(animeSeason);
                return Ok(response);
            }
            return BadRequest();
        }
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [HttpDelete]
        [Route("/deleteAnimeSeasons/{id}")]
        public IActionResult deleteAnimeSeasons(int id)
        {
            var response = animeSeasonService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeSeasons")]
        public IActionResult getAnimeSeasons()
        {
            var response = animeSeasonService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeSeasonsByAnimeID/{animeID}")]
        public IActionResult getAnimeSeasons(int animeID)
        {
            var response = animeSeasonService.getList(x => x.AnimeID == animeID);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeSeason/{id}")]
        public IActionResult getAnimeSeason(int id)
        {
            var response = animeSeasonService.get(x => x.ID == id);
            return Ok(response);
        }
        #endregion

        #region AnimeSeasonMusic
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addAnimeSeasonMusic")]
        public IActionResult addAnimeSeasonMusic([FromBody] AnimeSeasonMusic seasonMusic)
        {
            var response = animeSeasonMusicService.add(seasonMusic);
            return Ok(response);
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateAnimeSeasonMusic")]
        public IActionResult updateAnimeSeasonMusic([FromBody] AnimeSeasonMusic seasonMusic)
        {
            if (seasonMusic.MusicName.Length != 0 && seasonMusic.ID != 0)
            {
                var response = animeSeasonMusicService.add(seasonMusic);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/getAnimeSeasonMusic/{seasonID}")]
        public IActionResult getAnimeSeasonMusicByAnimeID(int seasonID)
        {
            var response = animeSeasonMusicService.getList(x => x.SeasonID == seasonID);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteAnimeSeasonMusic/{id}")]
        public IActionResult deleteAnimeSeasonMusic(int id)
        {
            var response = animeSeasonMusicService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/getAnimeSeasonMusics")]
        public IActionResult getAnimeSeasonMusics()
        {
            var response = animeSeasonMusicService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/getAnimeSeasonMusic/{id}")]
        public IActionResult getAnimeSeasonMusic(int id)
        {
            var response = animeSeasonMusicService.get(x => x.ID == id);
            return Ok(response);
        }
        #endregion

        #region AnimeEpisodes
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addAnimeEpisodes")]
        public IActionResult addAnimeEpisodes([FromBody] AnimeEpisodes animeEpisodes)
        {
            if (animeEpisodes.EpisodeName.Length != 0 && animeEpisodes.SeasonID != 0)
            {
                var response = animeEpisodesService.add(animeEpisodes);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateAnimeEpisodes")]
        public IActionResult updateAnimeEpisodes([FromBody] AnimeEpisodes animeEpisodes)
        {
            if (animeEpisodes.EpisodeName.Length != 0 && animeEpisodes.SeasonID != 0 && animeEpisodes.ID != 0)
            {
                var response = animeEpisodesService.update(animeEpisodes);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getAnimeEpisodes")]
        public IActionResult getAnimeEpisodes()
        {
            var response = animeEpisodesService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeEpisodesBySeasonID/{seasonID}")]
        public IActionResult getAnimeEpisodesBySeasonID(int seasonID)
        {
            var response = animeEpisodesService.getList(x => x.SeasonID == seasonID);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeEpisodesBySeasonID/{id}")]
        public IActionResult getAnimeEpisodesByID(int id)
        {
            var response = animeEpisodesService.getList(x => x.ID == id);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteAnimeEpisode/{id}")]
        public IActionResult deleteAnimeEpisode(int id)
        {
            var response = animeEpisodesService.getList(x => x.ID == id);
            return Ok(response);
        }
        #endregion

        #region Episodes
        [HttpPost]
        [Route("/addEpisodes")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addEpisodes([FromBody] Episodes episodes)
        {
            if (episodes.EpisodeID != 0)
            {
                var response = episodesService.add(episodes);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("/updateEpisode")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateEpisode([FromBody] Episodes episodes)
        {
            if (episodes.EpisodeID != 0 && episodes.ID != 0)
            {
                var response = episodesService.add(episodes);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getEpisodeByID/{id}")]
        public IActionResult getEpisodeByID(int id)
        {
            var response = episodesService.get(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getEpisodeByEpisodeID/{id}")]
        public IActionResult getEpisodeByEpisodeID(int episodeID)
        {
            var response = episodesService.get(x => x.EpisodeID == episodeID);
            return Ok(response);
        }
        #endregion
    }
}

