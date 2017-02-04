using ProyectoCapas.DataAccess;
using ProyectoCapas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapas.Dominio
{
    public class TiendaDominio
    {

        private readonly ArticuloDao _articuloDao;
        private readonly ClienteDao _clienteDao;

        public TiendaDominio()
        {
            this._articuloDao = new ArticuloDao();
            this._clienteDao = new ClienteDao();
        }

        public IList<Articulo> ListarArticulos(int page, int pageSize, string filter = null)
        {
            return this._articuloDao.ListarArticulos(page, pageSize, filter);
        }

        public int ContarArticulos()
        {
            return this._articuloDao.ContarArticulos();
        }

        public void GuardarArticulo(Articulo articulo)
        {
            this._articuloDao.GuardarArticulo(articulo);
        }

        public Articulo ListarArticulo(string cod_art)
        {
            return this._articuloDao.ListarArticulo(cod_art);
        }

        public void ActualizarArticulo(string cod_art, Articulo articulo)
        {
            this._articuloDao.ActualizarArticulo(cod_art, articulo);
        }

        public void EliminarArticulo(string cod_art)
        {
            this._articuloDao.EliminarArticulo(cod_art);
        }

        public IList<Cliente> ListarClientes(int page, int pageSize, string filter = null)
        {
            try
            {
                return this._clienteDao.ListarClientes(page, pageSize, filter);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public int ContarClientes()
        {
            return this._clienteDao.ContarClientes();
        }

        public void GuardarCliente(Cliente cliente)
        {
            this._clienteDao.GuardarCliente(cliente);
        }

        public Cliente ListarCliente(string cod_clie)
        {
            return this._clienteDao.ListarCliente(cod_clie);
        }

        public void ActualizarCliente(string cod_clie, Cliente cliente)
        {
            this._clienteDao.ActualizarCliente(cod_clie, cliente);
        }

        public void EliminarCliente(string cod_clie)
        {
            this._clienteDao.EliminarCliente(cod_clie);
        }


    }
}
