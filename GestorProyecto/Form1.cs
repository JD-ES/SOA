using GestorProyecto.Modelo;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GestorProyecto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RestClient proxy = new RestClient("http://localhost:59123/Service1.svc");
                RestRequest request = new RestRequest("add", Method.POST, DataFormat.Json);
                proyecto mPro = new proyecto();
                mPro.ID = textBox1.Text;
                mPro.Nombre = textBox2.Text;
                mPro.Ubicacion = textBox3.Text;
                mPro.Precio = textBox4.Text;
                mPro.estado = textBox5.Text;
                request.AddJsonBody(mPro);
                IRestResponse response = proxy.Execute(request);
            }
            catch (Exception ex)
            {
                label6.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                RestClient proxy = new RestClient("http://localhost:59123/Service1.svc");
                RestRequest request = new RestRequest("update", Method.PUT, DataFormat.Json);
                proyecto mProj = new proyecto();
                mProj.ID = textBox1.Text;
                mProj.Nombre = textBox2.Text;
                mProj.Ubicacion = textBox3.Text;
                mProj.Precio = textBox4.Text;
                mProj.estado = textBox5.Text;
                request.AddJsonBody(mProj);
                IRestResponse response = proxy.Execute(request);
            }
            catch (Exception ex)
            {
                label6.Text = ex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
            string ID = textBox1.Text;
            RestClient proxy = new RestClient("http://localhost:59123/Service1.svc");
            RestRequest request = new RestRequest("delete/"+ ID , Method.DELETE);

            IRestResponse response = proxy.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
