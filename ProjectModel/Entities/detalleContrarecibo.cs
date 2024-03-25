using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.Entities
{
    public class detalleContrarecibo
    {
        public int id { get; set; } 
        public string contrarecibo { get; set; }
        public string fecha_recibo { get; set; }
        public string obra { get; set; }
        public string nota { get; set; }
        public string fecha_nota { get; set; }
        public string total { get; set; }
        public string pagada { get; set; }
        public string extra { get; set; }

    }
}
