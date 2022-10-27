using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Proyecto.Models;
namespace Proyecto.Datos
{
    public class FichaDetalleDatos
    {
        CadenaConexion conn = new CadenaConexion();
        public bool GuardarFichaDetalle(FichaDetalle oFichaDetalle)
        {
            bool rpta;
            try
            {
                using (SqlConnection cn = new SqlConnection(conn.CConexion ))
                {//INGRESO DE LOS DATOS POR SP A LA BASE DE DATOS
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarFichaDetalle", cn);
                    cmd.Parameters.AddWithValue("fk_idExamen", oFichaDetalle.fk_idExamen);
                    cmd.Parameters.AddWithValue("fk_idPersonalMedico_especialista", oFichaDetalle.fk_idPersonalMedico_especialista);
                    cmd.Parameters.AddWithValue("fk_idPersonalMedico_Enfermero", oFichaDetalle.fk_idPersonalMedico_Enfermero);
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

        public List<Examen> ListarExamen()
        {
            var oLista = new List<Examen>();

            using (SqlConnection cn = new SqlConnection(conn.CConexion))
            {//LISTA TODOS LOS MOTIVOS DE LAS CITAS PARA APLICARLO AR DROPWDOWNLIST
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarExamen", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new Examen()
                        {
                            idExamen = Convert.ToInt32(dr["idExamen"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }
        public List<Medicamento> ListarMedicamento()
        {
            var oLista = new List<Medicamento>();

            using (SqlConnection cn = new SqlConnection(conn.CConexion))
            {//LISTA TODOS LOS MOTIVOS DE LAS CITAS PARA APLICARLO AR DROPWDOWNLIST
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarMedicamento", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new Medicamento()
                        {
                            idMedicamento = Convert.ToInt32(dr["idMedicamento"]),
                            nombreProducto = dr["nombreProducto"].ToString()
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }
        public List<FichaClinica_FichaDetalle> ListarDetalleParaActualizar(int idFichaDetalle)
        {
            var oLista = new List<FichaClinica_FichaDetalle>();

            using (SqlConnection cn = new SqlConnection(conn.CConexion ))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarDetallePorID", cn);
                cmd.Parameters.AddWithValue("@idFichaDetalle_", idFichaDetalle);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new FichaClinica_FichaDetalle() {
                            idFichaDetalle = Convert.ToInt32(dr["idFichaDetalle"]),
                            fk_idCita_Ficha = Convert.ToInt32(dr["fk_idCita_Ficha"]),
                            nombreExamen = Convert.ToString(dr["Examen"]),
                            resultadoExamen = dr["resultadoExamen"].ToString(),
                            diagnostico = dr["diagnostico"].ToString(),
                            nombreEnfermero = dr["Enfermero"].ToString(),
                            nombreMedicoEspecialista = dr["Medico_Espacialista"].ToString(),
                            nombreMedicamento = dr["medicamento"].ToString(),
                            cantidad = Convert.ToInt32(dr["cantidad"]),
                            recetaMedica = dr["RecetaMedica"].ToString(),
                            observacion = dr["observacion"].ToString()
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }

        public bool EditarDetalleFicha(FichaClinica_FichaDetalle oFichaClinica_FichaDetalle)
        {
            bool rpta;
            try
            {
                using (SqlConnection cn = new SqlConnection(conn.CConexion))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarDetalleFicha", cn);
                    cmd.Parameters.AddWithValue("idFichaDetalle", oFichaClinica_FichaDetalle.idFichaDetalle);
                    cmd.Parameters.AddWithValue("resultadoExamen", oFichaClinica_FichaDetalle.resultadoExamen);
                    cmd.Parameters.AddWithValue("diagnostico", oFichaClinica_FichaDetalle.diagnostico);
                    cmd.Parameters.AddWithValue("fk_idMedicamento", oFichaClinica_FichaDetalle.fk_idMedicamento);
                    cmd.Parameters.AddWithValue("cantidad", oFichaClinica_FichaDetalle.cantidad);
                    cmd.Parameters.AddWithValue("RecetaMedica", oFichaClinica_FichaDetalle.recetaMedica);
                    cmd.Parameters.AddWithValue("observación", oFichaClinica_FichaDetalle.observacion);
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

        public bool FinalizarFichaTraslado(int idFicha)
        {
            bool rpta;
            try
            {
                using (SqlConnection cn = new SqlConnection(conn.CConexion))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ProcesarCostosFicha", cn);
                    cmd.Parameters.AddWithValue("idCita_Ficha", idFicha);
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
    }
}