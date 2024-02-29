using ContactApiDTO.Models;
using ContactApiDTO.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactApiDTO.Controllers
{
    //[Route("api/[controller]")]
    [Route("contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IRepository<Contact> _repository;

        public ContactController(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        //GET /contacts
        [HttpGet]
        //public ActionResult<List<Contact>> GetAll()
        public IActionResult GetAll(string? startLastName) // ne pas oublier le "?" pour rendre le request param faculattif
        //public IActionResult GetAll([FromQuery] string? startLastName)
        {
            if (startLastName == null)
                return Ok(_repository.GetAll());

            return Ok(
                //_repository.GetAll(c => c.LastName!.ToLower().StartsWith(startLastName.ToLower()))
                _repository.GetAll(c => c.LastName!.StartsWith(startLastName.ToUpper()))
                );
        }


        //GET /contacts/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        //public IActionResult GetById([FromRoute] int id)
        {
            var contact = _repository.Get(id);


            if (contact == null)
                return NotFound(new
                {
                    Message = "There is no Contact with this Id."
                });
            //return NotFound("There is no Contact with this Id.");
            //return NotFound();

            return Ok(new
            {
                Message = "Contact found.",
                Contact = contact
            });
        }

        //GET /contacts/name/Pierre
        [HttpGet("name/{firstname}")]
        public IActionResult GetByFirstName(string firstname)
        //public IActionResult GetByFirstName([FromRoute] string firstname)
        {
            var contact = _repository.Get(c => c.FirstName == firstname);

            if (contact == null)
                return NotFound(new
                {
                    Message = "There is no Contact with this Firstname."
                });

            return Ok(new
            {
                Message = "Contact found.",
                Contact = contact
            });
        }

        //POST /contacts
        [HttpPost]
        public IActionResult Post([FromBody] Contact contact)
        {
            var contactAdded = _repository.Add(contact);

            if (contactAdded != null)
                //return Ok("Contact Added !");
                //return Created($"/contacts/{createdAtId}", "Contact Added !");
                return CreatedAtAction(nameof(GetById), 
                                       new { id = contactAdded.Id },
                                       new
                                       {
                                            Message = "Contact Added.",
                                            Contact = contactAdded
                                       });

            return BadRequest("Something went wrong...");
        }

        //PUT /contacts/4
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody] Contact contact)
        //public IActionResult Put([FromBody] Contact contact) // ici l'id serait dans le contact
        {
            var contactFromDb = _repository.Get(id);

            if (contactFromDb == null)
                return NotFound("There is no Contact with this Id.");

            contact.Id = id; // nécessaire dans le cas où l'id n'est pas ou mal définit dan la requete

            var contactUpdated = _repository.Update(contact);

            if (contactUpdated != null)
                return Ok(new
                {
                    Message = "Contact Updated.",
                    Contact = contactUpdated
                });

            return BadRequest("Something went wrong...");
        }


        //DELETE /contacts/12
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.Delete(id))
                return Ok("Contect Deleted");

            //return NotFound("Contact Not Found");
            return BadRequest("Something went wrong...");
        }
    }
}
