using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCapas.Host.TransferObjects
{
    public class DetalleDto
    {
        public decimal num_fact { get; set; }
        public string cod_art { get; set; }
        public decimal cant { get; set; }
        public string descrip { get; set; }
        public decimal prec_unic { get; set; }
    }
}