using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Datos;
namespace Proyecto.Controllers
{
    public class FinanzaController : Controller
    {
        FinanzaDatos finanza = new FinanzaDatos();
        // GET: Finanza
        public ActionResult EstadoFinancieroFechas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListarEstadoFinancieroFechas(string fecha_ini, string fecha_fin)
        {
            var oListaTransaccionContable = finanza.BuscarTransaccionesFechas(fecha_ini, fecha_fin);

            return View(oListaTransaccionContable);
        }
    }
}
