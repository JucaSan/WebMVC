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
    public class BLMaterial
    {
        DALMySQL objdal = null;

        public BLMaterial(string conex)
        {
            objdal = new DALMySQL(conex);
        }

        public List<Material> ObtenerMateriales(ref string msj)
        {
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            List<Material> Lsalida = new List<Material>();


            MySqlDataReader contatrapa = null;
            string consulta = "SELECT M.Idmaterial, M.NombreMat, M.Marca, C.NomCategoria, M.UnidadMedida FROM material as M JOIN categoriamaterial as C ON M.Categoria = C.idcategoria; ";
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    Lsalida.Add(new Material()
                    {
                        Idmaterial = (int)contatrapa[0],
                        NombreMat = contatrapa[1].ToString(),
                        Marca = contatrapa[2].ToString(),
                        Categoria = contatrapa[3].ToString(),
                        UnidadMedida = contatrapa[4].ToString(),

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



        public Boolean InsertarMaterial(Material nuevo, ref string msj)
        {
            Boolean salida = false;

            // Se crea la lista
            List<MySqlParameter> p = new List<MySqlParameter>();

            // Se crean los parametros con su tipo de dato correspondiente
            p.Add(new MySqlParameter("name", MySqlDbType.VarChar, 145));
            p.Add(new MySqlParameter("brand", MySqlDbType.VarChar, 145));
            p.Add(new MySqlParameter("category", MySqlDbType.Int64));
            p.Add(new MySqlParameter("unit", MySqlDbType.VarChar, 100));


            // Se le asignan valores a cada uno de los parametros
            p[0].Value = nuevo.NombreMat;
            p[1].Value = nuevo.Marca;
            p[2].Value = nuevo.Categoria;
            p[3].Value = nuevo.UnidadMedida;

            string sentencia = "INSERT INTO material(NombreMat, Marca, Categoria, UnidadMedida)" +
                "VALUES (@name, @brand, @category, @unit);";

            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            salida = objdal.ModificacionSeguraBD(sentencia, p, cnab, ref msj);
            return salida;
        }


        public Boolean EditarMaterial(Material nuevo, ref string msj)
        {
            Boolean salida = false;

            // Se crea la lista
            List<MySqlParameter> p = new List<MySqlParameter>();

            // Se crean los parametros con su tipo de dato correspondiente
            p.Add(new MySqlParameter("id", MySqlDbType.Int64));
            p.Add(new MySqlParameter("name", MySqlDbType.VarChar, 145));
            p.Add(new MySqlParameter("brand", MySqlDbType.VarChar, 145));
            p.Add(new MySqlParameter("category", MySqlDbType.Int64));
            p.Add(new MySqlParameter("unit", MySqlDbType.VarChar, 100));


            // Se le asignan valores a cada uno de los parametros
            p[0].Value = nuevo.Idmaterial;
            p[1].Value = nuevo.NombreMat;
            p[2].Value = nuevo.Marca;
            p[3].Value = nuevo.Categoria;
            p[4].Value = nuevo.UnidadMedida;

            string sentencia = "UPDATE material SET NombreMat = @name, Marca = @brand, Categoria = @category, UnidadMedida = @unit WHERE Idmaterial = @id";

            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            salida = objdal.ModificacionSeguraBD(sentencia, p, cnab, ref msj);
            return salida;
        }

        public Boolean EliminarMaterial(Material nuevo, ref string mensaje)
        {
            Boolean salida = false;

            List<MySqlParameter> p = new List<MySqlParameter>();
            p.Add(new MySqlParameter("id", MySqlDbType.Int64));

            p[0].Value = nuevo.Idmaterial;

            string sentencia = "DELETE FROM material WHERE Idmaterial = @id";
            MySqlConnection conexion = null;
            conexion = objdal.AbrirConexion(ref mensaje);
            salida = objdal.ModificacionSeguraBD(sentencia, p, conexion, ref mensaje);
            return salida;
        }

        public Material MaterialPorId(int id, ref string msj)
        {
            Material material = null;
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);

            MySqlDataReader contatrapa = null;
            string consulta = "SELECT * FROM material WHERE Idmaterial ="+id;
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    material = new Material()
                    {
                        NombreMat = contatrapa[1].ToString(),
                        Marca = contatrapa[2].ToString(),
                        UnidadMedida = contatrapa[4].ToString(),

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

            return material;
        }
    }
}
