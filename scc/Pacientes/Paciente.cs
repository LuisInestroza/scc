using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace de SQL
using System.Data;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Windows;

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

        /// <summary>
        /// Metodoo para agregar un paciente a la base de datos
        /// </summary>
        /// <param name="Paciente"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo para mostrar la lista de pacientes
        /// en el form de pacientes
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo para buscar paciente para agregarlo en la historia clinica
        /// </summary>
        /// <returns></returns>
        public List<Paciente> ListarPacienteBuscar()
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            string sql;
            List<Paciente> Lista = new List<Paciente>();

            // Query sql
            sql = @"SELECT idPaciente, identidadPaciente, nombrePaciente, edad, sexo FROM scc.Paciente";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            SqlDataReader rdr;


            try
            {


                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Paciente resultado = new Paciente();
                    resultado.idPaciente = rdr.GetInt32(0);
                    resultado.identidadPaciente = rdr.GetString(1);
                    resultado.nombrePaciente = rdr.GetString(2);
                    resultado.edad = rdr.GetInt32(3);
                    resultado.sexo = rdr.GetString(4);

                    
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
        public List<Paciente> ListarPacienteUnico(string nombre)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            string sql;
            List<Paciente> Lista = new List<Paciente>();

            // Query sql
            sql = @"SELECT idPaciente, identidadPaciente, nombrePaciente, edad, sexo  
                            FROM scc.Paciente WHERE nombrePaciente LIKE '%" + @nombre + "%'";

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
                    Paciente resultado = new Paciente();
                    resultado.idPaciente = rdr.GetInt32(0);
                    resultado.identidadPaciente = rdr.GetString(1);
                    resultado.nombrePaciente = rdr.GetString(2);
                    resultado.edad = rdr.GetInt32(3);
                    resultado.sexo = rdr.GetString(4);



                    Lista.Add(resultado);
                }

                return Lista;

            }
            catch (Exception)
            {

                return Lista;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        /// <summary>
        /// Listar todos los datos de un paciente unico 
        /// para actualiar
        /// </summary>
        /// <param name="identidad"></param>
        /// <returns></returns>
        public static Paciente ListarDatosPacieneIdentidad(string identidad)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            string sql;
            Paciente resultado = new Paciente();

            // Query sql
            sql = @"SELECT * FROM scc.Paciente WHERE identidadPaciente = @idenidadPaciente" ;

            SqlCommand cmd = conexion.EjecutarComando(sql);

            SqlDataReader rdr;

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@idenidadPaciente", SqlDbType.Char, 13).Value = identidad;
                    rdr = cmd.ExecuteReader();

                }
               
                while (rdr.Read())
                {
                    
                    resultado.idPaciente = rdr.GetInt32(0);
                    resultado.nombrePaciente = rdr.GetString(1);
                    resultado.identidadPaciente = rdr.GetString(2);
                    resultado.edad = rdr.GetInt32(3);
                    resultado.sexo = rdr.GetString(4);
                    resultado.escolaridad = rdr.GetString(5);
                    resultado.lugarNacimiento = rdr.GetString(6);
                    resultado.fechaNacimiento = rdr.GetDateTime(7);
                    resultado.raza = rdr.GetString(8);
                    resultado.lugarResidencia = rdr.GetString(9);
                    resultado.religion = rdr.GetString(10);
                    resultado.estadoCivil = rdr.GetString(11);
                    resultado.correo = rdr.GetString(12);
                    resultado.telefonos = rdr.GetString(13);
                    resultado.direccion = rdr.GetString(14);



                   
                }

                return resultado;

            }
            catch (Exception)
            {

                return resultado;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        /// <summary>
        /// metodo para actualizar paciente
        /// </summary>
        /// <param name="Paciente"></param>
        /// <returns></returns>
        public static bool ActulizarPaciente(Paciente Paciente)
        {
            Conexion conn = new Conexion(@"(local)\sqlexpress", "scc");

            // Ejecutar el Store Procedure
            SqlCommand cmd = conn.EjecutarComando("sp_ActualizarPaciente");
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
            Paciente paciente = new Paciente();
            paciente = Paciente.ListarDatosPacieneIdentidad(paciente.identidadPaciente);

            // Realizar el registro del paciente
            try
            {

                if(paciente.identidadPaciente == "")
                {
                    MessageBox.Show("Paciente No Existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                
                }
                else
                {
                    // Conexion
                    conn.EstablecerConexion();

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();

                    // Retornar 
                    return true;
                }
              

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



    }
}
