using SistemaLibreriaImagina.Core;
using System;
using System.Windows;

namespace SistemaLibreriaImagina.ViewModels
{
    class InicioViewModel : ObservableObject
    {
        private int cantidadLibros;
        public int CantidadLibros
        {
            get { return cantidadLibros; }
            set
            {
                cantidadLibros = value;
                OnPropertyChanged();
            }
        }


        public InicioViewModel()
        {
            obtenerCantidadDeLibros();

        }
        private int obtenerCantidadDeLibros()
        {
            try
            {
                var response = BookService.GetAmountBooks();
                cantidadLibros = response;
                CantidadLibros = cantidadLibros; // Utilizar el setter de la propiedad para notificar el cambio
                return cantidadLibros;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"No se puede obtener la cantidad de libros: {ex}");
                cantidadLibros = 0;
                CantidadLibros = cantidadLibros; // Utilizar el setter de la propiedad para notificar el cambio
                return cantidadLibros;
            }
        }


        private void ShowErrorMessage(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }));
        }
    }
}
