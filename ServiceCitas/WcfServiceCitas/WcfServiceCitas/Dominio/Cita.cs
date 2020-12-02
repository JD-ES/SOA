using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServiceCitas.Dominio
{
    [DataContract]
    public class Cita
    {
        [DataMember]
        public int IdCita { get; set; }
        [DataMember]
        public DateTime FechaCita { get; set; }
        [DataMember]
        public string NombreCliente { get; set; }
        [DataMember]
        public bool Estado { get; set; }


    }
}