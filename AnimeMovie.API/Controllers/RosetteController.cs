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
    public class RosetteController : Controller
    {
        private readonly IRosetteService rosetteService;
        private readonly IUserRosetteService userRosetteService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public RosetteController(IRosetteService rosette, IUserRosetteService userRosette, IWebHostEnvironment webHost)
        {
            rosetteService = rosette;
            userRosetteService = userRosette;
            webHostEnvironment = webHost;
        }
        #region Rosette
        [HttpPost]
        [Route("/addRosette")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addRosette([FromForm] IFormFile img, Rosette rosette)
        {
            if (rosette.Name.Length != 0)
            {
                if (img != null && img.Length != 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var patch = webHostEnvironment.WebRootPath + "/image/";
                    using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                    {
                        img.CopyTo(fs);
                        fs.Flush();
                        rosette.Img = "/image/" + guid + img.FileName;
                    }
                }
                var response = rosetteService.add(rosette);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("/updateRosette")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateRosette([FromForm] IFormFile img, Rosette rosette)
        {
            if (rosette.Name.Length != 0 && rosette.ID != 0)
            {
                if (img != null && img.Length != 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var patch = webHostEnvironment.WebRootPath + "/image/";
                    using (FileStream fs = System.IO.File.Create(patch + guid + img.FileName))
                    {
                        img.CopyTo(fs);
                        fs.Flush();
                        rosette.Img = "/image/" + guid + img.FileName;
                    }
                }
                var response = rosetteService.update(rosette);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteRosette")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteRosette(int id)
        {
            var response = rosetteService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getRosettes")]
        public IActionResult getRosettes()
        {
            var response = rosetteService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getRosette/{id}")]
        public IActionResult getRosette(int id)
        {
            var response = rosetteService.get(x => x.ID == id);
            return Ok(response);
        }
        #endregion

        #region UserRosette
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addUserRosette")]
        public IActionResult addUserRosette([FromBody] UserRosette userRosette)
        {
            if (userRosette.UserID != 0 && userRosette.RosetteID != 0)
            {
                var response = userRosetteService.add(userRosette);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateUserRosette")]
        public IActionResult updateUserRosette([FromBody] UserRosette userRosette)
        {
            if (userRosette.UserID != 0 && userRosette.RosetteID != 0 && userRosette.ID != 0)
            {
                var response = userRosetteService.update(userRosette);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteUserRosette")]
        public IActionResult deleteUserRosette(int id)
        {
            if (id != 0)
            {
                var response = userRosetteService.delete(x => x.ID == id);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getUserRosettes")]
        public IActionResult getUserRosettes()
        {
            var response = userRosetteService.getList();
            return Ok(response);
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getUserRosetteByID/{id}")]
        public IActionResult getUserRosetteByID(int id)
        {
            var response = userRosetteService.get(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Roles(Roles = RolesAttribute.All)]
        [Route("/getUserRosetteByUserID/{userID}")]
        public IActionResult getUserRosetteByUserID(int userID)
        {
            var response = userRosetteService.get(x => x.UserID == userID);
            return Ok(response);
        }
        #endregion

    }
}

