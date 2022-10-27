using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Datos;
using Proyecto.Models;
namespace Proyecto.Controllers
{
    public class PacienteController : Controller
    {
        PacienteDatos pacienteDatos = new PacienteDatos();
        public ActionResult GuardarDatosPaciente()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value ="M", Text ="M" });
            items.Add(new SelectListItem { Value = "F", Text = "F" });
            ViewBag.Sexo = items;
            return View();
        }

        [HttpPost]
        public ActionResult GuardarDatosPaciente(Paciente paciente)
        {
            var respuesta = pacienteDatos.GuardarPaciente(paciente);
            if (respuesta)
            {
                return RedirectToAction("ListarPacientes");
            }
            else
            {
                return View("ListarPacientes");
            }
        }

        public ActionResult ListarPacientes()
        {
            var oListaPaciente = pacienteDatos.Listar();

            return View(oListaPaciente);
        }
    }
}
