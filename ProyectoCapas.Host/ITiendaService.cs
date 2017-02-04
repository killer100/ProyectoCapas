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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ITiendaService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ITiendaService
    {
        [OperationContract]
        Pagination<ArticuloDto> GetPageArticulos(int page, int pageSize);

        [OperationContract]
        ArticuloDto GetArticulo(string cod_art);

        [OperationContract]
        void SaveArticulo(ArticuloDto articulo);

        [OperationContract]
        void UpdateArticulo(string cod_art, ArticuloDto articulo);

        [OperationContract]
        void DeleteArticulo(string cod_art);

        [OperationContract]
        IList<ArticuloDto> ListArticulosByFilter(string filter);

        [OperationContract]
        Pagination<ClienteDto> GetPageClientes(int page, int pageSize);

        [OperationContract]
        ClienteDto GetCliente(string cod_clie);

        [OperationContract]
        void SaveCliente(ClienteDto cliente);

        [OperationContract]
        void UpdateCliente(string cod_clie, ClienteDto cliente);

        [OperationContract]
        void DeleteCliente(string cod_clie);

        [OperationContract]
        IList<ClienteDto> ListClientesByFilter(string filter);
    }
}
