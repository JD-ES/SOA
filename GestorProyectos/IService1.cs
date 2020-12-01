using GestorProyectos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GestorProyectos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
         [WebInvoke(UriTemplate = "proyectos/{ID}", Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        proyect BuscarProyectos(string ID);
        [OperationContract]
         [WebInvoke(UriTemplate = "add", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool AgregarProyectos(proyecto mPro);
        [OperationContract]
       [WebInvoke(UriTemplate = "update", Method = "PUT", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool ModificarProyectos(proyecto mProye);

        [OperationContract]
          [WebInvoke(UriTemplate = "exist", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool ExisteProyectos(string ID);
        [OperationContract]
         [WebInvoke(UriTemplate = "delete/{ID}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool EliminarProyectos(string ID);

    }

}
