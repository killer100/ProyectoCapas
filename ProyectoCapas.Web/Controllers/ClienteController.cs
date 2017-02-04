using ProyectoCapas.Web.TiendaProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoCapas.Web.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly TiendaServiceClient _tiendaClient;
        public ClienteController()
        {
            this._tiendaClient = new TiendaServiceClient();
        }
        // GET api/cliente
        public Object Get(int page, int pageSize)
        {
            return new { pagination = this._tiendaClient.GetPageClientes(page, pageSize) };
        }

        // GET api/cliente/5
        public Object Get(string id)
        {
            return new { cliente = this._tiendaClient.GetCliente(id) };
        }

        // POST api/cliente
        public Object Post(ClienteDto cliente)
        {
            this._tiendaClient.SaveCliente(cliente);
            return new { success = true };
        }

        // PUT api/cliente/5
        public Object Put(string id, ClienteDto cliente)
        {
            this._tiendaClient.UpdateCliente(id, cliente);
            return new { success = true };
        }

        // DELETE api/cliente/5
        public Object Delete(string id)
        {
            this._tiendaClient.DeleteCliente(id);
            return new { success = true };
        }

        [HttpGet]
        [Route("api/cliente/listar")]
        public Object listar(string filtro) {
            var clientes = this._tiendaClient.ListClientesByFilter(filtro);
            return new { clientes = clientes };
        }
    }
}
