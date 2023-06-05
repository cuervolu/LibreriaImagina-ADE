using MahApps.Metro.Controls;
using SistemaLibreriaImagina.ViewModels;
using System.Windows;

namespace SistemaLibreriaImagina
{
    public partial class MainWindow : MetroWindow
    {
        private long usuario;
        private string token;

        public MainWindow(long usuario_id, string token)
        {
            InitializeComponent();
            this.usuario = usuario_id;
            this.token = token;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Asignar el token al ViewModel del MainWindow
            var mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.Token = token;
            DataContext = mainWindowViewModel;
        }
    }
}
