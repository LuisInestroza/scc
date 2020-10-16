using System;
using System.Collections.Generic;
using System.ComponentModel;
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


namespace scc
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
           

        
        }


        
        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la app y temrinar el proceso completo
            Application.Current.Shutdown();
        }

        private void btnWindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow abrirLogin = new MainWindow();
            this.Close();
            abrirLogin.Show();

        }
        
        private void btnFromPaciente_Click(object sender, RoutedEventArgs e)
        {
            Paciente paciente = new Paciente();
            paciente.Show();
          
        }

        private void btnFromHistorial_Click(object sender, RoutedEventArgs e)
        {
            Historial historial = new Historial();
            historial.Show();
        }

       

        private void btnFromReporte_Click(object sender, RoutedEventArgs e)
        {
            Reporte reporte = new Reporte();
            reporte.Show();

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sistema desarollado por:\n" +
                "LUIS DARIO RODRIGUEZ INESTROZA\n\n" +
                "Este sistema esta apto para el uso de un solo medico que lo va a trabajar.", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

     
    
}
