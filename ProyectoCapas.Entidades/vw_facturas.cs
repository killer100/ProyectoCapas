using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapas.Entidades
{
    public class vw_facturas
    {
        public string num_fact_format { get; set; }
        public decimal num_fact { get; set; }
        public string cod_clie { get; set; }
        public DateTime fech_vent { get; set; }
        public string mon_ape { get; set; }
    }
}
