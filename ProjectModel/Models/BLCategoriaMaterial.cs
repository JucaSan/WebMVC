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
    public class BLCategoriaMaterial
    {
        DALMySQL objdal = null;

        public BLCategoriaMaterial(string conex)
        {
            objdal = new DALMySQL(conex);
        }

        public List<CategoriaMaterial> ObtenerCategorias(ref string msj)
        {
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            List<CategoriaMaterial> Lsalida = new List<CategoriaMaterial>();


            MySqlDataReader contatrapa = null;
            string consulta = "SELECT * FROM categoriamaterial;";
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    Lsalida.Add(new CategoriaMaterial()
                    {
                        idCategoria = (int)contatrapa[0],
                        nomCategoria = contatrapa[1].ToString(),
                        extra = contatrapa[2].ToString()
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

        public Boolean InsertarCategoria(CategoriaMaterial nuevo, ref string msj)
        {
            Boolean salida = false;

            // Se crea la lista
            List<MySqlParameter> p = new List<MySqlParameter>();

            // Se crean los parametros con su tipo de dato correspondiente
            p.Add(new MySqlParameter("name", MySqlDbType.VarChar, 145));
            p.Add(new MySqlParameter("extra", MySqlDbType.VarChar, 145));


            // Se le asignan valores a cada uno de los parametros
            p[0].Value = nuevo.nomCategoria;
            p[1].Value = nuevo.extra;

            string sentencia = "INSERT INTO categoriamaterial(NomCategoria, extra)" +
                "VALUES (@name, @extra);";

            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            salida = objdal.ModificacionSeguraBD(sentencia, p, cnab, ref msj);
            return salida;
        }



        public Boolean EditarCategoria(CategoriaMaterial nuevo, ref string msj)
        {
            Boolean salida = false;

            // Se crea la lista
            List<MySqlParameter> p = new List<MySqlParameter>();

            // Se crean los parametros con su tipo de dato correspondiente
            p.Add(new MySqlParameter("id", MySqlDbType.Int64));
            p.Add(new MySqlParameter("name", MySqlDbType.VarChar, 145));
            p.Add(new MySqlParameter("extra", MySqlDbType.VarChar, 145));


            // Se le asignan valores a cada uno de los parametros
            p[0].Value = nuevo.idCategoria;
            p[1].Value = nuevo.nomCategoria;
            p[2].Value = nuevo.extra;

            string sentencia = "UPDATE categoriamaterial SET NomCategoria = @name, extra = @extra WHERE idcategoria = @id";

            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            salida = objdal.ModificacionSeguraBD(sentencia, p, cnab, ref msj);
            return salida;
        }


        public Boolean EliminarCategoria(CategoriaMaterial nuevo, ref string mensaje)
        {
            Boolean salida = false;

            List<MySqlParameter> p = new List<MySqlParameter>();
            p.Add(new MySqlParameter("id", MySqlDbType.Int64));

            p[0].Value = nuevo.idCategoria;

            string sentencia = "DELETE FROM categoriamaterial WHERE idcategoria = @id";
            MySqlConnection conexion = null;
            conexion = objdal.AbrirConexion(ref mensaje);
            salida = objdal.ModificacionSeguraBD(sentencia, p, conexion, ref mensaje);
            return salida;
        }

        public CategoriaMaterial CategoriaMaterialPorId(int id, ref string msj)
        {
            CategoriaMaterial categoriaMaterial = null;
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            MySqlDataReader contatrapa = null;
            string consulta = "SELECT * FROM categoriamaterial WHERE idcategoria =" + id;
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    categoriaMaterial = new CategoriaMaterial()
                    {
                        nomCategoria = contatrapa[1].ToString(),
                        extra = contatrapa[2].ToString(),

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

            return categoriaMaterial;
        }
    }
}
