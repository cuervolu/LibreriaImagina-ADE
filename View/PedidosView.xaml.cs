using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.ViewModels;
using System;
using System.Windows.Controls;

namespace SistemaLibreriaImagina.View
{
    /// <summary>
    /// Lógica de interacción para PedidosView.xaml
    /// </summary>
    public partial class PedidosView : UserControl
    {
        public PedidosView()
        {
            InitializeComponent();
        }

        private void PedidosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedPedido = comboBox.DataContext as PEDIDO;
            var selectedState = comboBox.SelectedItem as string;

            var viewModel = DataContext as PedidosViewModel;
            viewModel.ChangeStateCommand.Execute(new Tuple<PEDIDO, string>(selectedPedido, selectedState));
        }


    }
}
