using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Examen
    {
        public int idExamen { get; set; }
        public string tiempo { get; set; }
        public string disponible { get; set; }
        public string nombre { get; set; }
        public float costo { get; set; }

    }
}