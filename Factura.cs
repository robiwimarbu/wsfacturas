using System;
using System.Collections.Generic;
using System.Linq;


namespace WcfServiceFactura
{
    public class Factura
    {
        public int Id { get; set; }
        public string Nit { get ; set;}
        public string Descripcion { get; set; }
        public float ValorTotal { get; set; }
        public int PorcentajeIVA { get; set; }
        public RespuestaTotalFacturas ProcesarFacturas(List<Factura> facturas)
        {
            RespuestaTotalFacturas respuesta = new RespuestaTotalFacturas();
            try
            {
                foreach (Factura item in facturas)
                {
                    int cantidad = facturas.Where(x => x.Id == item.Id).Count();
                    List<Error> resultado = new List<Error>();
                    if (cantidad > 1)
                    {
                        respuesta.AddError("El Id " + item.Id + " existe " + cantidad + " veces ");
                    }
                    resultado = ValidaItem(item);
                    resultado.AddRange(respuesta.Errores);
                    respuesta.Errores = resultado;
                    

                }
                if (respuesta.Errores.Count() == 0)
                {
                    respuesta.Total = TotalFacturas(facturas);
                }
            }
            catch (Exception Ex)
            {
                respuesta.AddError("Error al Procesar los datos" + Ex.Message.ToString());
            }
            
            return respuesta;
        }
        public float TotalFacturas(List<Factura> facturas)
        {
            float totalFacturas = facturas.Sum(factura =>factura.ValorTotal);
            return totalFacturas;
        }
        public RespuestaValidacionFactura ValidaFactura(Factura factura)
        {
            List<Error> resultado= new List<Error>();
            RespuestaValidacionFactura respuesta = new RespuestaValidacionFactura();
            resultado = ValidaItem(factura);
            if (resultado.Count == 0)
            {
                respuesta.Validacion= "La Factura no tiene errores de formato";
            }
            else {

                respuesta.Errores = resultado;
            }

            return respuesta;
        }
        public RespuestaPorcentajeIVA GetValorIVa(Factura factura)
        {
            RespuestaPorcentajeIVA resultado = new RespuestaPorcentajeIVA();
            List<Error> validar =  ValidaItem(factura);
            if (validar.Count()==0)
            {
                float Iva = factura.ValorTotal * factura.PorcentajeIVA / 100;
                resultado.IVA = Iva;
            }
            else
            {
                resultado.Errores = validar;
            }
            return resultado;
        }
        private List<Error> ValidaItem(Factura factura)
        {
            long NumeroNit;
            String Errores="";
            List<Error> respuesta = new List<Error>();
            try
            {
                if (factura.Id < 0)
                {
                    Errores +=" El Id " + factura.Id +" No puede ser negativo";
                }
                
                bool canConvert = long.TryParse(factura.Nit, out NumeroNit);
                if (canConvert != true)
                    Errores+=" El Nit " + factura.Nit + " No es un valor númerico";

                if(factura.ValorTotal <=0)
                {
                    Errores+=" El Valor Total " + factura.ValorTotal + " No puede ser negativo";
                }

                if(factura.PorcentajeIVA >100 || factura.PorcentajeIVA<0)
                {
                    Errores+=" El Porcentaje de IVA " + factura.PorcentajeIVA + " No puede ser inferior a 0 o Mayor a 100";
                }
            }
            catch (Exception Ex)
            {

                Errores+="Error " + Ex.Message.ToString();
            }
            if (Errores.Length > 0)
            {
                if (factura.Descripcion == null)
                {
                    factura.Descripcion = "Factura";
                }
                respuesta.Add(new Error("Errores en " + factura.Descripcion+":"+ Errores));
            }
            return respuesta;
        }
    }
}
