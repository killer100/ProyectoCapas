using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProyectoCapas.Host.TransferObjects
{
    [DataContract]
    public class ClienteDto
    {
        [DataMember]
        public string cod_clie { get; set; }
        [DataMember]
        public string mon_ape { get; set; }
        [DataMember]
        public string telef { get; set; }
        [DataMember]
        public string dni { get; set; }
        [DataMember]
        public string dir { get; set; }
    }
}