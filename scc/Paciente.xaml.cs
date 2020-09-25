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
        }

        private void LimpiarText()
        {

        }
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
                }
                else
                {
                    MessageBox.Show("Los Datos No Han Sido Registrados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }



               
                
               
                
            }
        }

        
    }
}
