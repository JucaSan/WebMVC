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
    public class BLContrarecibo
    {
        DALMySQL objdal = null;

        public BLContrarecibo(string conex)
        {
            objdal = new DALMySQL(conex);
        }

        public List<Contrarecibo> ObtenerContrarecibo(ref string msj)
        {
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            List<Contrarecibo> Lsalida = new List<Contrarecibo>();


            MySqlDataReader contatrapa = null;
            string consulta = "SELECT idcontrarecibo, DATE_FORMAT(fecha, '%Y-%m-%d') AS fecha, obra, extra \r\nFROM contrarecibo;\r\n";
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    Lsalida.Add(new Contrarecibo()
                    {
                        id = (int)contatrapa[0],
                        fecha = contatrapa[1].ToString(),
                        obra = contatrapa[2].ToString(),
                        extra = contatrapa[3].ToString()
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

        public Boolean InsertarContrarecibo(Contrarecibo nuevo, ref string msj)
        {
            Boolean salida = false;

            // Se crea la lista
            List<MySqlParameter> p = new List<MySqlParameter>();

            // Se crean los parametros con su tipo de dato correspondiente
            p.Add(new MySqlParameter("fecha", MySqlDbType.Date));
            p.Add(new MySqlParameter("obra", MySqlDbType.Int64));
            p.Add(new MySqlParameter("extra", MySqlDbType.VarChar, 145));


            // Se le asignan valores a cada uno de los parametros
            p[0].Value = nuevo.fecha;
            p[1].Value = nuevo.obra;
            p[2].Value = nuevo.extra;

            string sentencia = "INSERT INTO contrarecibo(fecha,obra,extra)" +
                "VALUES (@fecha,@obra, @extra);";

            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            salida = objdal.ModificacionSeguraBD(sentencia, p, cnab, ref msj);
            return salida;
        }



        public Boolean EditarContrarecibo(Contrarecibo nuevo, ref string msj)
        {
            Boolean salida = false;

            // Se crea la lista
            List<MySqlParameter> p = new List<MySqlParameter>();

            // Se crean los parametros con su tipo de dato correspondiente
            p.Add(new MySqlParameter("id",MySqlDbType.Int64));
            p.Add(new MySqlParameter("fecha", MySqlDbType.Date));
            p.Add(new MySqlParameter("obra", MySqlDbType.Int64));
            p.Add(new MySqlParameter("extra", MySqlDbType.VarChar, 145));


            // Se le asignan valores a cada uno de los parametros
            p[0].Value = nuevo.id;
            p[1].Value = nuevo.fecha;
            p[2].Value = nuevo.obra;
            p[3].Value = nuevo.extra;

            string sentencia = "UPDATE contrarecibo SET fecha = @fecha, obra = @obra , extra = @extra WHERE idcontrarecibo = @id";

            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            salida = objdal.ModificacionSeguraBD(sentencia, p, cnab, ref msj);
            return salida;
        }

        public Boolean EliminarContrarecibo(Contrarecibo n, ref string mensaje)
        {
            Boolean salida = false;

            List<MySqlParameter> p = new List<MySqlParameter>();
            p.Add(new MySqlParameter("id", MySqlDbType.Int64));

            p[0].Value = n.id;

            string sentencia = "DELETE FROM contrarecibo WHERE idcontrarecibo = @id";
            MySqlConnection conexion = null;
            conexion = objdal.AbrirConexion(ref mensaje);
            salida = objdal.ModificacionSeguraBD(sentencia, p, conexion, ref mensaje);
            return salida;
        }

        public Contrarecibo ContrareciboPorId(int id, ref string msj)
        {
            Contrarecibo contra = null;
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);

            MySqlDataReader contatrapa = null;
            string consulta = "SELECT * FROM contrarecibo WHERE idcontrarecibo =" + id;
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    contra = new Contrarecibo()
                    {
                        fecha = contatrapa[1].ToString(),
                        obra = contatrapa[2].ToString(),
                        extra = contatrapa[3].ToString(),

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

            return contra;
        }
    }
}
