using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProyectoCapas.Host.Core
{

    [DataContract]
    public class Pagination<T> where T : class
    {
        [DataMember]
        public int page { get; set; }

        [DataMember]
        public int pageSize { get; set; }

        [DataMember]
        public int total { get; set; }

        [DataMember]
        public IList<T> items { get; set; }
    }
}