using Notifications.Wpf;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
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

        private USUARIO usuario;

        public USUARIO Usuario
        {
            get { return usuario; }
            set { usuario = value; OnPropertyChanged(); }
        }

        public InicioViewModel()
        {
            Usuario = UsuarioGlobal.Instancia.Usuario;
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
                var notificationManager = new NotificationManager();
                notificationManager.Show(new NotificationContent
                {
                    Title = "Error",
                    Message = message,
                    Type = NotificationType.Error
                });
            }));
        }
    }
}
