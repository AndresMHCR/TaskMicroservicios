using CuentaService.Models;
using Microsoft.AspNetCore.Mvc;
using CuentaService.Repository.Interface;
using System;
using CuentaService.Models.Dto;

namespace CuentaService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly ICuentaRepository _cuentaRepository;
        public MovimientosController(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IEnumerable<VillaDto>> GetVillas()
        //{
        //    return Ok(VillaStore.villaList);
        //}

        [HttpGet("{idMovimiento:int}", Name = "GetMovimiento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int idMovimiento)
        {
            if (idMovimiento == 0)
            {
                return BadRequest();
            }
            var movimiento = await _cuentaRepository.GetMovimiento(idMovimiento);
            if (movimiento == null)
            {
                return NotFound();
            }
            return Ok(movimiento);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movimiento>> RegisterMovimiento([FromBody] GenerarMovimientoDto movimiento)
        {

            if (movimiento == null)
            {
                return BadRequest(movimiento);
            }
            Cuenta regCuenta = await _cuentaRepository.GetCuentaXNumCuenta(movimiento.NumCuenta);
            
            if (regCuenta == null)
            {
                ModelState.AddModelError("ErrorCuenta", "No Existe Cuenta!");
                return BadRequest(ModelState);
            }
            Movimiento register = await _cuentaRepository.GetUltimoMovimientoxCuenta(regCuenta.Idcuenta);

            var valorMov = movimiento.Tipomovimiento.ToUpper().Equals("RETIRO") ? movimiento.Valor * (-1):movimiento.Valor;

            
                decimal saldoMov = 0;
            if(register == null)
            {
                saldoMov = (decimal) regCuenta.Saldoinicial + valorMov;
            }
            else
            {
                saldoMov = (decimal)register.Saldo + valorMov;
            }

            if (movimiento.Tipomovimiento.ToUpper().Equals("RETIRO") && saldoMov < 0 )
            {
                ModelState.AddModelError("ErrorMovimiento", "No Tiene Saldo Disponible para realizar esta transaccion");
                return BadRequest(ModelState);
            }


            Movimiento mov = new()
            {
                Idcuenta = regCuenta.Idcuenta,
                Fecha = movimiento.Fecha,
                Tipomovimiento = movimiento.Tipomovimiento.ToUpper(),
                Valor = valorMov,
                Saldo = saldoMov
            };


            await _cuentaRepository.CreateMovimiento(mov);
            return CreatedAtRoute("GetMovimiento", new { idMovimiento = mov.Idmovimiento }, mov);
        }

        [HttpDelete("{idMovimiento:int}", Name = "DeleteMovimiento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMovimiento(int IdMovimiento)
        {
            if (IdMovimiento == 0)
            {
                return BadRequest();
            }

            Movimiento register = await _cuentaRepository.GetMovimiento(IdMovimiento);
            if (register == null)
            {
                return NotFound();
            }

            await _cuentaRepository.DeleteMovimiento(register);
            return NoContent();
        }

        [HttpPut("{idMovimiento:int}", Name = "UpdateMovimiento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMovimiento(int idMovimiento, [FromBody] Movimiento movimiento)
        {
            if (movimiento == null || idMovimiento != movimiento.Idmovimiento)
            {
                return BadRequest();
            }

            Movimiento register = await _cuentaRepository.GetMovimiento(idMovimiento);
            register.Fecha = movimiento.Fecha;
            register.Tipomovimiento= movimiento.Tipomovimiento;
            register.Valor= movimiento.Valor;
            register.Saldo= movimiento.Saldo;

            _cuentaRepository.UpdateMovimiento(register);

            return NoContent();
        }


    }
}
