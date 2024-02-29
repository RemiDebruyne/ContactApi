using ContactApiDTO.Data;
using ContactApiDTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeContactController : ControllerBase
    {
        private readonly AppDbContext _fakeDb;

        public FakeContactController(AppDbContext fakeDb)
        {
            _fakeDb = fakeDb;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var crepes = _fakeDb.Contacts;

            if (crepes.Any()) // la liste est-elle non vide
                return Ok(crepes); // statuscode 200

            //return NotFound(); // 404 => implique que le fait de ne pas trouver de crêpes soit une erreur
            return NoContent(); // 204 => implique que le fait de ne pas trouver de crêpes soit une réussite
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var crepe = _fakeDb.Contacts.FirstOrDefault(c => c.Id == id);

            if (crepe == null)
                return NotFound(new
                {
                    Message = "Crepe non trouvée !"
                });

            return Ok(new
            {
                Message = "Crepe trouvée !",
                Crepe = crepe
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] Contact contact)
        //public IActionResult Post([FromForm]Crepe crepe) // si formulaire
        {
            _fakeDb.Contacts.Add(contact);

            //return Ok("Crepe ajoutée");
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, "Crepe ajoutée"); // meilleure version à utiliser de préférence
            //return Created($"api/Crepe/{crepe.Id}", "Crepe ajoutée");

            // dans le cas ou l'ajout aura échoué, il convient de retourner un BadRequest() => 400
        }
    }
}
