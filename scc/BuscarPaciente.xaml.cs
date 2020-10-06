using System;
using System.Collections.Generic;
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
using scc.Pacientes;
using System.Data.SqlClient;


namespace scc
{
    /// <summary>
    /// Lógica de interacción para BuscarPaciente.xaml
    /// </summary>
    public partial class BuscarPaciente : Window
    {
        public BuscarPaciente()
        {
            InitializeComponent();
            CargarGrid();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CargarGrid()
        {
            Pacientes.Paciente listar = new Pacientes.Paciente();
            dgListaPacientes.ItemsSource = listar.ListarPacienteBuscar();

                
        }

        private void btnSeleccionarPaciente_Click(object sender, RoutedEventArgs e)
        {
            Historial historial = new Historial();
            Pacientes.Paciente selecionar = dgListaPacientes.SelectedItem as Pacientes.Paciente;


            if (selecionar != null)
            {
                int idPaciente = selecionar.idPaciente;
                string nombrePaciente = selecionar.nombrePaciente;
                string identidadPaciente = selecionar.identidadPaciente;
                string sexoPaciente = selecionar.sexo;
                int edadPaciente = selecionar.edad;

                historial.idPaciente = idPaciente;
                historial.tbNombrePaciente.Text = nombrePaciente;
                historial.tbIdentidadPaciente.Text = identidadPaciente;
                historial.tbSexo.Text = sexoPaciente;
                historial.tbEdadPaciente.Text = Convert.ToString(edadPaciente +" años");


                MessageBox.Show("Datos Seleccionados", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
                historial.Show();




            }
            else
            {
                MessageBox.Show("No hay datos seleccionados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnBuscarPacientes_Click(object sender, RoutedEventArgs e)
        {
            Pacientes.Paciente listarPacienteUnico = new Pacientes.Paciente();
            dgListaPacientes.ItemsSource = listarPacienteUnico.ListarPacienteUnico(txtBuscarPacientes.Text);
        }
    }
}
