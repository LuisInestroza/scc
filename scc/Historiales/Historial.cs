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
