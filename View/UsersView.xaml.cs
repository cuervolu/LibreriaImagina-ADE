using MahApps.Metro.Controls;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.ViewModels;
using System;
using System.Windows;
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


        private void AdminSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"sender: {sender}");
            if (sender is ToggleSwitch toggleSwitch && toggleSwitch.DataContext is USUARIO usuario)
            {
                var viewModel = DataContext as UsersViewModel;
                bool isAdmin = toggleSwitch.IsOn;
                viewModel.AdminSwitchCommand.Execute(new Tuple<USUARIO, bool>(usuario, isAdmin));
            }
        }

        private void StaffSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch && toggleSwitch.DataContext is USUARIO usuario)
            {
                var viewModel = DataContext as UsersViewModel;
                bool isStaff = toggleSwitch.IsOn;
                viewModel.StaffSwitchCommand.Execute(new Tuple<USUARIO, bool>(usuario, isStaff));
            }
        }

        private void ActiveSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch && toggleSwitch.DataContext is USUARIO usuario)
            {
                var viewModel = DataContext as UsersViewModel;
                bool isActive = toggleSwitch.IsOn;
                viewModel.ActiveSwitchCommand.Execute(new Tuple<USUARIO, bool>(usuario, isActive));
            }
        }

    }
}
