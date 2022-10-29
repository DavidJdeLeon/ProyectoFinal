using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Datos;
namespace Proyecto.Controllers
{
    public class FinanzaController : Controller
    {
        FinanzaDatos finanza = new FinanzaDatos();
        CadenaConexion conn = new CadenaConexion();
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
        public ActionResult BuscarFichaPago()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EncabezadoPagoficha(int idFicha)
        {
            //se mostrara lista de fichas
            SqlConnection cn = new SqlConnection(conn.CConexion);

            DataTable dt = new DataTable();
            cn.Open();
            SqlCommand cmd = new SqlCommand("sp_BuscarFichaCosto", cn);
            cmd.Parameters.AddWithValue("@idFicha", idFicha);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();

            return View(dt);
        }
        public ActionResult EditarMontoPagoFicha(int idFicha)
        {
            ViewBag.idFicha = idFicha;
            return View();
        }
        public ActionResult EjecutarEditarDetalleFicha(int idFicha, float pago)
        {
            var respuesta = finanza.EjecutarPagoFicha(idFicha,pago);
            if (respuesta)
            {
                return RedirectToAction("FichaClinica","Home");
            }
            else
            {
                
                return View("FichaClinica", "Home");
            }
        }

        public ActionResult EstadoFinancieroIdFicha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListarEstadoFinancieroIdFicha(int idficha)
        {
            var oListaTransaccionContable = finanza.BuscarTransaccionesIdFicha(idficha);

            return View(oListaTransaccionContable);
        }
    }
}
