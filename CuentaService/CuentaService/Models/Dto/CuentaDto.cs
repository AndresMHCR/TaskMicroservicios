using System;
using System.Collections.Generic;

namespace CuentaService.Models.Dto
{
    public partial class CuentaDto
    {
        public CuentaDto()
        {
            Movimientos = new HashSet<MovimientoDto>();
        }

        public int Idcuenta { get; set; }
        public string? Numerocuenta { get; set; }
        public string? Tipocuenta { get; set; }
        public decimal? Saldoinicial { get; set; }
        public bool? Estadocuenta { get; set; }
        public string? Identificacioncli { get; set; }

        public virtual ICollection<MovimientoDto> Movimientos { get; set; }
    }
}
