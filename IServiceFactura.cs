using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfServiceFactura
{
    [ServiceContract]
    public interface IServiceFactura
    {

        [OperationContract]
        RespuestaTotalFacturas PostFacturar(List<Factura> facturas);
        
        [OperationContract]
        RespuestaValidacionFactura GetValidaFactura(Factura factura);
        [OperationContract]
        RespuestaPorcentajeIVA GetValorIVa(Factura factura);
    }
    
   

}
