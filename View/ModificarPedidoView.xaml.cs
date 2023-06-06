using MahApps.Metro.Controls;
using SistemaLibreriaImagina.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;

namespace SistemaLibreriaImagina.View
{
    /// <summary>
    /// Lógica de interacción para ModificarPedidoView.xaml
    /// </summary>
    public partial class ModificarPedidoView : MetroWindow
    {
        private ModificarPedidoViewModel viewModel;

        public ModificarPedidoView(long id_pedido)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // Crear una instancia del ModificarLibroViewModel
            viewModel = new ModificarPedidoViewModel(id_pedido);

            // Asignar el viewModel como DataContext de la vista
            DataContext = viewModel;
            InitializeComponent();

            // Suscribirse al evento CerrarVentanaRequested del viewModel
            viewModel.CerrarVentanaRequested += ViewModel_CerrarVentanaRequested;
        }

        public ModificarPedidoView()
        { }


        private void ViewModel_CerrarVentanaRequested(object sender, EventArgs e)
        {
            // Cerrar la ventana
            Dispatcher.Invoke(() => Close());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // Verificar si se debe cancelar el cierre de la ventana
            if (!viewModel.CerrarVentana)
            {
                e.Cancel = true;
            }
        }
    }
}
