using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class FichaClinica_FichaDetalle
    {
        public int idCita_Ficha { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public int fk_idPaciente { get; set; }
        public int fk_idMotivoCita { get; set; }
        public int fk_idEstadoVisita { get; set; }
        public string observacion { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string motivoCita { get; set; }
        public string estadoVisita { get; set; }

        public string detallle { get; set; }

        //detalle de la ficha
        public int idFichaDetalle { get; set; }
        public int fk_idCita_Ficha { get; set; }
        public int fk_idExamen { get; set; }
        public string nombreExamen { get; set; }
        public string resultadoExamen { get; set; }
        public string diagnostico { get; set; }
        public int fk_idPersonalMedico_especialista { get; set; }
        public string nombreMedicoEspecialista { get; set; }
        public int fk_idPersonalMedico_Enfermero { get; set; }
        public string nombreEnfermero { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public int fk_idMedicamento { get; set; }
        public string nombreMedicamento { get; set; }
        public int cantidad { get; set; }
        public string recetaMedica { get; set; }
        public string observacion2 { get; set; }
    }
}