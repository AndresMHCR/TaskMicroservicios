using CuentaService.Models;
using Microsoft.AspNetCore.Mvc;
using CuentaService.Repository.Interface;
using System;
using CuentaService.Models.Dto;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.IO.Pipelines;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;

namespace CuentaService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ICuentaRepository _cuentaRepository;
        public ReportesController(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ReporteMovimientos([FromQuery] string identificacion, [FromQuery] string fecha)
        {
            var fechaInicio = Convert.ToDateTime(fecha.Split(';')[0]);
            var fechaFin = Convert.ToDateTime(fecha.Split(';')[1]);

            if(fechaFin < fechaInicio)
            {
                return BadRequest();
            }

            Cuenta resp = await _cuentaRepository.GetReporteMovimiento(identificacion, fechaInicio, fechaFin);
            PersonaDto persona = await ObtenerPersona(identificacion);


            if (resp.Movimientos.Count == 0)
            {
                ModelState.AddModelError("ErrorReporte", "No existen Movimientos registrados.");
                return BadRequest(ModelState);
            }

            if (persona == null)
            {
                ModelState.AddModelError("ErrorPersona", "No existen Persona registrada.");
                return BadRequest(ModelState);
            }
            List<ReporteMovimientoDto> reporte = new();
            foreach(Movimiento mov in resp.Movimientos )
            {
                reporte.Add(new ReporteMovimientoDto { 
                    Fecha =  mov.Fecha.Value.ToString("dd/MM/yyyy"), 
                    Cliente = persona.Nombre,
                    NumeroCuenta = resp.Numerocuenta,
                    TipoCuenta = resp.Tipocuenta,
                    SaldoInicial = (decimal) resp.Saldoinicial,
                    Estado = (bool) resp.Estadocuenta,
                    ValorMovimiento = mov.Valor,
                    SaldoDisponible = (decimal)mov.Saldo
                });
            }


            return Ok(reporte);
        }

        private async Task<PersonaDto> ObtenerPersona (string identificacion)
        {
            string url = $"https://localhost:7059/Persona?identificacion={identificacion}";
            PersonaDto persona = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage serviceResponse = await httpClient.GetAsync(url);
                    if (serviceResponse.IsSuccessStatusCode)
                    {

                        // consume servicio
                        var resp = await serviceResponse.Content.ReadAsStringAsync();
                        persona = JsonConvert.DeserializeObject<PersonaDto>(resp);
                        httpClient.Dispose();
                        

                    }
                    else
                    {
                        httpClient.Dispose();
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return persona;
        }


    }
}
