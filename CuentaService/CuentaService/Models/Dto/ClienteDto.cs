using System;
using System.Collections.Generic;

namespace CuentaService.Models.Dto
{
    public partial class ClienteDto
    {
        public int Idcliente { get; set; }
        public int Idpersona { get; set; }
        public string? Clienteid { get; set; }
        public string? Contrasena { get; set; }
        public bool? Estadocliente { get; set; }

    }
}
