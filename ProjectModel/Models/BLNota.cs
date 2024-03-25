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
    public class BLNota
    {
        DALMySQL objdal = null;

        public BLNota(string conex)
        {
            objdal = new DALMySQL(conex);
        }

        public List<nota> Obtenernota(ref string msj)
        {
            MySqlConnection cnab = null;
            cnab = objdal.AbrirConexion(ref msj);
            List<nota> Lsalida = new List<nota>();


            MySqlDataReader contatrapa = null;
            string consulta = "SELECT * FROM notacompra;";
            contatrapa = objdal.ConsultaDR(consulta, cnab, ref msj);

            if (contatrapa != null)
            {
                // La consulta es correcta, se va a procesar el DR

                while (contatrapa.Read())
                {
                    Lsalida.Add(new nota()
                    {
                        Id = (int)contatrapa[0],
                        Numeronota = contatrapa[1].ToString(),
                        fecha = contatrapa[2].ToString(),
                        obra = contatrapa[3].ToString(),
                        provedor = contatrapa[4].ToString(),
                        extra = contatrapa[5].ToString(),
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

    }
}
