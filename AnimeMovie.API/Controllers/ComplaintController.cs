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
    public class ComplaintController : Controller
    {
        private readonly IComplaintListService complaintListService;
        private readonly IContentComplaintService contentComplaintService;
        public ComplaintController(IComplaintListService complaintList, IContentComplaintService contentComplaint)
        {
            complaintListService = complaintList;
            contentComplaintService = contentComplaint;
        }

        #region UserComplaint
        [HttpPost]
        [Route("/addComplaint")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addComplaint([FromBody] ComplaintList complaintList)
        {
            var userID = Handler.UserID(HttpContext);
            if (complaintList.ComplainantID == userID && complaintList.Description.Length != 0)
            {
                var response = complaintListService.add(complaintList);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/getComplaints")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult getComplaints()
        {
            var response = complaintListService.getComplaintListModels();
            return Ok(response);
        }
        [HttpGet]
        [Route("/getComplaintsByUserID")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult getComplaintsByUserID()
        {
            var userID = Handler.UserID(HttpContext);
            var response = complaintListService.getList(x => x.ComplainantID == userID);
            return Ok(response);
        }
        [HttpDelete]
        [Route("/deleteComplaint")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteComplaint([FromBody] List<int> list)
        {
            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    complaintListService.delete(x => x.ID == item);
                }
                return Ok();
            }
            return BadRequest();
        }
        #endregion
        #region ContentComplaint
        [HttpPost]
        [Route("/addContentComplaint")]
        [Roles(Roles = RolesAttribute.All)]
        public IActionResult addContentComplaint([FromBody] ContentComplaint contentComplaint)
        {
            var userID = Handler.UserID(HttpContext);
            if (contentComplaint.UserID == userID)
            {
                var response = contentComplaintService.add(contentComplaint);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteContentComplaint/{id}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteContentComplaint(int id)
        {
            var response = contentComplaintService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getContentComplaint/{pageNo}/{showCount}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult getContentComplaint(int pageNo = 1, int showCount = 10)
        {
            var response = contentComplaintService.getListPagined(pageNo, showCount);
            return Ok(response);
        }
        #endregion
    }
}

