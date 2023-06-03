using MahApps.Metro.Controls;
using SistemaLibreriaImagina.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;

namespace SistemaLibreriaImagina.View
{
    /// <summary>
    /// Lógica de interacción para ModificarLibroView.xaml
    /// </summary>
    public partial class ModificarLibroView : MetroWindow
    {
        private ModificarLibroViewModel viewModel;

        public ModificarLibroView()
        { }

        public ModificarLibroView(long id_libro)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            // Crear una instancia del ModificarLibroViewModel
            viewModel = new ModificarLibroViewModel(id_libro);

            // Suscribirse al evento CerrarVentanaRequested del viewModel
            viewModel.CerrarVentanaRequested += ViewModel_CerrarVentanaRequested;

            // Asignar el viewModel como DataContext de la vista
            DataContext = viewModel;
        }

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