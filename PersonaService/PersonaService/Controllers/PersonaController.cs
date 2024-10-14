using PersonaService.Models;
using Microsoft.AspNetCore.Mvc;
using PersonaService.Repository.Interface;

namespace PersonaService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;
        public PersonaController(IPersonaRepository personaRepository) 
        {
            _personaRepository = personaRepository;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IEnumerable<VillaDto>> GetVillas()
        //{
        //    return Ok(VillaStore.villaList);
        //}

        // GET PERSON

        [HttpGet("{idPerson:int}", Name ="GetPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Persona>> GetPerson(int idPerson)
        {
            if (idPerson == 0)
            {
                return BadRequest();
            }
            var person = await  _personaRepository.GetPerson(idPerson);
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // GET PERSON

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Persona>> GetPersonIdentificacion([FromQuery]string identificacion)
        {
            if (identificacion.Length > 10)
            {
                ModelState.AddModelError("Error", "Numero de identificacion invalido");
                return BadRequest(ModelState);
            }

            Persona register = await _personaRepository.GetPersonByNui(identificacion);
            
            if (register == null)
            {
                return NotFound();
            }
            return Ok(register);
        }

        // CREATE PERSON

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Persona>> CreatePerson([FromBody] Persona person)
        {
            Persona register = await _personaRepository.GetPersonByNui(person.Identificacion);
            if (register != null)
            {
                ModelState.AddModelError("CreateError", "Person already Exists!");
                return BadRequest(ModelState);
            }
            if (person == null)
            {
                return BadRequest(person);
            }
            if (person.Idpersona > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            await _personaRepository.CreatePerson(person);
            return CreatedAtRoute("GetPerson", new { idPerson = person.Idpersona }, person);
        }

        //[HttpDelete("{id:int}", Name ="DeleteVilla")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult DeleteVilla(int id)
        //{
        //    if (id  == 0)
        //    {
        //        return BadRequest();
        //    }

        //    var villa = VillaStore.villaList.FirstOrDefault(h => h.Id == id);
        //    if (villa == null)
        //    {
        //        return NotFound();
        //    }

        //    VillaStore.villaList.Remove(villa);
        //    return NoContent();
        //}

        //[HttpPut("{id:int}", Name = "UpdateVilla")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdateVilla(int id ,[FromBody] VillaDto villaDto)
        //{
        //    if (villaDto == null || id != villaDto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var villa = VillaStore.villaList.FirstOrDefault(h => h.Id == id);
        //    villa.Name = villaDto.Name;
        //    villa.Occupancy = villaDto.Occupancy;
        //    villa.Sqft = villaDto.Sqft;

        //    return NoContent();
        //}

        //[HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdatePartialVilla(int id, JsonPatchDocument patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    var villa = VillaStore.villaList.FirstOrDefault(h => h.Id == id);
        //    if(villa == null)
        //    {
        //        return BadRequest();

        //    }

        //    patchDTO.ApplyTo(villa);
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}


    }
}
