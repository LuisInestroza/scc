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
using System.Windows.Threading;
using scc.Pacientes;
using System.Data.SqlClient;
using System.Data;
using scc.Diagnosticos;
using scc.Historiales;


namespace scc
{
    /// <summary>
    /// Lógica de interacción para Historial.xaml
    /// </summary>
    public partial class Historial : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new DispatcherTimer();

        // Valores para la el idPaciente, idDiagnostico
        public int idPaciente;
        public int idDiagnostico;

        public Historial()
        {
            InitializeComponent();
            
            

            // Mostrar el hora y fecha
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
    

            // Mostrar los detalles de la historia clinica
            string historiaClinica;

            // Datos de la historia clinica 
            historiaClinica =

                "SIGNOS VITALES:\n\n" +
                "P/A:\tFC:\tFR:\tT:\tSO2:\tP:\t\n\n" +
                "PESO:\tTALLA:\tIMC:\t\n\n" +
                "Circunferencia Abdominal:\n\n" +
                "Neurologico:\n\n" +
                "Cabeza:\n\n" +
                "Cuello:\n\n" +
                "Torax:\n" +
                "\tMamas:\n" +
                "\tCorazon:\n" +
                "\tPulmones:\n\n" +
                "Abdomen:\n\n" +
                "Genitourinario:\n\n" +
                "Extremidades:\n\n\n" +
                "Resultados de Laboratorio:\n\n" +
                "Colesterol total:\n" +
                "Colesterol HDL:\n" +
                "Colesterol LDL:\n" +
                "Trigliceridos:\n" +
                "HB:\t\tLEUCO:\n" +
                "HTO:\t\tN%:\n" +
                "PLT:\t\tL%:\n\n" +
                "Diaganostico:";

           
            // Concatenar los campos de la historia clinica
            txtHistoriaClinica.AppendText(historiaClinica);

            


        }
        

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime date;
            date = DateTime.Now;
            txtBlockFechaIngreso.Text = date.ToShortTimeString() + " " + " " + date.ToLongDateString();
        }

        private void btnBuscarPaciente_Click(object sender, RoutedEventArgs e)
        {
            BuscarPaciente abrirForm = new BuscarPaciente();
            
            abrirForm.ShowDialog();
         
            


        }

        private void btnBuscarDiagnostico_Click(object sender, RoutedEventArgs e)
        {
            Diagnostico abrir = new Diagnostico();
         
            abrir.ShowDialog();
        }

        private void btnGuardarHistoria_Click(object sender, RoutedEventArgs e)
        {
            // Validar que todos los Texbox no esten vacios
            // Y evitar una inserccion vacia 

            if (idDiagnostico == null)
            {
                MessageBox.Show("No has selecionado el diagnostico", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (idPaciente == null)
            {
                MessageBox.Show("No has seleccioando el paciente", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (txtMotivoConsulta.Text == "")
            {
                MessageBox.Show("Ingresa el Motivo de la consulta", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (txtAntecedentes.Text == "")
            {
                MessageBox.Show("Ingresa los antecendetes del paciente", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (txtTratamiento.Text == "")
            {
                MessageBox.Show("Ingresa el tratamiento del paciente", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (dpCita.Text == "")
            {
                MessageBox.Show("Ingresa la cita del paciente", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Si hay campos vacios realizar la insercion de los 
                // resgistros a la base de datos

                Historiales.Historial agregarHistoria = new Historiales.Historial();

                agregarHistoria.idDiagnostico = idDiagnostico;
                agregarHistoria.idPaciente = idPaciente;
                agregarHistoria.antecedentes = txtAntecedentes.Text;
                agregarHistoria.descripcion = txtHistoriaClinica.Text;
                agregarHistoria.motivoConsulta = txtMotivoConsulta.Text;
                agregarHistoria.tratamiento = txtTratamiento.Text;
                agregarHistoria.fechaIngreso = DateTime.Now;
                agregarHistoria.cita = Convert.ToDateTime(dpCita.Text);

                if (Historiales.Historial.InsertarHistorial(agregarHistoria))
                {
                    MessageBox.Show("La historia clinica ha sido registrad", "Registro Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Los datos no han sido registrados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

}
