using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto.Models
{
    public class FichaClinica
    {

        public int idCita_Ficha { get; set; }
        [Required]
        public string fecha { get; set; }
        [Required]
        public string hora { get; set; }
        [Required]
        public int fk_idPaciente { get; set; }
        [Required]
        public int fk_idMotivoCita { get; set; }
        [Required]
        public int fk_idEstadoVisita { get; set; }
        public string observacion { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string motivoCita { get; set; }
        public string estadoVisita { get; set; }

        public string detallle { get; set; }

    }
}