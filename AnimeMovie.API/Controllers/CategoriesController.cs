using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Type = AnimeMovie.Entites.Type;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimeMovie.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICategoryTypeService categoryTypeService;
        public CategoriesController(ICategoriesService categories, ICategoryTypeService categoryType)
        {
            categoriesService = categories;
            categoryTypeService = categoryType;
        }
        #region Category
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addCategory")]
        public IActionResult addCategory([FromBody] Categories category)
        {
            if (category != null && category.Name.Length != 0)
            {
                var response = categoriesService.add(category);
                return Ok(response);
            }
            return Ok();
        }
        [HttpPut]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateCategory")]
        public IActionResult updateCategory([FromBody] Categories category)
        {
            if (category != null && category.Name.Length != 0 && category.ID != 0)
            {
                var response = categoriesService.update(category);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteCategory/{id}")]
        public IActionResult deleteCategory(int id)
        {
            var response = categoriesService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpDelete]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/deleteCategories")]
        public IActionResult deleteCategories([FromBody] List<int> categories)
        {
            foreach (var category in categories)
            {
                var response = categoriesService.delete(x => x.ID == category);
            }

            return Ok();
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
        #endregion

        #region CategoryType
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/addCategoryType")]
        public IActionResult addCategoryType([FromBody] List<CategoryType> categories)
        {
            foreach (var category in categories)
            {
                var response = categoryTypeService.add(category);
            }
            return Ok();
        }
        [HttpPost]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        [Route("/updateCategoryType/{contentID}")]
        public IActionResult updateCategoryType([FromBody] List<CategoryType> categories, int contentID)
        {
            var list = categoryTypeService.getList(x => x.ContentID == contentID);
            if (list.Count != 0)
            {
                foreach (var item in list.List)
                {
                    var check = categories.Where(x => x.ID == item.ID);
                    if (check == null)
                    {
                        categoryTypeService.delete(x => x.ID == item.ID);
                    }
                }
            }
            return Ok();
        }
        [HttpGet]
        [Route("/getCategoryType/{contentID}/{type}")]
        public IActionResult getCategoryType(int contentID, int type)
        {
            var response = categoryTypeService.getList(x => x.ContentID == contentID && x.Type == (Type)type);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getCategoryTypes")]
        public IActionResult getCategoryTypes()
        {
            var response = categoryTypeService.getList();
            return Ok(response);
        }
        #endregion
    }
}

