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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IVentaService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IVentaService
    {
        [OperationContract]
        Pagination<FacturaDto> GetListaFacturas(int page, int pageSize);

        [OperationContract]
        string SaveFactura(FacturaDto facturaDto);

        [OperationContract]
        FacturaDto GetFactura(decimal num_fact);
    }
}
