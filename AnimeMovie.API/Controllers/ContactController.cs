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
    public class ContactController : Controller
    {
        private readonly IContactService contactService;
        private readonly IContactSubjectService contactSubjectService;
        public ContactController(IContactService contact, IContactSubjectService contactSubject)
        {
            contactService = contact;
            contactSubjectService = contactSubject;
        }
        #region Contact
        [HttpPost]
        [Route("/addContact")]
        public IActionResult addContact([FromBody] Contact contact)
        {
            if (contact.NameSurname.Length != 0 && contact.Subject.Length != 0 && contact.Message.Length != 0 && contact.Email.Length != 0)
            {
                var response = contactService.add(contact);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteContact/{id}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteContact(int id)
        {
            var response = contactService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getContacts/{pageNo}/{showCount}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult getContacts(int pageNo = 1, int showCount = 10)
        {
            var response = contactService.getListPagined(pageNo, showCount);
            return Ok(response);
        }
        #endregion
        #region ContactSubjet
        [HttpPost]
        [Route("/addContactSubject")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult addContactSubject([FromBody] ContactSubject subject)
        {
            if (subject.Subject.Length != 0)
            {
                var response = contactSubjectService.add(subject);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("/updateContactSubject")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult updateContactSubject([FromBody] ContactSubject subject)
        {
            if (subject.Subject.Length != 0)
            {
                var response = contactSubjectService.update(subject);
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("/deleteContactSubject/{id}")]
        [Roles(Roles = RolesAttribute.AdminOrModerator)]
        public IActionResult deleteContactSubject(int id)
        {
            var response = contactSubjectService.delete(x => x.ID == id);
            return Ok(response);
        }
        [HttpGet]
        [Route("/getContactSubjects")]
        public IActionResult getContactSubjects()
        {
            var response = contactSubjectService.getList();
            return Ok(response);
        }
        #endregion
    }
}

