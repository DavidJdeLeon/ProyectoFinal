using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Paciente
    {
        //campos persona
        public int idPersona { get; set; }
        public string dpi { get; set; }
        public string nit { get; set; }
        public string nombre { get; set; }
        public string apeliido { get; set; }
        public string anioNacimiento { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }

        //campos paciente
        public int idPaciente { get; set; }
        public int fk_idPersona { get; set; }
        public string sexo { get; set; }
        public float estatura { get; set; }
        public string tipoSangre { get; set; }
    }
}