using MahApps.Metro.Controls;
using SistemaLibreriaImagina.Models;
using System.Windows;

namespace SistemaLibreriaImagina
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private USUARIO usuario;

        public MainWindow(USUARIO usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

    }
}
