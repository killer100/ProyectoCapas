using ProyectoCapas.Web.VentaProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoCapas.Web.Controllers
{
    public class FacturaController : ApiController
    {

        private readonly VentaServiceClient _ventasClient;

        public FacturaController()
        {
            this._ventasClient = new VentaServiceClient();
        }
        // GET api/factura
        public Object Get(int page, int pageSize)
        {
            var pagination = this._ventasClient.GetListaFacturas(page, pageSize);
            return new { pagination = pagination };
        }

        // GET api/factura/5
        public Object Get(decimal id)
        {
            var factura = this._ventasClient.GetFactura(id);
            return new { factura = factura };
        }

        // POST api/factura
        public Object Post(FacturaDto factura)
        {
            var numero_factura = this._ventasClient.SaveFactura(factura);
            return new { factura = numero_factura };
        }

        // PUT api/factura/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/factura/5
        public void Delete(int id)
        {
        }
    }
}
