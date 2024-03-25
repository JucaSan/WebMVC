using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.DAL
{
    public class DALMySQL
    {
        public string cadconex { get; set; }

        public DALMySQL(string cadconx)
        {
            cadconex = cadconx;
        }



        public MySqlConnection AbrirConexion(ref string msj)
        {
            MySqlConnection cn1 = new MySqlConnection();
            cn1.ConnectionString = cadconex;

            try
            {
                cn1.Open();
                msj = "Conexion abierta";
            }
            catch (Exception e)
            {
                cn1 = null;
                msj = "Error" + e.Message;
            }

            return cn1;
        }

        public Boolean ModificacionBD(string sentSQL, MySqlConnection cnab, ref string msj)
        {
            Boolean salida = false;
            if (cnab != null)
            {
                MySqlCommand carrito = new MySqlCommand();
                carrito.CommandText = sentSQL;
                carrito.Connection = cnab;

                try
                {
                    carrito.ExecuteNonQuery();
                    msj = "Modificacion Correcta";
                    salida = true;
                }
                catch (Exception e)
                {
                    salida = false;
                    msj = "Error" + e.Message;
                }
                cnab.Close();
                cnab.Dispose();

            }
            else
            {
                msj = "No hay conexion a la BD";
                salida = false;
            }
            return salida;
        }

        public Boolean ModificacionSeguraBD(string sentSQL, List<MySqlParameter> misparametros, MySqlConnection cnab, ref string msj)
        {
            Boolean salida = false;
            if (cnab != null)
            {
                MySqlCommand carrito = new MySqlCommand();
                carrito.CommandText = sentSQL;
                carrito.Connection = cnab;
                // Se van a agregar los sqlparameters al carrito
                if (misparametros != null)
                {
                    foreach (MySqlParameter par in misparametros)
                    {
                        carrito.Parameters.Add(par);
                    }
                }

                try
                {
                    carrito.ExecuteNonQuery();
                    msj = "Modificacion Correcta";
                    salida = true;
                }
                catch (Exception e)
                {
                    salida = false;
                    msj = "Error" + e.Message;
                }
                cnab.Close();
                cnab.Dispose();

            }
            else
            {
                msj = "No hay conexion a la BD";
                salida = false;
            }
            return salida;
        }


        public MySqlDataReader ConsultaDR(string querysql, MySqlConnection cnab, ref string msj)
        {
            MySqlDataReader contenedor = null;
            MySqlCommand carrito = new MySqlCommand();
            if (cnab != null)
            {
                carrito.CommandText = querysql;
                carrito.Connection = cnab;

                try
                {
                    contenedor = carrito.ExecuteReader();
                    msj = "La consulta es correcta";
                }
                catch (Exception e)
                {
                    msj = "Error" + e.Message;
                }
            }
            else
            {
                msj += ",No hay conexion con la base de datos";
            }
            return contenedor;
        }


        public DataSet ConsultaDS(string querysql, MySqlConnection cnab, ref string msj)
        {
            DataSet BDvirtual = null;
            MySqlCommand carrito = null;
            MySqlDataAdapter trailer = null;

            if (cnab != null)
            {
                carrito = new MySqlCommand();
                carrito.CommandText = querysql;
                carrito.Connection = cnab;

                trailer = new MySqlDataAdapter();
                BDvirtual = new DataSet();

                trailer.SelectCommand = carrito;

                try
                {
                    trailer.Fill(BDvirtual);
                    msj = "La consulta es correcta";
                }
                catch (Exception e)
                {
                    msj = "Error" + e.Message;
                }
                cnab.Close();
                cnab.Dispose();
            }
            else
            {
                msj += ",No hay conexion con la base de datos";
            }
            return BDvirtual;
        }

        public Boolean ConsultaMultipleDS(DataSet contenedor, string query, MySqlConnection cnab, string nomconsulta, ref string msj)
        {
            MySqlCommand carrito = null;
            MySqlDataAdapter trailer = null;
            Boolean salida = false;
            if (cnab != null)
            {
                carrito = new MySqlCommand();
                carrito.CommandText = query;
                carrito.Connection = cnab;
                trailer = new MySqlDataAdapter();
                trailer.SelectCommand = carrito;

                try
                {
                    trailer.Fill(contenedor, nomconsulta);
                    msj += "Consulta CORRECTA";
                    salida = true;
                }
                catch (Exception e)
                {
                    msj += "error" + e.Message;
                    salida = false;
                }
                cnab.Close();
                cnab.Dispose();
            }
            else
            {
                msj += "No hay conexion a la BD";
                salida = false;
            }

            return salida;
        }
    }
}
