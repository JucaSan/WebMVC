﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.Entities
{
    public class nota
    {
        public int Id { get; set; }
        public string Numeronota { get; set; } 
        public string  fecha { get; set; }
        public string obra { get; set; }
        public string provedor { get; set;}
        public string extra { get; set;}
    }
}
