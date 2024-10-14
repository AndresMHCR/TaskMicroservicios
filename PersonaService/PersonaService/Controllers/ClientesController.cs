using PersonaService.Models;
using Microsoft.AspNetCore.Mvc;
using PersonaService.Repository.Interface;
using System;

namespace PersonaService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;
        public ClientesController(IPersonaRepository personaRepository) 
        {
            _personaRepository = personaRepository;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IEnumerable<VillaDto>> GetVillas()
        //{
        //    return Ok(VillaStore.villaList);
        //}

        [HttpGet("{idClient:int}", Name ="GetCliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> GetCliente(int idClient)
        {
            if (idClient == 0)
            {
                return BadRequest();
            }
            var cliente = await  _personaRepository.GetClient(idClient);
            if(cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> CreateClient([FromBody] Cliente cliente)
        {
            Cliente register = await _personaRepository.GetClient(cliente.Idcliente);

            if (register!= null)
            {
                ModelState.AddModelError("CreateError", "Client already Exists!");
                return BadRequest(ModelState);
            }
            if (cliente == null)
            {
                return BadRequest(cliente);
            }
            if (cliente.Idcliente > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            await _personaRepository.CreateClient(cliente);
            return CreatedAtRoute("GetCliente", new { idClient = cliente.Idcliente }, cliente);
        }

        [HttpDelete("{idClient:int}", Name = "DeleteClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            if (idClient == 0)
            {
                return BadRequest();
            }

            Cliente register = await _personaRepository.GetClient(idClient);
            if (register == null)
            {
                return NotFound();
            }

            await _personaRepository.DeleteClient(register);
            return NoContent();
        }

        [HttpPut("{idClient:int}", Name = "UpdateClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClient(int idClient, [FromBody] Cliente client)
        {
            if (client == null || idClient != client.Idcliente)
            {
                return BadRequest();
            }

            Cliente register = await _personaRepository.GetClient(idClient);
            register.Clienteid = client.Clienteid;
            register.Contrasena = client.Contrasena;
            register.Estadocliente = client.Estadocliente;

            _personaRepository.UpdateClient(register);

            return NoContent();
        }


    }
}
