using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Datos;
using Proyecto.Models;
namespace Proyecto.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        CadenaConexion conn = new CadenaConexion();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ModeloUsuario ousuario)
        {
            //usuario.clave = ConvertirSha256(usuario.clave);
            using (SqlConnection cn = new SqlConnection(conn.CConexion ))
            {
                //BUSCA EN LA BASE DE DATOS EL USUARIO
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("usuario", ousuario.usuario1);
                cmd.Parameters.AddWithValue("clave", ousuario.clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                ousuario.idUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());//VERIFICA SI EL RESULTADO DE LA BASE DE DATOS EN VALDO PARA E INGRESO
                
            }

            if (ousuario.idUsuario != 0)
            {
                Session["usuario"] = ousuario;
                return RedirectToAction("FichaClinica", "Home",ousuario);
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }




        }
    }
}