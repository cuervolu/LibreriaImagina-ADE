using MahApps.Metro.Controls.Dialogs;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SistemaLibreriaImagina.ViewModels
{
    internal class MainWindowViewModel : ObservableObject
    {
        public RelayCommand InicioViewCommand { get; set; }
        public RelayCommand InventarioViewCommand { get; set; }
        public RelayCommand CerrarSesionCommand { get; private set; }
        public RelayCommand RestockCommand { get; set; }
        public RelayCommand PedidoViewCommand { get; set; }
        public RelayCommand UserViewCommand { get; set; }
        public RelayCommand POSViewCommand { get; set; }

        public InicioViewModel InicioVM { get; set; }
        public InventarioViewModel InventarioVM { get; set; }
        public RestockViewModel RestockVM { get; set; }
        public PedidosViewModel PedidosVM { get; set; }
        public UsersViewModel UserVM { get; set; }
        public POSViewModel POSVM { get; set; }

        public string RolUsuario { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public string Token { get; set; }
        public long UserID { get; set; }

        private USUARIO usuario;

        public USUARIO Usuario
        {
            get { return usuario; }
            set { usuario = value; OnPropertyChanged(); }
        }

        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            try
            {
                // Mostrar el elemento de carga
                IsLoading = true;
                InicioVM = new InicioViewModel();
                RolUsuario = UsuarioGlobal.Instancia.Usuario.TIPO_USUARIO;
                CurrentView = InicioVM;
                InicioViewCommand = new RelayCommand(o =>
                {
                    InicioVM = new InicioViewModel();
                    CurrentView = InicioVM;
                });

                InventarioViewCommand = new RelayCommand(o =>
                {
                    InventarioVM = new InventarioViewModel();
                    CurrentView = InventarioVM;
                });

                PedidoViewCommand = new RelayCommand(o =>
                {
                    PedidosVM = new PedidosViewModel();
                    CurrentView = PedidosVM;
                });

                RestockCommand = new RelayCommand(o =>
                {
                    RestockVM = new RestockViewModel();
                    CurrentView = RestockVM;
                });

                UserViewCommand = new RelayCommand(o =>
                {
                    UserVM = new UsersViewModel();
                    CurrentView = UserVM;
                });

                POSViewCommand = new RelayCommand(o =>
                {
                    POSVM = new POSViewModel();
                    CurrentView = POSVM;
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
                            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                        }
                        catch (Exception ex)
                        {
                            // Manejar cualquier error que ocurra durante el cierre de sesión
                            await progressDialog.CloseAsync();
                            await mainWindow.ShowMessageAsync("Error", "Se produjo un error al cerrar la sesión: " + ex.Message);
                        }
                    }
                });

                // Ocultar el elemento de carga una vez que se complete la carga
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<USUARIO> GetUserInfo()
        {
            try
            {
                Usuario = await UsersService.GetCurrentUserInfo(UserID);
                return Usuario;
            }
            catch (Exception ex)
            {
                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                await mainWindow.ShowMessageAsync("Error", "Se produjo un error al cargar la información del usuario: " + ex.Message);
                return null;
            }
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