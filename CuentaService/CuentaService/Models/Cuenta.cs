using System;
using System.Collections.Generic;

namespace CuentaService.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int Idcuenta { get; set; }
        public string? Numerocuenta { get; set; }
        public string? Tipocuenta { get; set; }
        public decimal? Saldoinicial { get; set; }
        public bool? Estadocuenta { get; set; }
        public string? Identificacioncli { get; set; }

        public virtual ICollection<Movimiento>? Movimientos { get; set; }
    }
}
