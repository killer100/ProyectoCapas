using ProyectoCapas.Web.TiendaProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoCapas.Web.Controllers
{
    public class ArticuloController : ApiController
    {
        private readonly TiendaServiceClient _tiendaClient;
        public ArticuloController()
        {
            this._tiendaClient = new TiendaServiceClient();
        }
        // GET api/articulo
        public Object Get(int page, int pageSize)
        {
            return new { pagination = this._tiendaClient.GetPageArticulos(page, pageSize) };
        }

        // GET api/articulo/5
        public Object Get(string id)
        {
            return new { articulo = this._tiendaClient.GetArticulo(id) };
        }

        // POST api/articulo
        public Object Post(ArticuloDto articulo)
        {
            this._tiendaClient.SaveArticulo(articulo);
            return new { success = true };
        }

        // PUT api/articulo/5
        public Object Put(string id, ArticuloDto articulo)
        {
            this._tiendaClient.UpdateArticulo(id, articulo);
            return new { success = true };
        }

        // DELETE api/articulo/5
        public Object Delete(string id)
        {
            this._tiendaClient.DeleteArticulo(id);
            return new { success = true };
        }

        [HttpGet]
        [Route("api/articulo/listar")]
        public Object listar(string filtro)
        {
            var articulos = this._tiendaClient.ListArticulosByFilter(filtro);
            return new { articulos = articulos };
        }
    }
}
