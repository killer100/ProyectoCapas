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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "VentaService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione VentaService.svc o VentaService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class VentaService : IVentaService
    {
        private readonly VentaDominio _ventaDominio;

        public VentaService()
        {
            this._ventaDominio = new VentaDominio();
        }
        public Pagination<FacturaDto> GetListaFacturas(int page, int pageSize)
        {

            var facturas = this._ventaDominio.ListarFacturas(page, pageSize);
            var cantidad = this._ventaDominio.ContarFacturas();

            return new Pagination<FacturaDto>()
            {
                items = Mapper.Map<IList<vw_facturas>, IList<FacturaDto>>(facturas),
                page = page,
                pageSize = pageSize,
                total = cantidad
            };

        }

        public string SaveFactura(FacturaDto facturaDto)
        {
            var factura = Mapper.Map<FacturaDto, Factura>(facturaDto);
            var detalles = Mapper.Map<IList<DetalleDto>, IList<Detalle>>(facturaDto.detalle);
            var numero_factura = this._ventaDominio.GuardarFactura(factura, detalles.ToList());
            return numero_factura;
        }

        public FacturaDto GetFactura(decimal num_fact)
        {
            var factura = this._ventaDominio.ListarFactura(num_fact);
            var response = Mapper.Map<vw_facturas, FacturaDto>(factura);
            var detalles = this._ventaDominio.ListarDetallePorFactura(num_fact);
            response.detalle = Mapper.Map<IList<vw_detalle>, IList<DetalleDto>>(detalles);
            return response;
        }
    }
}
