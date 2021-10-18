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
    /// Lógica de interacción para rRoles.xaml
    /// </summary>
    public partial class rRoles : Window
    {
        private Roles rol = new Roles();
        public rRoles()
        {
            InitializeComponent();
            this.DataContext = rol;
            RolIDTextBox.Text = "0";
            PermisosComboBox.ItemsSource = PermisosBLL.GetPermisos();
            PermisosComboBox.SelectedValuePath = "PermisoID";
            PermisosComboBox.DisplayMemberPath = "Descripcion";

        }

        private void Limpiar()
        {
            this.rol = new Roles();
            this.DataContext = rol;

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

            var paso = RolesBLL.Guardar(rol);

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
            if (RolesBLL.Eliminar(Utilidades.ToInt(RolIDTextBox.Text)))
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
            var roles = RolesBLL.Buscar(Utilidades.ToInt(RolIDTextBox.Text));

            if (rol != null)
                this.rol = roles;
            else
                this.rol = new Roles();

            this.DataContext = this.rol;
        }

        public void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = rol;
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {

            rol.RolesDetalle.Add(new RolesDetalles(rol.RolID, (int)PermisosComboBox.SelectedValue, PermisosComboBox.Text.ToString(),  Activo.IsEnabled));
            Actualizar();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
