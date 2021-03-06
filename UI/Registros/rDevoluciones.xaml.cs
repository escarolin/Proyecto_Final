using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//Using agregados
using Proyecto_Final.BLL;
using Proyecto_Final.Entidades;

namespace Proyecto_Final.UI.Registros
{
    public partial class rDevoluciones : Window
    {
        private Devoluciones devoluciones = new Devoluciones();
        public rDevoluciones()
        {
            InitializeComponent();
            this.DataContext = devoluciones;
            //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 ComboBox EstudianteId ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
            ClienteIdComboBox.ItemsSource = ClientesBLL.GetClientes();
            ClienteIdComboBox.SelectedValuePath = "ClienteId";
            ClienteIdComboBox.DisplayMemberPath = "Nombre";
            //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 ComboBox LibroId ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
            ProductoIdComboBox.ItemsSource = ProductosBLL.GetProductos();
            ProductoIdComboBox.SelectedValuePath = "ProductoId";
            ProductoIdComboBox.DisplayMemberPath = "NombreP";
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Cargar ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = devoluciones;
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Limpiar ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
        private void Limpiar()
        {
            this.devoluciones = new Devoluciones();
            this.DataContext = devoluciones;
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Validar ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
        private bool Validar()
        {
            bool Validado = true;
            if (DevolucionesIdTextbox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacci�n Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Buscar ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Devoluciones encontrado = DevolucionesBLL.Buscar(devoluciones.DevolucionId);

            if (encontrado != null)
            {
                devoluciones = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Esta Devoluci�n no fue encontrado.\n\nAseg�rese que existe o cree una nueva.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                DevolucionesIdTextbox.SelectAll();
                DevolucionesIdTextbox.Focus();
            }
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Agregar Fila ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 Libro Id ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
            if (ProductoIdComboBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo (Producto Id) est� vac�o.\n\nPorfavor, Seleccione el Producto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                ProductoIdComboBox.IsDropDownOpen = true;
                return;
            }
            if (CantidadTextBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo (Cantidad) est� vacio.\n\nEscriba la cantidad de productos devueltos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadTextBox.Focus();
                return;
            }

            var filaDetalle = new DevolucionesDetalle
            {
                DevolucionId = this.devoluciones.DevolucionId,
                ProductoId = Convert.ToInt32(ProductoIdComboBox.SelectedValue.ToString()),
                //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Nombre en el ComboBox ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
                productos = (Productos)ProductoIdComboBox.SelectedItem,
                //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
                Cantidad = Convert.ToDouble(CantidadTextBox.Text.ToString())
            };
            //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Total]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
            double cant =  (double.Parse(CantidadTextBox.Text));

            devoluciones.TotalDevoluciones += cant;
            //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
            this.devoluciones.Detalle.Add(filaDetalle);
            Cargar();

            ProductoIdComboBox.SelectedIndex = -1;
            CantidadTextBox.Clear();
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Remover Fila ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double total = Convert.ToDouble(ProductoIdComboBox.Text);
                if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
                {
                    devoluciones.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                    devoluciones.TotalDevoluciones -= total;
                    Cargar();
                }
            }
            catch
            {
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Nuevo ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Guardar ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 Devoluci�n Id ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
                if (DevolucionesIdTextbox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Devoluci�n Id) est� vac�o.\n\nAsigne un Id al Pr駸tamo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DevolucionesIdTextbox.Text = "0";
                    DevolucionesIdTextbox.Focus();
                    DevolucionesIdTextbox.SelectAll();
                    return;
                }
                //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 Cliente Id ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
                if (ClienteIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Cliente Id) est� vac�o.\n\nSelecione un Cliente.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClienteIdComboBox.IsDropDownOpen = true;
                    return;
                }

                var paso = DevolucionesBLL.Guardar(this.devoluciones);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transacci�n Exitosa", "ﾉxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transacci�n Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Eliminar ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (DevolucionesBLL.Eliminar(int.Parse(DevolucionesIdTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "ﾉxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ TextChanged ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧[ Devolucion Id ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
        private void PrestamoIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (DevolucionesIdTextbox.Text.Trim() != string.Empty)
                {
                    int.Parse(DevolucionesIdTextbox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Pr駸tamo Id) no es un n�mero.\n\nPor favor, digite un n�mero.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                DevolucionesIdTextbox.Text = "0";
                DevolucionesIdTextbox.Focus();
                DevolucionesIdTextbox.SelectAll();
            }
        }

        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CantidadTextBox.Text.Trim() != string.Empty)
                {
                    double.Parse(CantidadTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Cantidad) no es un n�mero.\n\nPor favor, digite un n�mero de dias valido.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadTextBox.Text = "0";
                CantidadTextBox.Focus();
                CantidadTextBox.SelectAll();
            }
        }
    }
}