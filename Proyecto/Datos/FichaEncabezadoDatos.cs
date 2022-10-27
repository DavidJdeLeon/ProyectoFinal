using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Proyecto.Models;
namespace Proyecto.Datos
{
    public class FichaEncabezadoDatos
    {
        CadenaConexion conn = new CadenaConexion();

        public IEnumerable<FichaClinica> Listar()
        {
            var oLista = new List<FichaClinica>();

            using (SqlConnection cn = new SqlConnection(conn.CConexion))
            {//LISTA TODAS LAS CITAS/FICHA CREADAS, SOLO ENCABEZADO
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarEncabezadoFicha",cn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new FichaClinica() {
                            idCita_Ficha=Convert.ToInt32(dr["idCita_Ficha"]),
                            fecha=Convert.ToString(dr["fecha"]),
                            hora = Convert.ToString(dr["hora"]),
                            nombre = dr["nombre"].ToString(),
                            apellido = dr["apeliido"].ToString(),
                            motivoCita=dr["motivo visita"].ToString(),
                            estadoVisita=dr["Estado Visita"].ToString(),
                            observacion=dr["observacion"].ToString()
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }

        public FichaClinica ObtenerFicha(int idFicha)
        {
            var oFicha = new FichaClinica();

            using (SqlConnection cn = new SqlConnection(conn.CConexion ))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerEncabezadoFicha", cn);
                cmd.Parameters.AddWithValue("idCita",idFicha);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oFicha.fecha = Convert.ToString(dr["fecha"]);
                        oFicha.hora = Convert.ToString(dr["hora"]);
                        oFicha.nombre = dr["nombre"].ToString();
                        oFicha.apellido = dr["apeliido"].ToString();
                        oFicha.motivoCita = dr["motivo visita"].ToString();
                        oFicha.estadoVisita = dr["Estado Visita"].ToString();
                        oFicha.observacion = dr["observacion"].ToString();
                    }
                }
                cn.Close();
            }
            return oFicha;
        }

        public bool GuardarFicha(FichaClinica oFicha)
        {//GUARDA LOS DATOS POR SP A LA BASE DE DATOS
            bool rpta;
            try
            {
                using (SqlConnection cn = new SqlConnection(conn.CConexion ))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarFichaEncabezado", cn);
                    cmd.Parameters.AddWithValue("fecha", oFicha.fecha);
                    cmd.Parameters.AddWithValue("hora", oFicha.hora);
                    cmd.Parameters.AddWithValue("fk_idPaciente", oFicha.fk_idPaciente);
                    cmd.Parameters.AddWithValue("fk_idMotivoCita", oFicha.fk_idMotivoCita);
                    cmd.Parameters.AddWithValue("fk_idEstadoVisita", oFicha.fk_idEstadoVisita);
                    cmd.Parameters.AddWithValue("observacion", oFicha.observacion);
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

        public List<MotivoCita> ListarMotivoCita()
        {
            var oLista = new List<MotivoCita>();

            using (SqlConnection cn = new SqlConnection(conn.CConexion ))
            {//LISTA TODOS LOS MOTIVOS DE LAS CITAS PARA APLICARLO AR DROPWDOWNLIST
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarMotivoCita", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new MotivoCita() {
                            idMotivoCita = Convert.ToInt32(dr["idMotivoCita"]),
                            detallle = dr["detallle"].ToString()
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }

        public List<EstadoVisita> ListarEstadoVisita()
        {
            var oLista = new List<EstadoVisita>();

            using (SqlConnection cn = new SqlConnection(conn.CConexion))
            {//LISTA TODOS LOS MOTIVOS DE LAS CITAS PARA APLICARLO AR DROPWDOWNLIST
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarEstadoVisita", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new EstadoVisita()
                        {
                            idEstado = Convert.ToInt32(dr["idEstado"]),
                            detalle = dr["detalle"].ToString()
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }
    }
}