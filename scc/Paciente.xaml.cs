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


namespace scc
{
    /// <summary>
    /// Lógica de interacción para Paciente.xaml
    /// </summary>
    public partial class Paciente : Window
    {
        public Paciente()
        {
            InitializeComponent();
            listaPaciente();
            btnActualizarPaciente.IsEnabled = false;
        }

        /// <summary>
        /// Metodo para listar pacientes almacenados en 
        /// la base de datos
        /// </summary>
        public void listaPaciente()
        {
            // Llamar la clase
            Pacientes.Paciente nuevo = new Pacientes.Paciente();
            // Crear la lista
            List<Pacientes.Paciente> lista = Pacientes.Paciente.ListarPaciente();

            lbListaPacientes.Items.Clear();
            // Mostrar todos los pacientes
            if (lista.Any())
            {
                lista.ForEach(paciente => lbListaPacientes.Items.Add(paciente.nombrePaciente.ToString()));
            }
            else
            {
                MessageBox.Show("No hay registros", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Limpiar()
        {
            listaPaciente();
            txtCorreo.Text = "";
            txtDireccion.Text = "";
            txtEdad.Text = "";
            txtIdentidad.Text = "";
            txtLugarNacimiento.Text = "";
            txtLugarResidencia.Text = "";
            txtNombre.Text = "";
            txtTelefonos.Text = "";
            cbEscolaridad.Text = "";
            cbEstadoCivil.Text = "";
            cbRaza.Text = "";
            cbReligion.Text = "";
            cbSexo.Text = "";
            dpFechaNaciemiento.Text = "";
            btnActualizarPaciente.IsEnabled = false;
            btnAgregarPaciente.IsEnabled = true;
            txtBuscarPacienteIdentidad.Text = "";


        }
        /// <summary>
        /// Metodo del buton para agreagar los pacientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarPaciente_Click(object sender, RoutedEventArgs e)
        {
            // Validar cada campo para evitar inserciones vacias
            if (txtNombre.Text =="")
            {
                MessageBox.Show("Ingresar el Nombre del paciente", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtIdentidad.Text == "")
            {
                MessageBox.Show("Ingresar la Identidad", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(txtEdad.Text == "")
            {
                MessageBox.Show("Ingresar la Edad", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(txtLugarNacimiento.Text == "")
            {
                MessageBox.Show("Ingresar el Lugar de Nacimiento", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtLugarResidencia.Text == "")
            {
                MessageBox.Show("Ingresar el Lugar de Residencia", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(txtCorreo.Text == "")
            {
                MessageBox.Show("Ingresar el Correo Electrinico","Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(txtDireccion.Text == "")
            {
                MessageBox.Show("Ingresar la Direccion", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(txtTelefonos.Text == "")
            {
                MessageBox.Show("Ingresar los Telfonos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(cbEscolaridad.Text == "")
            {
                MessageBox.Show("Selecciona la Escolaridad", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(cbEstadoCivil.Text == "")
            {
                MessageBox.Show("Selecciona el Estado Civil", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(cbRaza.Text == "")
            {
                MessageBox.Show("Selecciona la Raza", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(cbReligion.Text == "")
            {
                MessageBox.Show("Selecciona la Religion", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(cbSexo.Text == "")
            {
                MessageBox.Show("Seleciona el Sexo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(dpFechaNaciemiento.Text == "")
            {
                MessageBox.Show("Ingresa la Fecha de Nacimiento", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Si no hay campos vacios realizar la insercion  de los datos en la base
                // de datos.

                //Llamar a la clase
                Pacientes.Paciente agregarPaciente = new Pacientes.Paciente();
                agregarPaciente.nombrePaciente = txtNombre.Text;
                agregarPaciente.identidadPaciente = txtIdentidad.Text;
                agregarPaciente.edad = Convert.ToInt32(txtEdad.Text);
                agregarPaciente.correo = txtCorreo.Text;
                agregarPaciente.direccion = txtDireccion.Text;
                agregarPaciente.lugarNacimiento = txtLugarNacimiento.Text;
                agregarPaciente.lugarResidencia = txtLugarResidencia.Text;
                agregarPaciente.telefonos = txtTelefonos.Text;
                agregarPaciente.escolaridad = cbEscolaridad.Text.ToString();
                agregarPaciente.estadoCivil = cbEstadoCivil.Text.ToString();
                agregarPaciente.raza = cbRaza.Text.ToString();
                agregarPaciente.religion = cbReligion.Text.ToString();
                agregarPaciente.sexo = cbSexo.Text.ToString();
                agregarPaciente.fechaNacimiento = Convert.ToDateTime(dpFechaNaciemiento.Text);

                if (Pacientes.Paciente.InsertarPaciente(agregarPaciente))
                {
                    MessageBox.Show("Los Datos Han Sido Registrados", "Registro Guardado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Los Datos No Han Sido Registrados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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


        private void btnActualizarPaciente_Click(object sender, RoutedEventArgs e)
        {
            Pacientes.Paciente paciente = new Pacientes.Paciente();
            paciente.nombrePaciente = txtNombre.Text;
            paciente.identidadPaciente = txtIdentidad.Text;
            paciente.edad = Convert.ToInt32(txtEdad.Text);
            paciente.correo = txtCorreo.Text;
            paciente.direccion = txtDireccion.Text;
            paciente.lugarNacimiento = txtLugarNacimiento.Text;
            paciente.lugarResidencia = txtLugarResidencia.Text;
            paciente.telefonos = txtTelefonos.Text;
            paciente.escolaridad = cbEscolaridad.Text.ToString();
            paciente.estadoCivil = cbEstadoCivil.Text.ToString();
            paciente.raza = cbRaza.Text.ToString();
            paciente.religion = cbReligion.Text.ToString();
            paciente.sexo = cbSexo.Text.ToString();
            paciente.fechaNacimiento = Convert.ToDateTime(dpFechaNaciemiento.Text);
            if (Pacientes.Paciente.ActulizarPaciente(paciente))
            {
                MessageBox.Show("Los Datos Se Han Actuzalizado", "Actulizacion De Datos", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("Los Datos No Se Han Actuzalizado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }

        private void txtBuscarPacienteIdentidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            Pacientes.Paciente paciente = Pacientes.Paciente.ListarDatosPacieneIdentidad(txtBuscarPacienteIdentidad.Text);
            txtNombre.Text = paciente.nombrePaciente;
            txtIdentidad.Text = paciente.identidadPaciente;
            txtCorreo.Text = paciente.correo;
            txtDireccion.Text = paciente.direccion;
            txtEdad.Text = Convert.ToString(paciente.edad);
            txtLugarNacimiento.Text = paciente.lugarNacimiento;
            txtLugarResidencia.Text = paciente.lugarResidencia;
            txtTelefonos.Text = paciente.telefonos;
            cbEscolaridad.Text = paciente.escolaridad;
            cbEstadoCivil.Text = paciente.estadoCivil;
            cbRaza.Text = paciente.raza;
            cbReligion.Text = paciente.religion;
            cbSexo.Text = paciente.sexo;
            dpFechaNaciemiento.Text = Convert.ToString(paciente.fechaNacimiento);
            if (txtIdentidad.Text != "")
            {
                btnActualizarPaciente.IsEnabled = true;
                btnAgregarPaciente.IsEnabled = false;
            }
        }

        
    }
}
