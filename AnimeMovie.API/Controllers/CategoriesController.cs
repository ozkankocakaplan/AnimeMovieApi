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
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        public CategoriesController(ICategoriesService categories)
        {
            categoriesService = categories;
        }
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addCategory")]
        public IActionResult addCategory([FromBody] Categories category)
        {
            if (category.Name.Length != 0)
            {
                var response = categoriesService.add(category);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateCategory")]
        public IActionResult updateCategory([FromBody] Categories category)
        {
            if (category.Name.Length != 0 && category.ID != 0)
            {
                var response = categoriesService.add(category);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteCategory/{id}")]
        public IActionResult deleteCategory(int id)
        {
            var response = categoriesService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getCategory/{id}")]
        public IActionResult getCategory(int id)
        {
            var response = categoriesService.get(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getCategories")]
        public IActionResult getCategories()
        {
            var response = categoriesService.getList();
            return Ok(response);
        }
    }
}

