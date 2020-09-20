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

namespace scc
{
    /// <summary>
    /// Lógica de interacción para Historial.xaml
    /// </summary>
    public partial class Historial : Window
    {
        public Historial()
        {
            InitializeComponent();
            //Mostrar la fecha y hor actual
            txtBlockFechaIngreso.Text = DateTime.Now.ToString();

            // Mostrar los detalles de la historia clinica
            string historiaClinica;

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
                "Extremidades:\n\n";

            txtHistoriaClinica.AppendText(historiaClinica);
        }
    }
}
