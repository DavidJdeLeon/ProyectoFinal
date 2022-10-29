﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Proyecto.Models;
namespace Proyecto.Datos
{
    public class FinanzaDatos
    {
        CadenaConexion conn = new CadenaConexion();
        public IEnumerable<TransaccionContable> BuscarTransaccionesFechas(string fecha_ini, string fecha_fin)
        {
            var oLista = new List<TransaccionContable>();
            
            using (SqlConnection cn = new SqlConnection(conn.CConexion))
            {//LISTA TODAS LAS CITAS/FICHA CREADAS, SOLO ENCABEZADO
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_TransaccionContableFechas", cn);
                cmd.Parameters.AddWithValue("fecha_ini", fecha_ini);
                cmd.Parameters.AddWithValue("fecha_fin", fecha_fin);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new TransaccionContable()
                        {
                            idTransaccionContable = Convert.ToInt32(dr["idTransaccionContable"]),
                            CCU = Convert.ToString(dr["CCU"]),
                            DescripcionCCU = Convert.ToString(dr["DescripcionCCU"]),
                            CC = dr["CC"].ToString(),
                            DescripcionCC = dr["DescripcionCC"].ToString(),
                            Fecha = dr["Fecha"].ToString(),
                            Motivo = dr["Motivo"].ToString(),
                            idCitaCosto = Convert.ToInt32(dr["IdCitaCosto"]),
                            Debe = Convert.ToSingle(dr["Debe"]),
                            Haber = Convert.ToSingle(dr["Haber"])
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }

        public bool EjecutarPagoFicha(int idFicha, float pago)
        {//GUARDA LOS DATOS POR SP A LA BASE DE DATOS
            bool rpta;
            try
            {
                if (pago > 0)
                {
                    using (SqlConnection cn = new SqlConnection(conn.CConexion))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("sp_EjecutarPagoFicha", cn);
                        cmd.Parameters.AddWithValue("idFicha", idFicha);
                        cmd.Parameters.AddWithValue("pago", pago);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                        cn.Close();
                    }
                    rpta = true;
                }
                else
                {
                    rpta = false;
                }
                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }
            return rpta;
        }

        public IEnumerable<TransaccionContable> BuscarTransaccionesIdFicha(int idFicha)
        {
            var oLista = new List<TransaccionContable>();

            using (SqlConnection cn = new SqlConnection(conn.CConexion))
            {//LISTA TODAS LAS CITAS/FICHA CREADAS, SOLO ENCABEZADO
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarTransaccionContableId", cn);
                cmd.Parameters.AddWithValue("idFicha", idFicha);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new TransaccionContable()
                        {
                            idTransaccionContable = Convert.ToInt32(dr["idTransaccionContable"]),
                            CCU = Convert.ToString(dr["CCU"]),
                            DescripcionCCU = Convert.ToString(dr["DescripcionCCU"]),
                            CC = dr["CC"].ToString(),
                            DescripcionCC = dr["DescripcionCC"].ToString(),
                            Fecha = dr["Fecha"].ToString(),
                            Motivo = dr["Motivo"].ToString(),
                            idCitaCosto = Convert.ToInt32(dr["IdCitaCosto"]),
                            Debe = Convert.ToSingle(dr["Debe"]),
                            Haber = Convert.ToSingle(dr["Haber"])
                        });
                    }
                }
                cn.Close();
            }
            return oLista;
        }
    }
}