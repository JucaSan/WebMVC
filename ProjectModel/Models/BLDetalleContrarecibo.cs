using MySql.Data.MySqlClient;
using ProjectModel.DAL;
using ProjectModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.Models
{
    public class BLDetalleContrarecibo
    {
        DALMySQL objdal = null;

        public BLDetalleContrarecibo(string conex)
        {
            objdal = new DALMySQL(conex);
        }

        public List<detalleContrarecibo> ObtenerDetaleContrarecibo(ref string msj)
        {
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            List<detalleContrarecibo> Lsalida = new List<detalleContrarecibo>();


            MySqlDataReader contatrapa = null;
            string consulta = "select iddetallecontrar as id, contrarecibo, DATE_FORMAT(c.fecha, '%Y-%m-%d')  as fecha_recibo,c.obra, n.numeronota,DATE_FORMAT(n.fecha, '%Y-%m-%d') as fecha_nota, total,pagada from detallecontrarecibo as d \r\ninner join notacompra as n on d.nota= n.idnotacompra \r\ninner join contrarecibo as c on d.contrarecibo= c.idcontrarecibo;";
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    Lsalida.Add(new detalleContrarecibo()
                    {
                        id = (int)contatrapa[0],
                        contrarecibo = contatrapa[1].ToString(),
                        fecha_recibo = contatrapa[2].ToString(),
                        obra = contatrapa[3].ToString(),
                        nota = contatrapa[4].ToString(),
                        fecha_nota = contatrapa[5].ToString(),  
                        total = contatrapa[6].ToString(),
                        pagada = contatrapa[7].ToString()

                    });
                }
                cnab.Close();
                cnab.Dispose();
            }
            else
            {
                if (contatrapa.IsClosed)
                {
                    msj += "El DR esta cerrado";
                }
            }

            return Lsalida;
        }

        public Boolean InsertarDetalleContrarecibo(detalleContrarecibo nuevo, ref string msj)
        {
            Boolean salida = false;

            // Se crea la lista
            List<MySqlParameter> p = new List<MySqlParameter>();

            // Se crean los parametros con su tipo de dato correspondiente
            p.Add(new MySqlParameter("contrarecibo", MySqlDbType.Int64));
            p.Add(new MySqlParameter("nota", MySqlDbType.Int64));
            p.Add(new MySqlParameter("total", MySqlDbType.Float));
            p.Add(new MySqlParameter("pagada", MySqlDbType.Bit));
            p.Add(new MySqlParameter("extra", MySqlDbType.VarChar, 145));


            // Se le asignan valores a cada uno de los parametros
            p[0].Value = nuevo.contrarecibo;
            p[1].Value = nuevo.nota;
            p[2].Value = nuevo.total;
            p[3].Value = nuevo.pagada;
            p[4].Value = nuevo.extra;

            string sentencia = "INSERT INTO detallecontrarecibo(contrarecibo,nota,total,pagada,extra)" +
                "VALUES (@contrarecibo,@nota, @total,@pagada,@extra);";

            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            salida = objdal.ModificacionSeguraBD(sentencia, p, cnab, ref msj);
            return salida;
        }



        public Boolean EditarDetalleContrarecibo(detalleContrarecibo nuevo, ref string msj)
        {
            Boolean salida = false;

            // Se crea la lista
            List<MySqlParameter> p = new List<MySqlParameter>();

            // Se crean los parametros con su tipo de dato correspondiente
            p.Add(new MySqlParameter("id", MySqlDbType.Int64));
            p.Add(new MySqlParameter("contrarecibo", MySqlDbType.Int64));
            p.Add(new MySqlParameter("nota", MySqlDbType.Int64));
            p.Add(new MySqlParameter("total", MySqlDbType.Float));
            p.Add(new MySqlParameter("pagada", MySqlDbType.Bit));



            // Se le asignan valores a cada uno de los parametros
            p[0].Value = nuevo.id;
            p[1].Value = nuevo.contrarecibo;
            p[2].Value = nuevo.nota;
            p[3].Value = nuevo.total;
            p[4].Value = nuevo.pagada;


            string sentencia = "UPDATE detallecontrarecibo SET contrarecibo = @contrarecibo, nota = @nota ,total=@total,pagada=@pagada WHERE iddetallecontrar = @id";

            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            salida = objdal.ModificacionSeguraBD(sentencia, p, cnab, ref msj);
            return salida;
        }

        public Boolean EliminarDetalleContrarecibo(detalleContrarecibo n, ref string mensaje)
        {
            Boolean salida = false;

            List<MySqlParameter> p = new List<MySqlParameter>();
            p.Add(new MySqlParameter("id", MySqlDbType.Int64));

            p[0].Value = n.id;

            string sentencia = "DELETE FROM detallecontrarecibo WHERE iddetallecontrar = @id";
            MySqlConnection conexion = null;
            conexion = objdal.AbrirConexion(ref mensaje);
            salida = objdal.ModificacionSeguraBD(sentencia, p, conexion, ref mensaje);
            return salida;
        }

        public detalleContrarecibo detallereciboPorId(int id, ref string msj)
        {
            detalleContrarecibo detalle = null;
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            MySqlDataReader contatrapa = null;
            string consulta = "SELECT * FROM detallecontrarecibo WHERE iddetallecontrar =" + id;
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    detalle = new detalleContrarecibo()
                    {
                        contrarecibo = contatrapa[1].ToString(),
                        nota = contatrapa[2].ToString(),
                        total = contatrapa[3].ToString(),
                        pagada = contatrapa[4].ToString(),
                    };
                }
                cnab.Close();
                cnab.Dispose();
            }
            else
            {
                if (contatrapa.IsClosed)
                {
                    msj += "El DR esta cerrado";
                }
            }

            return detalle;
        }
    }
}
