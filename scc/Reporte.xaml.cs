using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using scc.Reportes;
using System.Data.SqlClient;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;
using Microsoft.Reporting.WinForms;


namespace scc
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {

        public string nombre;
        public string edad;
        public string sexo;
        public string motivo;
        public string fecha;
        public string enfermedad;
        public string examendFisico;

  
        public Reporte()
        {
            InitializeComponent();

           

        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSeleccionarReportePaciente_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnBuscarPacienteReporte_Click(object sender, RoutedEventArgs e)
        {
            rpReportePaciente.Reset();
            DataTable dt = GetData();
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            rpReportePaciente.LocalReport.DataSources.Add(ds);
            rpReportePaciente.LocalReport.ReportEmbeddedResource = "scc.ReportePaciente.rdlc";

            rpReportePaciente.RefreshReport();


        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");
            
            SqlCommand cmd  = conexion.EjecutarComando("sp_reportePacienteUnico");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@nombrePaciente", SqlDbType.Text));
            cmd.Parameters["@nombrePaciente"].Value = txtBuscarPacienteReporte.Text;

            cmd.Parameters.Add(new SqlParameter("@identidadPaciente", SqlDbType.Char, 13));
            cmd.Parameters["@identidadPaciente"].Value = txtBuscarPacienteIdentidad.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

       

           
    }
}

