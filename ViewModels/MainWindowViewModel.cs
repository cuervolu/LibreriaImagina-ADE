using MahApps.Metro.Controls.Dialogs;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Services;
using SistemaLibreriaImagina.View;
using System;
using System.Linq;
using System.Windows;

namespace SistemaLibreriaImagina.ViewModels
{
    class MainWindowViewModel : ObservableObject
    {
        public RelayCommand InicioViewCommand { get; set; }
        public RelayCommand InventarioViewCommand { get; set; }
        public RelayCommand CerrarSesionCommand { get; private set; }
        public RelayCommand RestockCommand { get; set; }
        public RelayCommand PedidoViewCommand { get; set; }

        public InicioViewModel InicioVM { get; set; }
        public InventarioViewModel InventarioVM { get; set; }
        public RestockViewModel RestockVM { get; set; }
        public PedidosViewModel PedidosVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public string Token { get; set; }

        public MainWindowViewModel()
        {
            InicioVM = new InicioViewModel();
            InventarioVM = new InventarioViewModel();
            RestockVM = new RestockViewModel();
            PedidosVM = new PedidosViewModel();
            CurrentView = InicioVM;

            InicioViewCommand = new RelayCommand(o =>
            {
                CurrentView = InicioVM;
            });

            InventarioViewCommand = new RelayCommand(o =>
            {
                CurrentView = InventarioVM;
            });

            PedidoViewCommand = new RelayCommand(o =>
            {
                CurrentView = PedidosVM;
            });

            RestockCommand = new RelayCommand(o =>
            {
                CurrentView = RestockVM;
            });

            CerrarSesionCommand = new RelayCommand(async o =>
            {
                // Mostrar un diálogo de confirmación antes de cerrar sesión
                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                var result = await mainWindow?.ShowMessageAsync("Cerrar sesión", "¿Estás seguro de que deseas cerrar sesión?", MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    // Mostrar pantalla de carga
                    var progressDialog = await mainWindow.ShowProgressAsync("Cerrando sesión", "Cerrando sesión...");

                    try
                    {
                        // Llamar al método de cierre de sesión del AuthenticationService
                        await AuthenticationService.LogoutAsync(Token);

                        // Cerrar la pantalla de carga
                        await progressDialog.CloseAsync();

                        // Redirigir al inicio de sesión
                        LoginView loginView = new LoginView();
                        loginView.Show();
                        CloseMainWindow();
                    }
                    catch (Exception ex)
                    {
                        // Manejar cualquier error que ocurra durante el cierre de sesión
                        await progressDialog.CloseAsync();
                        await mainWindow.ShowMessageAsync("Error", "Se produjo un error al cerrar la sesión: " + ex.Message);
                    }
                }
            });
        }

        private void CloseMainWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow mainWindow)
                {
                    mainWindow.Close();
                    break;
                }
            }
        }
    }
}
