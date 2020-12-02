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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace GestorProyectos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public bool AgregarProyectos(proyecto mPro)
        {
            try
            {
                InmobilariaEntities contexto = new InmobilariaEntities();
                contexto.proyectoes.Add(mPro);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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
                    HostName = "Host",
                    VirtualHost = "user",
                    UserName = "user",
                    Password = "Password"
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

        public bool EliminarProyectos(string ID)
        {
            try 
            {
                InmobilariaEntities db = new InmobilariaEntities();
                var user = db.proyectoes.Single(x => x.ID == ID);
                db.proyectoes.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return false;
            }

        }

        public bool ExisteProyectos(string ID)
        {
            try
            {
                proyecto mProyect = new proyecto();
                InmobilariaEntities db = new InmobilariaEntities();
                mProyect = db.proyectoes.Where(x => x.ID == ID).FirstOrDefault();
                if (mProyect != null)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        } 

        public bool ModificarProyectos(proyecto mProye)
        {
            try
            {
                if (true)
                {

                    proyecto mProy = new proyecto();
                    InmobilariaEntities db = new InmobilariaEntities();
                    mProy = db.proyectoes.Where(x => x.ID == mProye.ID).FirstOrDefault();
                    //mProy.ID = mProye.ID;
                    mProy.Nombre = mProye.Nombre;
                    mProy.Ubicacion = mProye.Ubicacion;
                    mProy.Precio = mProye.Precio;
                    mProy.estado = mProye.estado;
                    var accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
                    var authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

                    TwilioClient.Init(accountSid, authToken);

                    var messageOptions = new CreateMessageOptions(
                        new PhoneNumber("whatsapp:+number"));
                    messageOptions.From = new PhoneNumber("whatsapp:+number");
                    messageOptions.Body = "El Id del proyecto es: " +mProye.ID + "" + "El nombre del proyectos es " + " " +
                    mProye.Nombre + " " + "Ubicado en: " + mProye.Ubicacion;
                    var message = MessageResource.Create(messageOptions);
                    Console.WriteLine(message.Body);
                    db.SaveChanges();
                    return true;
                }
            }

            catch (Exception e)
            {
                return false;
            }
        }
    }
}
