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

        
    }
}
