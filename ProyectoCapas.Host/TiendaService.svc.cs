using AutoMapper;
using ProyectoCapas.Dominio;
using ProyectoCapas.Entidades;
using ProyectoCapas.Host.Core;
using ProyectoCapas.Host.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProyectoCapas.Host
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "TiendaService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione TiendaService.svc o TiendaService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class TiendaService : ITiendaService
    {

        private readonly TiendaDominio _tiendaDominio;

        public TiendaService()
        {
            this._tiendaDominio = new TiendaDominio();
        }


        public Pagination<ArticuloDto> GetPageArticulos(int page, int pageSize)
        {
            try
            {
                var articulos = this._tiendaDominio.ListarArticulos(page, pageSize);
                return new Pagination<ArticuloDto>()
                {
                    page = page,
                    pageSize = pageSize,
                    total = this._tiendaDominio.ContarArticulos(),
                    items = Mapper.Map<IList<Articulo>, IList<ArticuloDto>>(articulos)
                };
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public ArticuloDto GetArticulo(string cod_art)
        {
            var articulo = this._tiendaDominio.ListarArticulo(cod_art);
            return Mapper.Map<Articulo, ArticuloDto>(articulo);
        }

        public void SaveArticulo(ArticuloDto articulo)
        {
            try
            {
                this._tiendaDominio.GuardarArticulo(Mapper.Map<ArticuloDto, Articulo>(articulo));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public void UpdateArticulo(string cod_art, ArticuloDto articulo)
        {
            try
            {
                this._tiendaDominio.ActualizarArticulo(cod_art, Mapper.Map<ArticuloDto, Articulo>(articulo));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public void DeleteArticulo(string cod_art)
        {
            try
            {
                this._tiendaDominio.EliminarArticulo(cod_art);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public IList<ArticuloDto> ListArticulosByFilter(string filter) {
            var articulos = this._tiendaDominio.ListarArticulos(1, 5, filter);
            return Mapper.Map<IList<Articulo>, IList<ArticuloDto>>(articulos);
        }

        //CLIENTES
        public Pagination<ClienteDto> GetPageClientes(int page, int pageSize)
        {
            try
            {
                var clientes = this._tiendaDominio.ListarClientes(page, pageSize);
                return new Pagination<ClienteDto>()
                {
                    page = page,
                    pageSize = pageSize,
                    total = this._tiendaDominio.ContarClientes(),
                    items = Mapper.Map<IList<Cliente>, IList<ClienteDto>>(clientes)
                };
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }
        public ClienteDto GetCliente(string cod_clie)
        {
            var cliente = this._tiendaDominio.ListarCliente(cod_clie);
            return Mapper.Map<Cliente, ClienteDto>(cliente);
        }

        public void SaveCliente(ClienteDto cliente)
        {
            try
            {
                this._tiendaDominio.GuardarCliente(Mapper.Map<ClienteDto, Cliente>(cliente));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public void UpdateCliente(string cod_clie, ClienteDto cliente)
        {
            try
            {
                this._tiendaDominio.ActualizarCliente(cod_clie, Mapper.Map<ClienteDto, Cliente>(cliente));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public void DeleteCliente(string cod_clie)
        {
            try
            {
                this._tiendaDominio.EliminarCliente(cod_clie);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public IList<ClienteDto> ListClientesByFilter(string filter)
        {
            var clientes = this._tiendaDominio.ListarClientes(1, 5, filter);
            return Mapper.Map<IList<Cliente>, IList<ClienteDto>>(clientes);
        }
    }
}
