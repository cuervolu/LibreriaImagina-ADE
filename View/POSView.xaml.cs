﻿using SistemaLibreriaImagina.ViewModels;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace SistemaLibreriaImagina.View
{
    /// <summary>
    /// Lógica de interacción para POSView.xaml
    /// </summary>
    public partial class POSView : UserControl
    {
        private POSViewModel viewModel;
        public POSView()
        {
            InitializeComponent();
            viewModel = new POSViewModel();
            DataContext = viewModel;
        }


        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            // Utiliza una expresión regular para permitir solo números enteros
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
