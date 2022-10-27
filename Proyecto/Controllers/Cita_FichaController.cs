using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Datos;
using Proyecto.Models;
using System.Data.SqlClient;
using System.Data;
namespace Proyecto.Controllers
{
    public class Cita_FichaController : Controller
    {
        FichaEncabezadoDatos fichaEncabezadoDatos = new FichaEncabezadoDatos();
        CadenaConexion conn = new CadenaConexion();

        // GET: Cita_Ficha

        public ActionResult Listar()//LISTAR LOS ENCABEZADOS DE FICHA
        {
            //se mostrara lista de fichas
            var oLista = fichaEncabezadoDatos.Listar();
            return View(oLista);
        }

        public ActionResult Guardar()
        {
            //devuelve la vista
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in fichaEncabezadoDatos.ListarMotivoCita())
            {
                items.Add(new SelectListItem { Value = item.idMotivoCita.ToString(),Text = item.detallle});
            }

            List<SelectListItem> itemVisitas = new List<SelectListItem>();
            foreach (var itemVisita in fichaEncabezadoDatos.ListarEstadoVisita()) 
            {
                itemVisitas.Add(new SelectListItem { Value = itemVisita.idEstado.ToString(), Text = itemVisita.detalle });
            }

            ViewBag.ListadoMotivoCita = items;
            ViewBag.ListadoEstadoVisita = itemVisitas;
            return View();
        }

        [HttpPost]
        public ActionResult Guardar(FichaClinica fichaClinica)
        {
            //recibe objeto  y manda a la base de datos
            var respuesta = fichaEncabezadoDatos.GuardarFicha(fichaClinica);
            if (respuesta)
            {
                return RedirectToAction("GuardarDetalle");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ActualizarResultadosFicha()
        {
            
            return View();
        }
        public ActionResult BuscarIdFicha()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ActualizarResultadosFicha(int idFichaDetalle)
        {
            //se mostrara lista de fichas
            SqlConnection cn = new SqlConnection(conn.CConexion);
            
                DataTable dt = new DataTable();
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarDetallePorID", cn);
                cmd.Parameters.AddWithValue("@idFichaDetalle_", idFichaDetalle);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cn.Close();
            
            return View(dt);
        }

        public ActionResult EditarDetalleFicha(int idFichaDetalle)
        {
            List<SelectListItem> itemMedicamentos = new List<SelectListItem>();
            foreach (var itemMedicamento in fichaDetalleDatos.ListarMedicamento())
            {
                itemMedicamentos.Add(new SelectListItem { Value = itemMedicamento.idMedicamento.ToString(), Text = itemMedicamento.nombreProducto });
            }
            ViewBag.listaMedicamentos = itemMedicamentos;
            ViewBag.idFichaDetalle_ = idFichaDetalle;
            return View();
        }

        public ActionResult EjecutarEditarDetalleFicha(FichaClinica_FichaDetalle fichaClinica_FichaDetalle)
        {
            var respuesta = fichaDetalleDatos.EditarDetalleFicha(fichaClinica_FichaDetalle);
            if (respuesta)
            {
                return RedirectToAction("BuscarIdFicha");
            }
            else
            {
                return View("BuscarIdFicha");
            }
        }

        // GET: Cita_Ficha
        public ActionResult ListarDetalle()
        {
            //se mostrara lista de fichas
            var oLista = fichaEncabezadoDatos.Listar();
            return View(oLista);
        }
        FichaDetalleDatos fichaDetalleDatos = new FichaDetalleDatos();
        public ActionResult GuardarDetalle()
        {
            List<SelectListItem> itemExamenes = new List<SelectListItem>();
            foreach (var itemExamen in fichaDetalleDatos.ListarExamen())
            {
                itemExamenes.Add(new SelectListItem { Value = itemExamen.idExamen.ToString(), Text = itemExamen.nombre });
            }
            ViewBag.ListarExamen = itemExamenes;
            //devuelve la vista
            return View();
        }

        [HttpPost]
        public ActionResult GuardarDetalle(FichaDetalle fichaDetalle)
        {
            //recibe objeto  y manda a la base de datos
            var respuesta = fichaDetalleDatos.GuardarFichaDetalle(fichaDetalle);
            if (respuesta)
            {
                return RedirectToAction("GuardarDetalle");
            }
            else
            {
                return View();
            }
        }

        public ActionResult BuscarIdFicha2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BuscarIdFicha2(int idFicha)
        {
            var respuesta = fichaDetalleDatos.FinalizarFichaTraslado(idFicha);
            if (respuesta)
            {
                return RedirectToAction("ReporteCostoFichaId", "Reporte",new { @idFicha=idFicha });
            }
            else
            {
                return View();
            }
        }
        public ActionResult ActualizarEstadoCita(int idFicha)
        {
            //se mostrara lista de fichas
            List<SelectListItem> itemVisitas = new List<SelectListItem>();
            foreach (var itemVisita in fichaEncabezadoDatos.ListarEstadoVisita())
            {
                itemVisitas.Add(new SelectListItem { Value = itemVisita.idEstado.ToString(), Text = itemVisita.detalle });
            }
            ViewBag.ListarEstados=itemVisitas;

            SqlConnection cn = new SqlConnection(conn.CConexion);

            DataTable dt = new DataTable();
            cn.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarEncabezadoFichaId", cn);
            cmd.Parameters.AddWithValue("idCita_Ficha", idFicha);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();
            return View(dt);
        }
    }
}