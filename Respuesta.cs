using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfServiceFactura
{
    public class Error
    {
        private string _Descripcion;

        public Error()
        {
            _Descripcion = "";
        }
        public Error(string Descripcion)
        {
            _Descripcion = Descripcion;
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
    }

    [DataContract]
    public class Respuesta: IExtensibleDataObject
    {    
        
        private List<Error> Lerrores = new List<Error>();
        [DataMember]
        public List<Error> Errores
        {
            get { return Lerrores; }
            set { Lerrores = value; }
        }
        private ExtensionDataObject _Data;
        public virtual ExtensionDataObject ExtensionData
        {
            get { return _Data; }
            set { _Data=value; }
        }

        public void AddError(string Descripcion)
        {
            Error error = new Error(Descripcion);
             Lerrores.Add(error);
        }
    }
    [DataContract(
    Name = "RespuestaTotalFacturas")]
    public class RespuestaTotalFacturas:Respuesta 
    {
        [DataMember]
        public float Total;
        private ExtensionDataObject _Data;
        public override ExtensionDataObject ExtensionData
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }
    [DataContract(
    Name = "RespuestaPorcentajeIVA")]
    public class RespuestaPorcentajeIVA : Respuesta
    {
        [DataMember]
        private float _IVA;

        public float IVA
        {
            get { return _IVA; }
            set { _IVA = value; }
        }

    }
    [DataContract(
    Name = "RespuestaValidacionFactura")]
    public class RespuestaValidacionFactura : Respuesta
    {
        [DataMember]
        public string Validacion;

    }
}
