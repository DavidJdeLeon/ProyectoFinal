using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Datos
{
    public class CadenaConexion
    {
        private string cadenaConexion = @"Data Source=SQL8003.site4now.net;Initial Catalog=db_a8cb3f_ddeleon;User Id=db_a8cb3f_ddeleon_admin;Password=david123";

        public string CConexion
        {
            get
            {
                return cadenaConexion;
            }
        }
    }
}