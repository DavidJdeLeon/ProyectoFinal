using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Datos
{
    public class CadenaConexion
    {
        private string cadenaConexion = @"Data Source=LAPTOP-4NELPGNL\SQLEXPRESS;Initial Catalog=ProyectoAnalisis;Integrated Security=True";

        public string CConexion
        {
            get
            {
                return cadenaConexion;
            }
        }
    }
}