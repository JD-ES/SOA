using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WcfServiceCitas.Dominio;

namespace WcfServiceCitas.Persistencia
{
    public class CitaDAO
    {
        private string CadenaConexionDB = "Data Source=192.168.10.82;Initial Catalog=Delivery_Movil;User ID=sa;Password=Bdsql08";

        public Cita Insertar(Cita obj)
        {

            Cita proyecto = null;
            string sql = "INSERT INTO Cita VALUES(@IdCita,@Fecha,@NombreCliente,@Estado)";
            using (SqlConnection conexion = new SqlConnection(CadenaConexionDB))
            {
                conexion.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdCita", obj.IdCita));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", obj.NombreCliente));
                    cmd.Parameters.Add(new SqlParameter("@NombreCliente", obj.NombreCliente));
                    cmd.Parameters.Add(new SqlParameter("@Estado", obj.Estado));

                }

                proyecto = ObtenerCita(proyecto.IdCita.ToString());
                return proyecto;

            }
        }

        public Cita ObtenerCita(string idCita)
        {
            Cita proyecto = null;
            string sql = "select * from Cita where IdCita=@IdCita";
            using (SqlConnection conexion = new SqlConnection(CadenaConexionDB))
            {
                conexion.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdCita", idCita));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proyecto = new Cita()
                            {
                                IdCita = Convert.ToInt32(reader["IdCita"]),
                                FechaCita = Convert.ToDateTime(reader["FechaCita"]),
                                NombreCliente = Convert.ToString(reader["NombreCliente"]),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };

                        }

                    }
                }

                return proyecto;

            }
        }


        public Cita Modificar(Cita obj) {
            Cita citaModificado = null;

            string sentencia = "UPDATE CITA SET FechaCita=@FechaCita,NombreCliente=@NombreCliente where IdCita=@IdCita";

            using (SqlConnection conexion = new SqlConnection(CadenaConexionDB)) {

                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sentencia, conexion)) {

                    comando.Parameters.Add(new SqlParameter("FechaCita",obj.FechaCita));
                    comando.Parameters.Add(new SqlParameter("NombreCliente", obj.FechaCita));
                    comando.Parameters.Add(new SqlParameter("IdCita", obj.IdCita));
                    comando.ExecuteNonQuery();
                }
            }
            citaModificado = ObtenerCita(obj.IdCita.ToString());
           return citaModificado;

        }


        public void Eliminar(string idCita)
        {
         
            string sentencia = "delete from Cita where IdCita=@IdCita";

            using (SqlConnection conexion = new SqlConnection(CadenaConexionDB))
            {

                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                {

                    comando.Parameters.Add(new SqlParameter("IdCita", idCita));
                    
                    comando.ExecuteNonQuery();
                }
            }
        }



        public List<Cita> Listar()
        {
            List<Cita> listadoCita = null;
            Cita citaEncontrado = null;

            string sentencia = "select IdCita,NombreCliente,FechaCita,Estado from Cita";

            using (SqlConnection conexion = new SqlConnection(CadenaConexionDB))
            {

                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sentencia, conexion))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader()) {

                        while (resultado.Read())
                        {
                            citaEncontrado = new Cita()
                            {
                                IdCita = (int)resultado["IdCita"],
                                NombreCliente = (string)resultado["NombreCliente"],
                                FechaCita = (DateTime)resultado["FechaCita"],
                                Estado = (bool)resultado["Estado"]
                            };
                        }
                    }
                }
            }
            return listadoCita;
        }
    }
}