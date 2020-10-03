using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace de SQL
using System.Data;
using System.Data.SqlClient;
using System.IO.Packaging;

namespace scc.Pacientes
{
    class Paciente
    {
        // Propiedades de la clase paciente
        public int idPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public string identidadPaciente { get; set; }
        public int edad { get; set; }
        public string sexo { get; set; }
        public string escolaridad { get; set; }
        public string lugarNacimiento { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string raza { get; set; }
        public string lugarResidencia { get; set; }
        public string religion { get; set; }
        public string estadoCivil { get; set; }
        public string correo { get; set; }
        public string telefonos { get; set; }
        public string direccion { get; set; }


        //Construcor de la clase Paciente
        public Paciente() { }

        // Metodos de la clase Paciente

        // Metodo de Insertar
        public static bool InsertarPaciente(Paciente Paciente)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "scc");

            // Ejecutar el Store Procedure
            SqlCommand cmd = conn.EjecutarComando("sp_AgregarPaciente");
            cmd.CommandType = CommandType.StoredProcedure;

            //Agregar los parametros junto con las propiedades de la clases

            cmd.Parameters.Add(new SqlParameter("@nombrePaciente", SqlDbType.Text));
            cmd.Parameters["@nombrePaciente"].Value = Paciente.nombrePaciente;

            cmd.Parameters.Add(new SqlParameter("@identidadPaciente", SqlDbType.Char, 13));
            cmd.Parameters["@identidadPaciente"].Value = Paciente.identidadPaciente;

            cmd.Parameters.Add(new SqlParameter("@edad", SqlDbType.Int));
            cmd.Parameters["@edad"].Value = Paciente.edad;

            cmd.Parameters.Add(new SqlParameter("@sexo", SqlDbType.Text));
            cmd.Parameters["@sexo"].Value = Paciente.sexo;

            cmd.Parameters.Add(new SqlParameter("@escolaridad", SqlDbType.Text));
            cmd.Parameters["@escolaridad"].Value = Paciente.escolaridad;

            cmd.Parameters.Add(new SqlParameter("@lugarNacimiento", SqlDbType.Text));
            cmd.Parameters["@lugarNacimiento"].Value = Paciente.lugarNacimiento;

            cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", SqlDbType.Date));
            cmd.Parameters["@fechaNacimiento"].Value = Paciente.fechaNacimiento;

            cmd.Parameters.Add(new SqlParameter("@raza", SqlDbType.Text));
            cmd.Parameters["@raza"].Value = Paciente.raza;

            cmd.Parameters.Add(new SqlParameter("@lugarResidencia", SqlDbType.Text));
            cmd.Parameters["@lugarResidencia"].Value = Paciente.lugarResidencia;

            cmd.Parameters.Add(new SqlParameter("@religion", SqlDbType.Text));
            cmd.Parameters["@religion"].Value = Paciente.religion;

            cmd.Parameters.Add(new SqlParameter("@estadoCivil", SqlDbType.Text));
            cmd.Parameters["@estadoCivil"].Value = Paciente.estadoCivil;

            cmd.Parameters.Add(new SqlParameter("@correo", SqlDbType.Text));
            cmd.Parameters["@correo"].Value = Paciente.correo;

            cmd.Parameters.Add(new SqlParameter("@telefonos", SqlDbType.Text));
            cmd.Parameters["@telefonos"].Value = Paciente.telefonos;

            cmd.Parameters.Add(new SqlParameter("@direccion", SqlDbType.Text));
            cmd.Parameters["@direccion"].Value = Paciente.direccion;

            // Realizar el registro del paciente
            try
            {
                // Conexion
                conn.EstablecerConexion();

                // Ejecutar el comando
                cmd.ExecuteNonQuery();

                // Retornar 
                return true;

            }
            catch (SqlException)
            {

                return false;

            }
            finally
            {
                conn.CerrarConexion();
            }



        }

        // Metodo para listar todos los pacientes por el nombre
        public static List<Paciente> ListarPaciente()
        {
            List<Paciente> paciente = new List<Paciente>();

            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");
            string sql;
            

            // Query para listar todos los pacientes
            sql = @"SELECT nombrePaciente FROM scc.Paciente";

            // Comando
            SqlCommand cmd = conexion.EjecutarComando(sql);

            try
            {
                // Establecer la conexion
                conexion.EstablecerConexion();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Paciente listaPaciente = new Paciente();
                    listaPaciente.nombrePaciente = rdr.GetString(0);

                    paciente.Add(listaPaciente);
                }

                return paciente;
            }
            catch (Exception)
            {

                return paciente;

            }
            finally
            {

                conexion.CerrarConexion();
            }
           
        }

      



    }
}
