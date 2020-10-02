using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows;

namespace scc.Diagnosticos
{
    class Diagnostico
    {
        public int id { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }

        // Constructor
        public Diagnostico() { }

        /// <summary>
        /// Metodo para listar todos los CIE de la
        /// Base de datos
        /// </summary>
        /// <returns></returns>
        public List<Diagnostico> ListarDiagnostico()
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            string sql;
            List<Diagnostico> Lista = new List<Diagnostico>();

            // Query sql
            sql = @"SELECT id, clave, nombre FROM scc.CIECAT";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            SqlDataReader rdr;
            

            try
            {
            
         
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Diagnostico resultado = new Diagnostico();
                    resultado.id = rdr.GetInt32(0);
                    resultado.clave = rdr.GetString(1);
                    resultado.nombre = rdr.GetString(2);

                    Lista.Add(resultado);

                  
                }
               
                return Lista;
                

            }
           
            catch (SqlException )
            {
                return Lista;
            }
            finally
            {
                conexion.CerrarConexion();
            }

        }
        public List<Diagnostico>ListarDiagnosticoUnico(string nombre)
        {
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            string sql;
            List<Diagnostico> Lista = new List<Diagnostico>();

            // Query sql
            sql = @"SELECT id, clave, nombre FROM scc.CIECAT WHERE nombre LIKE '%"+@nombre+"%'";

            SqlCommand cmd = conexion.EjecutarComando(sql);

            SqlDataReader rdr;

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 255).Value = nombre;

                }
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Diagnostico resultado = new Diagnostico();
                    resultado.id = rdr.GetInt32(0);
                    resultado.clave = rdr.GetString(1);
                    resultado.nombre = rdr.GetString(2);

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



    }
}
