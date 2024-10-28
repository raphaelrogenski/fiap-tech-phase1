using Contacts.Domain.Contacts.Services;
using Contacts.Domain.Contacts.VOs;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                return Ok(contactService.List());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("ByDDD/{ddd}")]
        public async Task<IActionResult> ListByDDD(string ddd)
        {
            try
            {
                return Ok(contactService.ListByDDD(ddd));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create(ContactVO contact)
        {
            try
            {
                contactService.Create(contact);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update(ContactVO contact)
        {
            try
            {
                contactService.Update(contact);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                contactService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
