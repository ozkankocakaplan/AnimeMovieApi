using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMovie.API.Models;
using AnimeMovie.Business;
using AnimeMovie.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimeMovie.API.Controllers
{
    public class ReportController : Controller
    {
        private readonly IAnimeService animeService;
        private readonly IMangaService mangaService;
        private readonly IRosetteService rosetteService;
        private readonly ICategoriesService categoriesService;
        private readonly IUsersService usersService;
        public ReportController(IAnimeService anime,
            IMangaService manga, IRosetteService rosette,
            ICategoriesService categories, IUsersService users)
        {
            usersService = users;
            animeService = anime;
            mangaService = manga;
            rosetteService = rosette;
            categoriesService = categories;
        }
        [HttpGet]
        [Route("/getDashboardReport")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult DashboardReport()
        {
            var response = new ServiceResponse<ReportModels>();
            ReportModels report = new ReportModels();
            report.AnimeCount = animeService.getList().Count;
            report.CategoriesCount = categoriesService.getList().Count;
            report.MangaCount = mangaService.getList().Count;
            report.RosetteCount = rosetteService.getList().Count;
            report.UserCount = usersService.getList(x => x.RoleType == Entites.RoleType.User && x.isBanned == false).Count;
            response.Entity = report;
            response.IsSuccessful = true;
            return Ok(response);
        }
    }
}

