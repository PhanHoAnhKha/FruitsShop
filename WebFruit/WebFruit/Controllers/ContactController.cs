using Microsoft.AspNetCore.Mvc;
using WebFruit.DTOs;
using WebFruit.Interfaces;
using WebFruit.Models;

namespace WebFruit.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] ContactDTO contact)
        {
            if (contact == null)
            {
                return BadRequest("Contact is null.");
            }

            _contactRepository.AddContact(contact);
            return Ok("Contact added successfully.");
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetAllContacts()
        {
            var contacts = _contactRepository.GetAllContacts();
            return Ok(contacts);
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmailSubscriptions>> GetAllSubscribers()
        {
            var subscribers = _contactRepository.GetAllSubscriber();
            return Ok(subscribers);
        }
    }
}
