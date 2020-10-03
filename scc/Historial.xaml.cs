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

namespace scc
{
    /// <summary>
    /// Lógica de interacción para Historial.xaml
    /// </summary>
    public partial class Historial : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new DispatcherTimer();

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
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");
            Pacientes.Paciente paciente = new Pacientes.Paciente();
            
            string sql;


            // Query para listar todos los pacientes
            sql = @"SELECT idPaciente, nombrePaciente, edad, identidadPaciente FROM scc.Paciente WHERE identidadPaciente = @identidad";

            // Comando
            SqlCommand cmd = conexion.EjecutarComando(sql);

            string identidad = txtBuscarPaciente.Text;
            SqlDataReader rdr;

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@identidad", SqlDbType.Char, 13).Value = identidad;
                    rdr = cmd.ExecuteReader();

                }
                


                while (rdr.Read())
                {
                    paciente.idPaciente = rdr.GetInt32(0);
                    paciente.nombrePaciente = rdr.GetString(1);
                    paciente.edad = rdr.GetInt32(2);
                    paciente.identidadPaciente = rdr.GetString(3);
                   
                }
                
                if(paciente.identidadPaciente == null)
                {
                    MessageBox.Show("Paciente No Existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    tbNombrePaciente.Text = paciente.nombrePaciente;
                    tbEdadPaciente.Text = Convert.ToString(paciente.edad) +" años";

                }
               
               
            }
            catch (Exception)
            {

                throw;

            }
            finally
            {

                conexion.CerrarConexion();
            }








        }
    }
}
