using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using scc.Usuarios;

namespace scc
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    

        private void btnInisiarSesion_Click(object sender, RoutedEventArgs e)
        {
            // Crear la conexion
            Conexion conexion = new Conexion(@"(local)\sqlexpress", "scc");
            // Llamar la clase
            Usuarios.Usuario usuario = new Usuarios.Usuario();
            // Query
            string sql = @"SELECT nombreUsuario, password FROM scc.Usuario WHERE nombreUsuario = @usuario";
            // Ejecuatar el query
            SqlCommand cmd = conexion.EjecutarComando(sql);
            // Variables de los TextBox
            string nombreUsuario = txtNombreUsuario.Text;
            string password = txtPassword.Password;

            // Leer los datos
            SqlDataReader rdr;

            try
            {

                using (cmd)
                {
                    // Ver los parametros
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 255).Value = nombreUsuario;
                   
                    rdr = cmd.ExecuteReader();
                }




                // Leer los datos
                while (rdr.Read())
                {
                    usuario.nombreUsuario = rdr.GetString(0);
                    usuario.contrasena = rdr.GetString(1);

                }

                // Verificar que los TextBox no esten vacios
                if (nombreUsuario == "")
                {
                    MessageBox.Show("Ingresa tu Nombre de Usuario", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (password == "")
                {
                    MessageBox.Show("Ingresa tu Contraseña", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                // Si no exita un nombre de usuario registrado en la base de datos.
                else if(usuario.nombreUsuario == null)
                {
                    MessageBox.Show("El Usuario No Existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // Virificar si la contrasena es incorrecta
                else if ( password != usuario.contrasena)
                {
                    MessageBox.Show("Contraseña Incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {   // Si no hayb errores en las credenciales 
                    // Iniciar session
                    Menu abrirMenu = new Menu();
                    this.Hide();
                    abrirMenu.ShowDialog();
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

        private void btnCloseWindowLogin_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnWindowMinimizeLogin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
