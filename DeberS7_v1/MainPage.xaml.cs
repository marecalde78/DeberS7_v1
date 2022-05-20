using DeberS7_v1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeberS7_v1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void btnRegistar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Alumno alum = new Alumno
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Edad = int.Parse(txtEdad.Text),

                };
                await App.SQLiteDB.SaveAlumnoAsync(alum);

                await DisplayAlert("Registro", "Informacion Guardada", "OK");
                LimpiarControles();
                llenarDatos();
           ///     var alumnoList = await App.SQLiteDB.GetAlumnosAsync();
           ///     if (alumnoList!=null)
           ///      {
           ///         lstAlumnos.ItemsSource = alumnoList;
           ///      }
            }
            else
            {
                await DisplayAlert("Warning", "Ingrese los Datos requeridos", "OK");
            }
        }


        public async void llenarDatos()
        {
                 var alumnoList = await App.SQLiteDB.GetAlumnosAsync();
                 if (alumnoList!=null)
                  {
                     lstAlumnos.ItemsSource = alumnoList;
                  }

        }





        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellido.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else { 
                respuesta =true;
            }
            return respuesta;

        }

        private async void btnActualiza_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAlumno.Text))
            {
                Alumno alumno = new Alumno()
                { 
                    IdAlumno = Convert.ToInt32(txtIdAlumno.Text),
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                                  
                
                };
                await App.SQLiteDB.SaveAlumnoAsync(alumno);
                await DisplayAlert("Registro", "Actializado exitosamente", "OK");
                LimpiarControles();
                txtIdAlumno.IsVisible = false;
                btnActualiza.IsVisible = false;
                btnRegistar.IsVisible = true;
                llenarDatos();

            }
        }

        private async void lstAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Alumno)e.SelectedItem;
            btnRegistar.IsVisible = false;
            txtIdAlumno.IsVisible = true;
            btnActualiza.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdAlumno.ToString()))
            {
                var alumno= await App.SQLiteDB.GetAlumnoByIdAsync(obj.IdAlumno);
                if (alumno!=null)
                {
                    txtIdAlumno.Text = alumno.IdAlumno.ToString();
                    txtNombre.Text = alumno.Nombre;
                    txtApellido.Text = alumno.Apellido;
                    txtEdad.Text = alumno.Edad.ToString();      



                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(Convert.ToInt32(txtIdAlumno.Text));
            if (alumno!=null)
            {
                await App.SQLiteDB.DeleteAlumnoAsync(alumno);
                await DisplayAlert("Registro", "Registro Eliminado", "OK");
                LimpiarControles();
                llenarDatos();
                txtIdAlumno.IsVisible = false;
                btnActualiza.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistar.IsVisible = true;

            }
        }

        public void LimpiarControles()
        {
            txtIdAlumno.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";


        }
    }
}
