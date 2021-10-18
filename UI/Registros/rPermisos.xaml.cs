using RolesConPermisos2.BLL;
using RolesConPermisos2.Entidades;
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

namespace RolesConPermisos2.UI.Registros
{
    /// <summary>
    /// Lógica de interacción para rPermisos.xaml
    /// </summary>
    public partial class rPermisos : Window
    {
        private Permisos permiso = new Permisos();
        public rPermisos()
        {
            InitializeComponent();
            this.DataContext = permiso;
            PermisoIDTextBox.Text = "0";
        }

        private void Limpiar()
        {
            this.permiso = new Permisos();
            this.DataContext = permiso;

        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool Validar()
        {
            bool esValido = true;

            if (DescripcionTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Ingrese la descripcion", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = PermisosBLL.Guardar(permiso);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transaccione exitosa!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transaccion Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesBLL.Eliminar(Utilidades.ToInt(PermisoIDTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var permisos = PermisosBLL.Buscar(Utilidades.ToInt(PermisoIDTextBox.Text));

            if (permiso != null)
                this.permiso = permisos;
            else
                this.permiso = new Permisos();

            this.DataContext = this.permiso;
        }
        public void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = permiso;
        }
    }
}
