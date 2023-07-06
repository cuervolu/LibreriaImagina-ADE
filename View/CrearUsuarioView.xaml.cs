using MahApps.Metro.Controls;
using SistemaLibreriaImagina.ViewModels;

namespace SistemaLibreriaImagina.View
{
    /// <summary>
    /// Lógica de interacción para CrearUsuarioView.xaml
    /// </summary>
    public partial class CrearUsuarioView : MetroWindow
    {
        private CrearUsuarioViewModel viewModel;
        public CrearUsuarioView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
