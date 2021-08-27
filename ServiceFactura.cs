using System.Collections.Generic;
using System.ServiceModel;

namespace WcfServiceFactura
{

    public class ServiceFactura : IServiceFactura
    {
        public RespuestaValidacionFactura GetValidaFactura(Factura factura)
        {
            RespuestaValidacionFactura Resultado = new RespuestaValidacionFactura();
            Resultado = factura.ValidaFactura(factura);
            return Resultado;

        }

        RespuestaTotalFacturas IServiceFactura.PostFacturar(List<Factura> facturas)
        {
            RespuestaTotalFacturas Resultado = new RespuestaTotalFacturas();
            Factura factura = new Factura();
            Resultado = factura.ProcesarFacturas(facturas);
            return Resultado;
        }

        public RespuestaPorcentajeIVA GetValorIVa(Factura factura)
        {
            RespuestaPorcentajeIVA Resultado = new RespuestaPorcentajeIVA();
            Resultado = factura.GetValorIVa(factura);
            return Resultado;
        }
    }
}
