using System;
using System.Collections.Generic;

namespace CuentaService.Models
{
    public partial class Movimiento
    {
        public int Idmovimiento { get; set; }
        public int Idcuenta { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Tipomovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal? Saldo { get; set; } = 0;

        public virtual Cuenta? IdcuentaNavigation { get; set; } = null!;
    }
}
