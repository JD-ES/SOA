using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProyect
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCrearProyecto()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Proyecto AgregarAProyectos = new Proyecto()
            {
                ID = "13",
                Nombre = "Costa verde",
                Ubicacion = "Miraflores",
                Precio = "20000",
                estado = "Activo"
                    
            };
            string postdata = js.Serialize(AgregarAProyectos);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:59123/Service1.svc/add");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            Proyecto proyectoCreado = js.Deserialize<Proyecto>(tramaJson);
            Assert.AreEqual("13", proyectoCreado.ID);
            Assert.AreEqual("Costa verde", proyectoCreado.Nombre);
            Assert.AreEqual("Miraflores", proyectoCreado.Ubicacion);
            Assert.AreEqual("20000", proyectoCreado.Precio);
            Assert.AreEqual("Activo", proyectoCreado.estado);
        }
        [TestMethod]
        public void BuscarProyecto() { 
        }
    }
}
