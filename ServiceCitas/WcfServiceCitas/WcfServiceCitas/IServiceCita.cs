using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServiceCitas.Dominio;

namespace WcfServiceCitas
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceCita
    {

        [OperationContract]
        [WebInvoke(Method ="POST",UriTemplate ="Cita",ResponseFormat =WebMessageFormat.Json)]
        Cita Insertar(Cita obj);
        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Cita/{idCita}", ResponseFormat = WebMessageFormat.Json)]
        Cita ObtenerCita(string idCita);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Cita", ResponseFormat = WebMessageFormat.Json)]
        Cita Modificar(Cita obj);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Cita/{idCita}", ResponseFormat = WebMessageFormat.Json)]
        void Eliminar(string idCita);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Cita", ResponseFormat = WebMessageFormat.Json)]
        List<Cita> Listar();

    }

}
