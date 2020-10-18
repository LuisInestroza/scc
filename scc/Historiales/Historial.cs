using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scc.Historiales
{
    class Historial
    {
        // Propiedades de la clase Historial
        public int idHistorial { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string motivoConsulta { get; set; }
        public string antecedentes { get; set; }
        public string descripcion { get; set; }
        public string tratamiento { get; set; }
        public DateTime cita{ get; set; }
        public int idPaciente { get; set; }
        public int idDiagnostico { get; set; }
        public string HEA { get; set; }

        // Parametros para el inner join

        // Pacientes
        public string nombrePaciente { get; set; }
        public string identidadPaciente { get; set; }
        public int edadPaciente { get; set; }
        public string sexoPaciente{ get; set; }
        //Diagnostico
        public string claveDiagnostico { get; set; }
        public string nombreEnfermedad { get; set; }


        // Constructor de la clase
        public Historial() { }

        public static bool InsertarHistorial(Historial historial)
        {
            // Realizar la conexion

            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            //Ejecutar el Store 
            SqlCommand cmd = conexion.EjecutarComando("sp_AgregarHistorial");
            cmd.CommandType = CommandType.StoredProcedure;

            // Agregar los parametros del Store Procedure  
            // A los atribustos de la clase 

            cmd.Parameters.Add(new SqlParameter("@fechaIngreso", SqlDbType.DateTime));
            cmd.Parameters["@fechaIngreso"].Value = historial.fechaIngreso;

            cmd.Parameters.Add(new SqlParameter("@motivoConsulta", SqlDbType.Text));
            cmd.Parameters["@motivoConsulta"].Value = historial.motivoConsulta;

            cmd.Parameters.Add(new SqlParameter("@antecedentes", SqlDbType.Text));
            cmd.Parameters["@antecedentes"].Value = historial.antecedentes;

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.Text));
            cmd.Parameters["@descripcion"].Value = historial.descripcion;

            cmd.Parameters.Add(new SqlParameter("@tratamiento", SqlDbType.Text));
            cmd.Parameters["@tratamiento"].Value = historial.tratamiento;

            cmd.Parameters.Add(new SqlParameter("@cita", SqlDbType.Date));
            cmd.Parameters["@cita"].Value = historial.cita;

            cmd.Parameters.Add(new SqlParameter("@idPaciente", SqlDbType.Int));
            cmd.Parameters["@idPaciente"].Value = historial.idPaciente;

            cmd.Parameters.Add(new SqlParameter("@idDiagnostico", SqlDbType.Int));
            cmd.Parameters["@idDiagnostico"].Value = historial.idDiagnostico;

            cmd.Parameters.Add(new SqlParameter("@HEA", SqlDbType.Text));
            cmd.Parameters["@HEA"].Value = historial.HEA;


            // Realizar la el registro del Historial
            try
            {
                // Conexion a la base de datos
                conexion.EstablecerConexion();

                // Ejecutar el Store Procedure
                cmd.ExecuteNonQuery();

                // Retornar los valores
                return true;

            }
            catch (SqlException)
            {

                return false; 
            }
            finally
            {
                conexion.CerrarConexion();
            }
        
        
        
        }

        /// <summary>
        /// Metodo para listar todas las historias
        /// clinicas registradas
        /// </summary>
        /// <returns></returns>
        public List<Historial> ListarHistoriaClinca()
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            string sql;
            List<Historial> Lista = new List<Historial>();

            // Query sql
            sql = @"SELECT Paciente.idPaciente, identidadPaciente, nombrePaciente, edad, sexo, idHistorial, motivoConsulta,HEA, antecedentes,tratamiento, fechaIngreso,cita, descripcion, ID, Clave, Nombre 
	                            FROM scc.Paciente Paciente 
		                            INNER JOIN scc.Historial Historial 
			                            ON Paciente.idPaciente = Historial.idPaciente
		                            INNER JOIN scc.CIECAT Diagnostico
			                            ON Diagnostico.ID = Historial.idDiagnostico";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            SqlDataReader rdr;


            try
            {

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Historial resultado = new Historial();
                    resultado.idPaciente = rdr.GetInt32(0);
                    resultado.identidadPaciente = rdr.GetString(1);
                    resultado.nombrePaciente = rdr.GetString(2);
                    resultado.edadPaciente = rdr.GetInt32(3);
                    resultado.sexoPaciente = rdr.GetString(4);
                    resultado.idHistorial = rdr.GetInt32(5);
                    resultado.motivoConsulta = rdr.GetString(6);
                    resultado.HEA = rdr.GetString(7);
                    resultado.antecedentes = rdr.GetString(8);
                    resultado.tratamiento = rdr.GetString(9);
                    resultado.fechaIngreso = rdr.GetDateTime(10);
                    resultado.cita = rdr.GetDateTime(11);
                    resultado.descripcion = rdr.GetString(12);
                    resultado.idDiagnostico = rdr.GetInt32(13);
                    resultado.claveDiagnostico = rdr.GetString(14);
                    resultado.nombreEnfermedad = rdr.GetString(15);
                  

                    Lista.Add(resultado);


                }

                return Lista;


            }

            catch (SqlException)
            {
                return Lista;
            }
            finally
            {
                conexion.CerrarConexion();
            }

        }

        /// <summary>
        /// Listar las historia de un paciente especifico
        /// </summary>
        /// <returns></returns>
        public List<Historial> ListarHistoriaClincaUnica(string nombre)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            string sql;
            List<Historial> Lista = new List<Historial>();

            // Query sql
            sql = @"SELECT Paciente.idPaciente, identidadPaciente, nombrePaciente, edad, sexo, idHistorial, motivoConsulta,HEA, antecedentes,tratamiento, fechaIngreso,cita, descripcion, ID, Clave, Nombre 
	                            FROM scc.Paciente Paciente 
		                            INNER JOIN scc.Historial Historial 
			                            ON Paciente.idPaciente = Historial.idPaciente
		                            INNER JOIN scc.CIECAT Diagnostico
			                            ON Diagnostico.ID = Historial.idDiagnostico
                    WHERE CONVERT(varchar, nombrePaciente) LIKE '%" + @nombre + "%'";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            SqlDataReader rdr;


            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@nombre", SqlDbType.Text).Value = nombre;

                }
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Historial resultado = new Historial();
                    resultado.idPaciente = rdr.GetInt32(0);
                    resultado.identidadPaciente = rdr.GetString(1);
                    resultado.nombrePaciente = rdr.GetString(2);
                    resultado.edadPaciente = rdr.GetInt32(3);
                    resultado.sexoPaciente = rdr.GetString(4);
                    resultado.idHistorial = rdr.GetInt32(5);
                    resultado.motivoConsulta = rdr.GetString(6);
                    resultado.HEA = rdr.GetString(7);
                    resultado.antecedentes = rdr.GetString(8);
                    resultado.tratamiento = rdr.GetString(9);
                    resultado.fechaIngreso = rdr.GetDateTime(10);
                    resultado.cita = rdr.GetDateTime(11);
                    resultado.descripcion = rdr.GetString(12);
                    resultado.idDiagnostico = rdr.GetInt32(13);
                    resultado.claveDiagnostico = rdr.GetString(14);
                    resultado.nombreEnfermedad = rdr.GetString(15);


                    Lista.Add(resultado);


                }

                return Lista;


            }

            catch (SqlException)
            {
                return Lista;
            }
            finally
            {
                conexion.CerrarConexion();
            }

        }
        /// <summary>
        /// Metodo para acutalizar una historia clinoca
        /// </summary>
        /// <param name="historial"></param>
        /// <returns></returns>

        public static bool ActualizarHistorial(Historial historial)
        {
            // Realizar la conexion

            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            //Ejecutar el Store 
            SqlCommand cmd = conexion.EjecutarComando("sp_ActualizarHistoria");
            cmd.CommandType = CommandType.StoredProcedure;

            // Agregar los parametros del Store Procedure  
            // A los atribustos de la clase 

            cmd.Parameters.Add(new SqlParameter("@idHistorial", SqlDbType.Int));
            cmd.Parameters["@idHistorial"].Value = historial.idHistorial;

            cmd.Parameters.Add(new SqlParameter("@motivoConsulta", SqlDbType.Text));
            cmd.Parameters["@motivoConsulta"].Value = historial.motivoConsulta;

            cmd.Parameters.Add(new SqlParameter("@antecedentes", SqlDbType.Text));
            cmd.Parameters["@antecedentes"].Value = historial.antecedentes;

            cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.Text));
            cmd.Parameters["@descripcion"].Value = historial.descripcion;

            cmd.Parameters.Add(new SqlParameter("@tratamiento", SqlDbType.Text));
            cmd.Parameters["@tratamiento"].Value = historial.tratamiento;

            cmd.Parameters.Add(new SqlParameter("@cita", SqlDbType.Date));
            cmd.Parameters["@cita"].Value = historial.cita;

            cmd.Parameters.Add(new SqlParameter("@idPaciente", SqlDbType.Int));
            cmd.Parameters["@idPaciente"].Value = historial.idPaciente;

            cmd.Parameters.Add(new SqlParameter("@idDiagnostico", SqlDbType.Int));
            cmd.Parameters["@idDiagnostico"].Value = historial.idDiagnostico;

            cmd.Parameters.Add(new SqlParameter("@HEA", SqlDbType.Text));
            cmd.Parameters["@HEA"].Value = historial.HEA;


            // Realizar la el registro del Historial
            try
            {
                // Conexion a la base de datos
                conexion.EstablecerConexion();

                // Ejecutar el Store Procedure
                cmd.ExecuteNonQuery();

                // Retornar los valores
                return true;

            }
            catch (SqlException)
            {

                return false;
            }
            finally
            {
                conexion.CerrarConexion();
            }



        }




    }
}
