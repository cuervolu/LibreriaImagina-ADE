using MahApps.Metro.Controls;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.ViewModels;
using System.Windows;

namespace SistemaLibreriaImagina
{
    public partial class MainWindow : MetroWindow
    {
        private USUARIO usuario;
        private string token;

        public MainWindow(USUARIO usuario, string token)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.token = token;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Asignar el token al ViewModel del MainWindow
            var mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.Token = token;
            DataContext = mainWindowViewModel;
        }
    }
}
