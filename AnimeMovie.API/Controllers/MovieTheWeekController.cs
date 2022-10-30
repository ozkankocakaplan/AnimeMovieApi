using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMovie.API.Models;
using AnimeMovie.Business;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Mvc;


namespace AnimeMovie.API.Controllers
{

    public class AnimeOfTheWeekController : Controller
    {
        private readonly IMovieTheWeekService animeOfTheWeekService;
        private readonly IAnimeService animeService;
        private readonly IMangaService mangaService;
        private readonly IUsersService usersService;
        public AnimeOfTheWeekController(IMovieTheWeekService animeOfTheWeek
            , IUsersService users, IMangaService manga, IAnimeService anime)
        {
            usersService = users;
            animeOfTheWeekService = animeOfTheWeek;
            animeService = anime;
            mangaService = manga;
        }
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [HttpPost]
        [Route("/addMovieTheWeek")]
        public IActionResult addMovieTheWeek([FromBody] List<MovieTheWeek> lists)
        {
            foreach (var item in lists)
            {
                var check = animeOfTheWeekService.get(x => x.ContentID == item.ContentID && x.Type == item.Type);
                if (check.Entity == null)
                {
                    var response = animeOfTheWeekService.add(item);
                }

            }
            return Ok();
        }
        [HttpGet]
        [Route("/getMovieTheWeeks")]
        public IActionResult getMovieTheWeeks()
        {
            var list = animeOfTheWeekService.getList();
            if (list.Count != 0)
            {
                var response = new ServiceResponse<MovieTheWeekModels>();
                List<MovieTheWeekModels> movieTheWeeks = new List<MovieTheWeekModels>();
                foreach (var item in list.List)
                {
                    MovieTheWeekModels movieTheWeek = new MovieTheWeekModels(item);
                    if (item.Type == Entites.Type.Anime)
                    {
                        movieTheWeek.Anime = animeService.get(x => x.ID == item.ContentID).Entity;
                    }
                    else
                    {
                        movieTheWeek.Manga = mangaService.get(x => x.ID == item.ContentID).Entity;
                    }
                    movieTheWeek.Users = usersService.get(x => x.ID == item.UserID).Entity;
                    movieTheWeeks.Add(movieTheWeek);

                }
                response.List = movieTheWeeks;
                response.Count = movieTheWeeks.Count;
                response.IsSuccessful = true;
                return Ok(response);
            }
            return NoContent();
        }
        [HttpGet]
        [Route("/getMovieTheWeek/{id}")]
        public IActionResult getMovieTheWeek(int id)
        {
            var response = animeOfTheWeekService.get(x => x.ID == id);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteMovieTheWeek")]
        public IActionResult deleteMovieTheWeek([FromBody]List<int> lists)
        {
            if (lists != null && lists.Count != 0)
            {
                foreach (var item in lists)
                {
                    var response = animeOfTheWeekService.delete(x => x.ID == item);
                }
              
            }
            return Ok();
        }
    }
}

