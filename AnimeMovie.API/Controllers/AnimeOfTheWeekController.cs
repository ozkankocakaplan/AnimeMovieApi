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

    public class AnimeOfTheWeekController : Controller
    {
        private readonly IAnimeOfTheWeekService animeOfTheWeekService;
        public AnimeOfTheWeekController(IAnimeOfTheWeekService animeOfTheWeek)
        {
            animeOfTheWeekService = animeOfTheWeek;
        }
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [HttpPost]
        [Route("/addAnimeOfTheWeek")]
        public IActionResult addAnimeOfTheWeek([FromBody] AnimeOfTheWeek animeOfThe)
        {
            var response = animeOfTheWeekService.add(animeOfThe);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeOfTheWeeks")]
        public IActionResult getAnimeOfTheWeeks()
        {
            var response = animeOfTheWeekService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getAnimeOfTheWeeks/{id}")]
        public IActionResult getAnimeOfTheWeek(int id)
        {
            var response = animeOfTheWeekService.get(x => x.ID == id);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/getAnimeOfTheWeeks")]
        public IActionResult deleteAnimeOfTheWeek(int id)
        {
            var response = animeOfTheWeekService.delete(x => x.ID == id);
            return Ok(response);
        }
    }
}

