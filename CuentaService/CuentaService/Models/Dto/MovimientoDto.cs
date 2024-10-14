using System;
using System.Collections.Generic;

namespace CuentaService.Models.Dto
{
    public partial class MovimientoDto
    {
        public int Idmovimiento { get; set; }
        public int Idcuenta { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Tipomovimiento { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Saldo { get; set; }

    }

    public partial class GenerarMovimientoDto
    {
        public string NumCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipomovimiento { get; set; }
        public decimal Valor { get; set; }

    }

    public partial class ReporteMovimientoDto
    {
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta{ get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal ValorMovimiento { get; set; }
        public decimal SaldoDisponible { get; set; }

    }
}
