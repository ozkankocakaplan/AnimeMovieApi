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

    public class HomeSliderController : Controller
    {
        private readonly IHomeSliderService homeSliderService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeSliderController(IHomeSliderService homeSlider, IWebHostEnvironment webHost)
        {
            homeSliderService = homeSlider;
            webHostEnvironment = webHost;
        }
        [HttpPost]
        [Route("/addHomeSlider")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addHomeSlider([FromForm] IFormFile img, HomeSlider homeSlider)
        {
            if (img != null && img.Length != 0)
            {
                var guid = Guid.NewGuid().ToString();
                var patch = webHostEnvironment.WebRootPath + "/image/";
                using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    homeSlider.Image = "/image/" + guid + img.FileName;
                }
            }
            var response = homeSliderService.add(homeSlider);
            return Ok(response);
        }
        [HttpPost]
        [Route("/updateHomeSlider")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateHomeSlider([FromForm] IFormFile img, HomeSlider homeSlider)
        {
            if (img != null && img.Length != 0)
            {
                var guid = Guid.NewGuid().ToString();
                var patch = webHostEnvironment.WebRootPath + "/image/";
                using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                {
                    img.CopyTo(fs);
                    fs.Flush();
                    homeSlider.Image = "/image/" + guid + img.FileName;
                }
            }
            var response = homeSliderService.add(homeSlider);
            return Ok(response);
        }
        [HttpPost]
        [Route("/deleteHomeSlider/{id}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteHomeSlider(int id)
        {
            var response = homeSliderService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getHomeSliders")]
        public IActionResult getHomeSliders()
        {
            var response = homeSliderService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getHomeSlider")]
        public IActionResult getHomeSlider(int id)
        {
            var response = homeSliderService.get(x => x.ID == id);
            return Ok(response);
        }

    }
}

