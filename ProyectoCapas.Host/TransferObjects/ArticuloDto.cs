using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProyectoCapas.Host.TransferObjects
{
    [DataContract]
    public class ArticuloDto
    {
        [DataMember]
        public string cod_art { get; set; }
        [DataMember]
        public string descrip { get; set; }
        [DataMember]
        public decimal prec_unic { get; set; }
        [DataMember]
        public decimal stock { get; set; }
        [DataMember]
        public string title { get; set; }
    }
}