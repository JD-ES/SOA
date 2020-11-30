using GestorProyectos.Modelo;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace GestorProyectos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public proyect BuscarProyectos(string ID)
        {
            try
            {
                InmobilariaEntities db = new InmobilariaEntities();
                proyecto mProyTemp = new proyecto();
                proyect mProy = new proyect();
             
                mProyTemp = db.proyectoes.Where(x => x.ID == ID).FirstOrDefault();
                mProy.ID = mProyTemp.ID;
                mProy.Nombre = mProyTemp.Nombre;
                mProy.Ubicacion = mProyTemp.Ubicacion;
                mProy.Precio = mProyTemp.Precio;
                mProy.estado = mProyTemp.estado;
                var factory = new ConnectionFactory()
                {
                    HostName = "moose.rmq.cloudamqp.com",
                    VirtualHost = "zvgcbkxw",
                    UserName = "zvgcbkxw",
                    Password = "SYn_rxQDqttTasFcUpwTa7-7-5GzPUjW"
                };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "proyecto",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    string message = "Proyecto encontrado!: ID " + ID + " " +  "el Nombre de proyecto es: " + mProy.Nombre + " "+  "Ubicado en" + mProy.Ubicacion + " " +
                         " Ver en el Mapa: click en el Mapa!!!!";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                         routingKey: "proyecto",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }

                return mProy;
            }
            catch(Exception e)
            {

                throw e;
            }
        }
    }
}
