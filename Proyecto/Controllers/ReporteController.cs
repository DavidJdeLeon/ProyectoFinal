using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Datos;
namespace Proyecto.Controllers
{
    public class ReporteController : Controller
    {
        CadenaConexion conn = new CadenaConexion();
        // GET: Reporte
        [Route]
        public ActionResult ReporteCostoFichaId(int idFicha)
        {
            SqlConnection cn = new SqlConnection(conn.CConexion);

            DataTable dt = new DataTable();
            cn.Open();
            SqlCommand cmd = new SqlCommand("sp_ConsultarCostosFichaId", cn);
            cmd.Parameters.AddWithValue("idFicha", idFicha);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();
            return View(dt);
        }
        public ActionResult BuscarReporteCostoFichaId()
        {
            return View();
        }

    }
}
