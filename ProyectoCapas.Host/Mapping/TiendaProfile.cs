using AutoMapper;
using ProyectoCapas.Entidades;
using ProyectoCapas.Host.TransferObjects;

namespace ProyectoCapas.Host.Mapping
{
    public class TiendaProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Articulo, ArticuloDto>()
                .ForMember(x => x.title, option => option.MapFrom(model => model.cod_art + " " + model.descrip + "   cantidad: " + model.stock));
            Mapper.CreateMap<ArticuloDto, Articulo>();
            Mapper.CreateMap<Cliente, ClienteDto>();
            Mapper.CreateMap<ClienteDto, Cliente>();
            Mapper.CreateMap<vw_facturas, FacturaDto>();
            Mapper.CreateMap<FacturaDto, Factura>();
            Mapper.CreateMap<DetalleDto, Detalle>();
            Mapper.CreateMap<Detalle, DetalleDto>();
            Mapper.CreateMap<DetalleDto, vw_detalle>();
            Mapper.CreateMap<vw_detalle, DetalleDto>();

        }
    }
}