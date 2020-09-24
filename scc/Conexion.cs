
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Nacespace de sql 
using System.Data;
using System.Data.SqlClient;

namespace scc
{
    class Conexion
    {

        // Propiedades
        private string servidor;
        private string baseDatos;
        public SqlConnection conn;
        public SqlCommand cmd;


        //Constructor 
        public Conexion() { }
        public Conexion(string sqlServidor, string sqlBDD) {

            servidor = sqlServidor;
            baseDatos = sqlBDD;
            EstablecerConexion();
        }


        private void EstablecerConexion()
        {
            try 
            {   
                // Realizar la conexion
                conn = new SqlConnection(@"server=" + servidor + ";" + "integrated security = true; database = " + baseDatos + ";");
                // Abrir la conexion
                conn.Open();
            }
            catch (Exception)
            {
                throw new Exception("Servidor Y Base de datos no encontrados");
            }

         

        }


        public SqlCommand EjecutarComando(string sqlComando)
        {
            return cmd = new SqlCommand(sqlComando, conn);
        }


        public void CerrarConexion()
        {
            conn.Close();
        }
    }
}
