using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class TransaccionContable
    {
        public int idTransaccionContable { get; set; }
        public string CCU { get; set; }
        public string DescripcionCCU { get; set; }
        public string CC { get; set; }
        public string DescripcionCC { get; set; }
        public string Fecha { get; set; }
        public string Motivo { get; set; }
        public int idCitaCosto { get; set; }
        public float Debe { get; set; }
        public float Haber { get; set; }

    }
}