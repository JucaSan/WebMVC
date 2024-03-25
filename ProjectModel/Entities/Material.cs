using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.Entities
{
    public class Material
    {
        public int Idmaterial { get; set; }
        public string NombreMat { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public string UnidadMedida { get; set; }
    }
}
