using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class FichaDetalle
    {
        public int idFichaDetalle { get; set; }
        public int fk_idCita_Ficha { get; set; }
        public int fk_idExamen { get; set; }
        public string resultadoExamen { get; set; }
        public string diagnostico { get; set; }
        public int fk_idPersonalMedico_especialista { get; set; }
        public int fk_idPersonalMedico_Enfermero { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public int fk_idMedicamento { get; set; }
        public int cantidad { get; set; }
        public string RecetaMedica { get; set; }
        public string observacion { get; set; }

    }
}