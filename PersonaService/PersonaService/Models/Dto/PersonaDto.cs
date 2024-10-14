using System;
using System.Collections.Generic;

namespace PersonaService.Models.Dto
{
    public partial class PersonaDto
    {

        public int Idpersona { get; set; }
        public string? Identificacion { get; set; }
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

    }
}
