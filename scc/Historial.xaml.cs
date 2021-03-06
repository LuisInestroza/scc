﻿using System;
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
        public int idHistoria;

        public Historial()
        {
            InitializeComponent();

           
            btnActualizarHistoria.IsEnabled = false;
            

            // Mostrar el hora y fecha
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
    

            // Mostrar los detalles de la historia clinica
             string historiaClinica;
             string antecedentes;

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


            antecedentes = "Antecedentes Pesonales Patologicos:\n\n" +
                  "Antecedentes Quirurgicos / Traumaticos:\n\n" +
                  "Antecendes Alergicos:\n\n" +
                  "Antecendes Familiares:";

            txtAntecedentes.AppendText(antecedentes);
            


   
            


        }
       
        private void Limpiar()
        {
            string historiaClinica =

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

            string antecedentes = "Antecedentes Pesonales Patologicos:\n\n" +
                  "Antecedentes Quirurgicos / Traumaticos:\n\n" +
                  "Antecendes Alergicos:\n\n" +
                  "Antecendes Familiares:";
            txtHistoriaClinica.Text = "";
            txtAntecedentes.Text = "";
            txtHistoriaClinica.AppendText(historiaClinica);
            txtAntecedentes.AppendText(antecedentes);
            txtHEA.Text = "";
            txtMotivoConsulta.Text = "";
            txtTratamiento.Text = "";
            tbClaveDiagnostico.Text = "";
            tbEdadPaciente.Text = "";
            tbIdentidadPaciente.Text = "";
            tbNombrePaciente.Text = "";
            tbNombreDiagnostico.Text = "";
            tbSexo.Text = "";
            tbSexo.Text = "";
            btnActualizarHistoria.IsEnabled = false;
            btnGuardarHistoria.IsEnabled = true;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime date;
            date = DateTime.Now;
            txtBlockFechaIngreso.Text = date.ToShortTimeString() + " " + " " + date.ToLongDateString();
        }

        private void btnBuscarPaciente_Click(object sender, RoutedEventArgs e)
        {
            BuscarPaciente abrirForm = new BuscarPaciente(this);

            abrirForm.Show(); 
         
            


        }

        private void btnBuscarDiagnostico_Click(object sender, RoutedEventArgs e)
        {
            Diagnostico abrir = new Diagnostico(this);

            abrir.Show();
        }

        private void btnGuardarHistoria_Click(object sender, RoutedEventArgs e)
        {
            // Validar que todos los Texbox no esten vacios
            // Y evitar una inserccion vacia 

            if (idPaciente == 0)
            {
                MessageBox.Show("No has selecionado el paciente", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (idDiagnostico == 0)
            {
                MessageBox.Show("No has seleccioando el diagnostico", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (txtMotivoConsulta.Text == "")
            {
                MessageBox.Show("Ingresa el Motivo de la consulta", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (txtHEA.Text == "")
            {
                MessageBox.Show("Ingresa el HEA", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
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
                agregarHistoria.HEA = txtHEA.Text;
                agregarHistoria.antecedentes = txtAntecedentes.Text;
                agregarHistoria.descripcion = txtHistoriaClinica.Text;
                agregarHistoria.motivoConsulta = txtMotivoConsulta.Text;
                agregarHistoria.tratamiento = txtTratamiento.Text;
                agregarHistoria.fechaIngreso = DateTime.Now;
                agregarHistoria.cita = Convert.ToDateTime(dpCita.Text);

                

                if (Historiales.Historial.InsertarHistorial(agregarHistoria))
                {
                    MessageBox.Show("La historia clinica ha sido registrad", "Registro Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
                    Limpiar();

               
                    
                }
                else
                {
                    MessageBox.Show("Los datos no han sido registrados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Limpiar();
                }
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBuscarHistoria_Click(object sender, RoutedEventArgs e)
        {
            BuscarHistoria abrir = new BuscarHistoria(this);
            abrir.Show();
        }

        private void btnActualizarHistoria_Click(object sender, RoutedEventArgs e)
        {
            Historiales.Historial historial = new Historiales.Historial();
            historial.idPaciente = idPaciente;
            historial.idDiagnostico = idDiagnostico;
            historial.motivoConsulta = txtMotivoConsulta.Text;
            historial.antecedentes = txtAntecedentes.Text;
            historial.tratamiento = txtTratamiento.Text;
            historial.HEA = txtHEA.Text;
            historial.descripcion = txtHistoriaClinica.Text;
            historial.cita = Convert.ToDateTime(dpCita.Text);
            historial.idHistorial = idHistoria;

            if (Historiales.Historial.ActualizarHistorial(historial))
            {
                MessageBox.Show("Los Datos Se Han Actualizados", "Actualizacion", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("Los Datos No Se Han Actualizados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
