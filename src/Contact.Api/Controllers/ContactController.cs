using Contacts.Domain.Contacts.Services;
using Contacts.Domain.Contacts.VOs;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        [HttpGet]
        public ActionResult<IList<ContactVO>> List()
        {
            try
            {
                var result = _contactService.List();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("ByDDD/{ddd}")]
        public ActionResult<IList<ContactVO>> ListByDDD(string ddd)
        {
            try
            {
                var result = _contactService.ListByDDD(ddd);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public IActionResult Create(ContactVO contact)
        {
            try
            {
                _contactService.Create(contact);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        public IActionResult Update(ContactVO contact)
        {
            try
            {
                _contactService.Update(contact);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _contactService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
