using MahApps.Metro.Controls;
using SistemaLibreriaImagina.ViewModels;
using System.Windows;
using System.Windows.Controls;
namespace SistemaLibreriaImagina.View
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        public LoginView()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void txtContrasena_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            ((LoginViewModel)DataContext).Contrasena = passwordBox.SecurePassword;
        }
    }
}
