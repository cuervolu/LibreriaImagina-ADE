using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.ViewModels;
using System;
using System.Windows.Controls;

namespace SistemaLibreriaImagina.View
{
    /// <summary>
    /// Lógica de interacción para UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedUsuario = comboBox.DataContext as USUARIO;
            var selectedRol = comboBox.SelectedItem as string;


            var viewModel = DataContext as UsersViewModel;
            viewModel.ChangeRolCommand.Execute(new Tuple<USUARIO, string>(selectedUsuario, selectedRol));
        }
    }
}
