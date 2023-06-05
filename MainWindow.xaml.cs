using MahApps.Metro.Controls;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.ViewModels;
using System.Windows;

namespace SistemaLibreriaImagina
{
    public partial class MainWindow : MetroWindow
    {
        private long usuario;
        private USUARIO user;
        private string token;

        public MainWindow(USUARIO user, long usuario_id, string token)
        {
            InitializeComponent();
            this.user = user;
            this.usuario = usuario_id;
            this.token = token;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Asignar el token al ViewModel del MainWindow
            var mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.Token = token;
            mainWindowViewModel.UserID = usuario_id;
            mainWindowViewModel.Usuario = user;
            DataContext = mainWindowViewModel;
        }
    }
}
