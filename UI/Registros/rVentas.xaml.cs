﻿using System;
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
using Proyecto_Final.BLL;
using Proyecto_Final.Entidades;

namespace Proyecto_Final.UI.Registros
{
    public partial class rVentas : Window
    {
        private Ventas ventas = new Ventas();
        public rVentas()
        {
            InitializeComponent();
            this.DataContext = ventas;

            //—————————————————————————————————————[ ComboBox UsuarioId ]—————————————————————————————————————
            ClienteIdComboBox.ItemsSource = ClientesBLL.GetClientes();
            ClienteIdComboBox.SelectedValuePath = "ClienteId";
            ClienteIdComboBox.DisplayMemberPath = "Nombre";

            //————————————————————————————[ VALORES DEL ComboBox Productos]————————————————————————————————————
            ProductoIdComboBox.ItemsSource = ProductosBLL.GetProductos();
            ProductoIdComboBox.SelectedValuePath = "ProductoId";
            ProductoIdComboBox.DisplayMemberPath = "Descripcion";
        }

        //—————————————————————————————————————————————————————[ CARGAR ]—————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = ventas;
        }
        //—————————————————————————————————————————————————————[ LIMPIAR ]—————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.ventas = new Ventas();
            this.DataContext = ventas;
        }
        //—————————————————————————————————————————————————————[ Validar ]—————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;

            if (ClienteIdComboBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Campos vacios, Por favor llenarlo y continuar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return Validado;
        }
        //—————————————————————————————————————————————————————[ BUSCAR ]—————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Ventas encontrado = VentasBLL.Buscar(ventas.VentaId);

            if (encontrado != null)
            {
                ventas = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Este pedido no fue encontrado.\n\nAsegurese que existe o cree uno nuevo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                VentaIdTextbox.Clear();
                VentaIdTextbox.Focus();
            }
        }
        //—————————————————————————————————————————————————————[ AGREGAR FILA ]—————————————————————————————————————————————————————
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            Productos producto = (Productos)ProductoIdComboBox.SelectedItem;
            var filaDetalle = new VentasDetalle
            {
                VentaId = this.ventas.VentaId,
                ProductoId = Convert.ToInt32(ProductoIdComboBox.SelectedValue.ToString()),
                productos = (Productos)ProductoIdComboBox.SelectedItem,
                Cantidadv = Convert.ToInt32(CantidadvTextBox.Text)
            };

            //————————————————————————————————[ Calculos Total ]——————————————————————————————
            double subtotal = (producto.Precio) * (int.Parse(CantidadvTextBox.Text));
            double itbisTotal = ((producto.Itebis) / 100) * subtotal;
            double total1 = (subtotal + itbisTotal);

            ventas.Total = Convert.ToDouble(total1);
            //————————————————————————————————————————————————————————————————————————————————

            this.ventas.Detalle.Add(filaDetalle);
            Cargar();

            ProductoIdComboBox.SelectedIndex = -1;
            CantidadvTextBox.Clear();
            CantidadvTextBox.Focus();
        }
        //—————————————————————————————————————————————————————[ REMOVER FILA ]—————————————————————————————————————————————————————
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double total = Convert.ToDouble(TotalTextBox.Text);
                if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
                {
                    var detalle = (VentasDetalle)DetalleDataGrid.SelectedItem;
                    ventas.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);

                    ventas.Total -= total;
                    Cargar();
                }
            }
            catch
            {
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //—————————————————————————————————————————————————————[ NUEVO ]—————————————————————————————————————————————————————
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //—————————————————————————————————————————————————————[ GUARDAR ]—————————————————————————————————————————————————————
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                var paso = VentasBLL.Guardar(this.ventas);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("El Registro a sido guargado con  Exito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("NO se pudo guardar el Registro ", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //—————————————————————————————————————————————————————[ ELIMINAR ]—————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (VentasBLL.Eliminar(Utiidades.ToInt(VentaIdTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
