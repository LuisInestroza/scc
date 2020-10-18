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
using scc.Historiales;

namespace scc
{
    /// <summary>
    /// Lógica de interacción para BuscarHistoria.xaml
    /// </summary>
    public partial class BuscarHistoria : Window
    {
        public BuscarHistoria(Historial frHistoria)
        {
            InitializeComponent();
            this.formHistorial = frHistoria;
            CargarGrid();
        }

        private Historial formHistorial;
        private void btnCerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CargarGrid()
        {
            Historiales.Historial listar = new Historiales.Historial();
            dgListaHistoria.ItemsSource = listar.ListarHistoriaClinca();
        }
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnSeleccionarHistoria_Click(object sender, RoutedEventArgs e)
        {
            Historiales.Historial seleccionar = dgListaHistoria.SelectedItem as Historiales.Historial;
            if(formHistorial != null)
            {
                // Paciente
                int idPaciente = seleccionar.idPaciente;
                string identidadPaciente = seleccionar.identidadPaciente;
                string nombrePaciente = seleccionar.nombrePaciente;
                string edadPaciente = Convert.ToString(seleccionar.edadPaciente);
                string sexoPaciente = seleccionar.sexoPaciente;
                // Diagnostico
                int idDiagnostico = seleccionar.idDiagnostico;
                string claveEnfermedad = seleccionar.claveDiagnostico;
                string enfermedad = seleccionar.nombreEnfermedad;
                // Historial
                int idHistoria = seleccionar.idHistorial;
                string motivo = seleccionar.motivoConsulta;
                string HEA = seleccionar.HEA;
                string antecedentes = seleccionar.antecedentes;
                string tratamiento = seleccionar.tratamiento;
                string examenFisico = seleccionar.descripcion;
                string cita = Convert.ToString(seleccionar.cita);

                // Llenar los datos del form Historia clinica
                // Paciente
                formHistorial.idPaciente = idPaciente;
                formHistorial.tbIdentidadPaciente.Text = identidadPaciente;
                formHistorial.tbNombrePaciente.Text = nombrePaciente;
                formHistorial.tbEdadPaciente.Text = edadPaciente+" años";
                formHistorial.tbSexo.Text = sexoPaciente;
                //Diagnostico
                formHistorial.idDiagnostico = idDiagnostico;
                formHistorial.tbClaveDiagnostico.Text = claveEnfermedad;
                formHistorial.tbNombreDiagnostico.Text = enfermedad;

                //Historia Clinica
                formHistorial.idHistoria = idHistoria;
                formHistorial.txtMotivoConsulta.Text = motivo;
                formHistorial.txtHEA.Text = HEA;
                formHistorial.txtAntecedentes.Text = "";
                formHistorial.txtAntecedentes.Text = antecedentes;
                formHistorial.txtTratamiento.Text = tratamiento;
                formHistorial.dpCita.Text = cita;
                formHistorial.txtHistoriaClinica.Text = "";
                formHistorial.txtHistoriaClinica.Text = examenFisico;

                MessageBox.Show("Datos Seleccionados", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                if(formHistorial.idDiagnostico != 0)
                {
                    formHistorial.btnActualizarHistoria.IsEnabled =true;
                    formHistorial.btnGuardarHistoria.IsEnabled = false;
                    this.Close();
                }

               

            }
            else
            {
                MessageBox.Show("No hay datos seleccionados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }

        private void btnBuscarHistoria_Click(object sender, RoutedEventArgs e)
        {
            Historiales.Historial listarHistoriaUnico = new Historiales.Historial();
            dgListaHistoria.ItemsSource = listarHistoriaUnico.ListarHistoriaClincaUnica(txtBuscarHistoria.Text);
        }
    }
}
