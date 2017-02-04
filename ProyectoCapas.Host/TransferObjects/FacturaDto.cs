using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCapas.Host.TransferObjects
{
    public class FacturaDto
    {
        public decimal num_fact { get; set; }
        public string num_fact_format { get; set; }
        public string cod_clie { get; set; }
        public DateTime fech_vent { get; set; }
        public string mon_ape { get; set; }
        public IList<DetalleDto> detalle { get; set; }

    }
}