using System;
using System.Collections.Generic;

namespace PersonaService.Models
{
    public partial class Cliente
    {
        public int Idcliente { get; set; }
        public int Idpersona { get; set; }
        public string? Clienteid { get; set; }
        public string? Contrasena { get; set; }
        public bool? Estadocliente { get; set; }

        public virtual Persona? IdpersonaNavigation { get; set; } = null!;
    }
}
