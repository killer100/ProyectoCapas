using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapas.Entidades
{
    public class Articulo
    {
        public string cod_art { get; set; }
        public string descrip { get; set; }
        public decimal prec_unic { get; set; }
        public decimal stock { get; set; }
    }
}
