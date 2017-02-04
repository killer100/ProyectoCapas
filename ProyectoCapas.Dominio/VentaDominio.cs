using ProyectoCapas.DataAccess;
using ProyectoCapas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapas.Dominio
{
    public class VentaDominio
    {
        private readonly FacturaDao _facturaDao;
        private readonly DetalleDao _detalleDao;
        private readonly ArticuloDao _articuloDao;

        public VentaDominio()
        {
            this._facturaDao = new FacturaDao();
            this._detalleDao = new DetalleDao();
            this._articuloDao = new ArticuloDao();
        }

        public IList<vw_facturas> ListarFacturas(int page, int pageSize)
        {
            return this._facturaDao.ListarFacturas(page, pageSize);
        }

        public int ContarFacturas()
        {
            return this._facturaDao.ContarFacturas();
        }

        public String GuardarFactura(Factura factura, List<Detalle> detalles)
        {
            try
            {
                var vw_factura = this._facturaDao.GuardarFactura(factura);
                detalles.ForEach(detalle =>
                {
                    detalle.num_fact = vw_factura.num_fact;
                    this._detalleDao.GuardarDetalle(detalle);
                    var articulo = this._articuloDao.ListarArticulo(detalle.cod_art);
                    articulo.stock = articulo.stock - detalle.cant;
                    this._articuloDao.ActualizarArticulo(articulo.cod_art, articulo);
                });
                return vw_factura.num_fact_format;
            }
            catch (Exception e)
            {
                return (e.Message + " " + e.InnerException);
            }

        }

        public vw_facturas ListarFactura(decimal num_fact)
        {
            return this._facturaDao.ListarFactura(num_fact);
        }

        public IList<vw_detalle> ListarDetallePorFactura(decimal num_fact)
        {
            return this._facturaDao.ListarDetallePorFactura(num_fact);
        }
    }
}
