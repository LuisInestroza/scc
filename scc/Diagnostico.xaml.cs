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

using scc.Diagnosticos;


namespace scc
{
    /// <summary>
    /// Lógica de interacción para Diagnostico.xaml
    /// </summary>
    public partial class Diagnostico : Window
    {
        public Diagnostico()
        {
            InitializeComponent();
            CargarGrid();
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void CargarGrid()
        {
            Diagnosticos.Diagnostico listar = new Diagnosticos.Diagnostico();
            dgDiagnosticoCIE.ItemsSource = listar.ListarDiagnostico();



        }

        private void btnBuscarDiagnostico_Click(object sender, RoutedEventArgs e)
        {
            Diagnosticos.Diagnostico listarCIE = new Diagnosticos.Diagnostico();
            dgDiagnosticoCIE.ItemsSource = listarCIE.ListarDiagnosticoUnico(txtBuscarCIE.Text);
        }

      
        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            Historial historial = new Historial();
            Diagnosticos.Diagnostico selecionar = dgDiagnosticoCIE.SelectedItem as Diagnosticos.Diagnostico;


            if (selecionar != null)
            {
                int id = selecionar.id;
                string clave = selecionar.clave;
                string nombre = selecionar.nombre;

                historial.idDiagnostico = id;
                historial.tbClaveDiagnostico.Text = clave.ToString();
                historial.tbNombreDiagnostico.Text = nombre.ToString();
                MessageBox.Show("Datos Seleccionados", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                historial.Show();
                this.Close();
              
                



            }
            else
            {
                MessageBox.Show("No hay datos seleccionados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}
