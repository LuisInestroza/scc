using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace scc.Reportes
{

    class Reporte
    {
        public string nombrePaciente { get; set; }
        public int edadPaciente { get; set; }
        public string sexoPaciente { get; set; }
        public string motivoConsulta { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string examenFisico { get; set; }
        public string nombreEnfermedad { get; set; }



 

        public List<Reporte> ListarReportePacienteUnico(string nombre)
        {
            //Conexion a la base de datos
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");

            List<Reporte> Lista = new List<Reporte>();
            string sql = @"SELECT nombrePaciente, edad, sexo, motivoConsulta, fechaIngreso, descripcion, Nombre 
	                            FROM scc.Paciente Paciente 
		                            INNER JOIN scc.Historial Historial 
			                            ON Paciente.idPaciente = Historial.idPaciente
		                            INNER JOIN scc.CIECAT Diagnostico
			                            ON Diagnostico.ID = Historial.idDiagnostico	
                            WHERE nombrePaciente LIKE '%" + @nombre + "%'";

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
                    Reporte resultado = new Reporte();
                    resultado.nombrePaciente = rdr.GetString(0);
                    resultado.edadPaciente = rdr.GetInt32(1);
                    resultado.sexoPaciente = rdr.GetString(2);
                    resultado.motivoConsulta = rdr.GetString(3);
                    resultado.fechaIngreso = rdr.GetDateTime(4);
                    resultado.examenFisico = rdr.GetString(5);
                    resultado.nombreEnfermedad = rdr.GetString(6);

                    
                   

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
