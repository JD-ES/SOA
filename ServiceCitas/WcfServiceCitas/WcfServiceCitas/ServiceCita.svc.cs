using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServiceCitas.Dominio;
using WcfServiceCitas.Errores;
using WcfServiceCitas.Persistencia;

namespace WcfServiceCitas
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IServiceCita
    {
        private CitaDAO citaDAO = new CitaDAO();

        public void Eliminar(string idCita)
        {
            citaDAO.Eliminar(idCita);
        }
        
        public Cita Insertar(Cita obj)
        {
            Cita citaExistencia = citaDAO.ObtenerCita(obj.IdCita.ToString());
            if (citaExistencia != null) {
                throw new WebFaultException<DuplicadoException>(new DuplicadoException()
                {
                    Codigo = 102,
                    Descripcion="Cita duplicada"
                },System.Net.HttpStatusCode.Conflict);

            }
            return citaDAO.Insertar(obj);
        }

        public List<Cita> Listar()
        {
            return citaDAO.Listar();
        }

        public Cita Modificar(Cita obj)
        {
             return citaDAO.Modificar(obj);
        }

        public Cita ObtenerCita(string idCita)
        {
            return citaDAO.ObtenerCita(idCita);
        }


    }
}
