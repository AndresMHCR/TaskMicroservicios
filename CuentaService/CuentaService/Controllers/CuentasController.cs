using CuentaService.Models;
using Microsoft.AspNetCore.Mvc;
using CuentaService.Repository.Interface;
using System;
using CuentaService.Models.Dto;

namespace CuentaService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaRepository _cuentaRepository;
        public CuentasController(ICuentaRepository cuentaRepository) 
        {
            _cuentaRepository = cuentaRepository;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IEnumerable<VillaDto>> GetVillas()
        //{
        //    return Ok(VillaStore.villaList);
        //}

        [HttpGet("{idCuenta:int}", Name ="GetCuenta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cuenta>> GetCuenta(int idCuenta)
        {
            if (idCuenta == 0)
            {
                return BadRequest();
            }
            var cuenta = await  _cuentaRepository.GetCuenta(idCuenta);
            if(cuenta == null)
            {
                return NotFound();
            }
            return Ok(cuenta);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cuenta>> CreateCuenta([FromBody] Cuenta cuenta)
        {
            Cuenta register = await _cuentaRepository.GetCuenta(cuenta.Idcuenta);

            if (register!= null)
            {
                ModelState.AddModelError("CreateError", "Cuenta already Exists!");
                return BadRequest(ModelState);
            }
            if (cuenta == null)
            {
                return BadRequest(cuenta);
            }
            if (cuenta.Idcuenta > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            await _cuentaRepository.CreateCuenta(cuenta);
            return CreatedAtRoute("GetCuenta", new { idCuenta = cuenta.Idcuenta }, cuenta);
        }

        [HttpDelete("{idCuenta:int}", Name = "DeleteCuenta")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCuenta(int idCuenta)
        {
            if (idCuenta == 0)
            {
                return BadRequest();
            }

            Cuenta register = await _cuentaRepository.GetCuenta(idCuenta);
            if (register == null)
            {
                return NotFound();
            }

            await _cuentaRepository.DeleteCuenta(register);
            return NoContent();
        }

        [HttpPut("{idCuenta:int}", Name = "UpdateCuenta")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClient(int idCuenta, [FromBody] Cuenta cuenta)
        {
            if (cuenta == null || idCuenta != cuenta.Idcuenta)
            {
                return BadRequest();
            }

            Cuenta register = await _cuentaRepository.GetCuenta(idCuenta);
            register.Numerocuenta = cuenta.Numerocuenta;
            register.Tipocuenta = cuenta.Tipocuenta;
            register.Saldoinicial = cuenta.Saldoinicial;
            register.Estadocuenta = cuenta.Estadocuenta;

            _cuentaRepository.UpdateCuenta(register);

            return NoContent();
        }


    }
}
