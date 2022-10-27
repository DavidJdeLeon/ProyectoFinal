using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Proyecto.Models;
namespace Proyecto.Datos
{
    public class PacienteDatos
    {
        CadenaConexion conn = new CadenaConexion();
        public bool GuardarPaciente(Paciente oPaciente)
        {//GUARDA LOS DATOS POR SP A LA BASE DE DATOS
            bool rpta;
            try
            {
                using (SqlConnection cn = new SqlConnection(conn.CConexion))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarPaciente", cn);
                    cmd.Parameters.AddWithValue("dpi", oPaciente.dpi);
                    cmd.Parameters.AddWithValue("nit", oPaciente.nit);
                    cmd.Parameters.AddWithValue("nombre", oPaciente.nombre);
                    cmd.Parameters.AddWithValue("apellido", oPaciente.apeliido);
                    cmd.Parameters.AddWithValue("anioNacimiento", oPaciente.anioNacimiento);
                    cmd.Parameters.AddWithValue("correo", oPaciente.correo);
                    cmd.Parameters.AddWithValue("telefono", oPaciente.telefono);
                    cmd.Parameters.AddWithValue("direccion", oPaciente.direccion);
                    cmd.Parameters.AddWithValue("sexo", oPaciente.sexo);
                    cmd.Parameters.AddWithValue("estatura", oPaciente.estatura);
                    cmd.Parameters.AddWithValue("tipoSangre", oPaciente.tipoSangre);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    cn.Close();
                }
                rpta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }
            return rpta;
        }

        public IEnumerable<Paciente> Listar()
        {
            var oLista = new List<Paciente>();

            using (SqlConnection cn = new SqlConnection(conn.CConexion))
            {//LISTA TODAS LAS CITAS/FICHA CREADAS, SOLO ENCABEZADO
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarPaciente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new Paciente()
                        {
                            idPaciente = Convert.ToInt32(dr["idPaciente"]),
                            dpi = Convert.ToString(dr["dpi"]),
                            nombre = Convert.ToString(dr["nombre"]),
                            apeliido = dr["apeliido"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            correo = dr["correo"].ToString(),
                            anioNacimiento = dr["anioNacimiento"].ToString(),
                            direccion = dr["direccion"].ToString()
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }
    }
}