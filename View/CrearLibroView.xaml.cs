using MahApps.Metro.Controls;
using SistemaLibreriaImagina.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SistemaLibreriaImagina.View
{
    /// <summary>
    /// Lógica de interacción para CrearLibroView.xaml
    /// </summary>
    public partial class CrearLibroView : MetroWindow
    {
        private CrearLibroViewModel viewModel;
        public CrearLibroView()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            // Crear una instancia del ModificarLibroViewModel
            viewModel = new CrearLibroViewModel();

            // Suscribirse al evento CerrarVentanaRequested del viewModel
            viewModel.CerrarVentanaRequested += ViewModel_CerrarVentanaRequested;

            // Asignar el viewModel como DataContext de la vista
            DataContext = viewModel;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidNumericInput(e.Text);
        }

        private bool IsValidNumericInput(string input)
        {
            return int.TryParse(input, out _) || decimal.TryParse(input, out _);
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