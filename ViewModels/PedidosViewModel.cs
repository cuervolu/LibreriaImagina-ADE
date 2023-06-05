using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace SistemaLibreriaImagina.ViewModels
{
    internal class PedidosViewModel : ObservableObject
    {
        private ObservableCollection<PEDIDO> pedidos;

        public ObservableCollection<PEDIDO> Pedidos
        {
            get { return pedidos; }
            set { pedidos = value; OnPropertyChanged(); }
        }

        private List<string> estados;

        public List<string> Estados
        {
            get { return estados; }
            set { estados = value; OnPropertyChanged(); }
        }


        private RelayCommand createShipmentCommand;

        public RelayCommand CreateShipmentCommand
        {
            get
            {
                if (createShipmentCommand == null)
                {
                    createShipmentCommand = new RelayCommand(CreateShipment);
                }
                return createShipmentCommand;
            }
        }

        private RelayCommand changeStateCommand;

        public RelayCommand ChangeStateCommand
        {
            get
            {
                if (changeStateCommand == null)
                {
                    changeStateCommand = new RelayCommand(ChangeState);
                }
                return changeStateCommand;
            }
        }

        public PedidosViewModel()
        {

            Estados = new List<string>
            {
                "En Validación",
                "En Preparación",
                "En Ruta",
                "Entregado",
                "Cancelado"
            };
            LoadOrders();
        }

        public void LoadOrders()
        {
            try
            {
                var response = OrderService.GetOrdersList();

                if (response != null)
                {
                    Pedidos = new ObservableCollection<PEDIDO>(response);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ocurrió un error al cargar los pedidos: " + ex.Message);
            }
        }

        private void CreateShipment(object parameter)
        {
            if (parameter is PEDIDO pedido)
            {
                try
                {
                    using (var dbContext = new Entities())
                    {
                        var id_pedido = pedido.ID_PEDIDO;
                        DateTime fecha_envio = DateTime.Now;
                        USUARIO cliente = dbContext.USUARIOs.FirstOrDefault(u => u.ID == pedido.CLIENTE_ID);
                        if (cliente.DIRECCION_ID != null)
                        {
                            long direccion_id = (long)cliente.DIRECCION_ID;

                            var response = OrderService.GenerateShipment(fecha_envio, direccion_id, id_pedido, cliente.ID);

                            // Convertir la respuesta XML a una cadena con formato
                            XDocument xmlDoc = XDocument.Parse(response.OuterXml);
                            string formattedXml = xmlDoc.ToString();
                            Console.WriteLine(formattedXml);
                            MessageBox.Show("Se creo el envío con éxito", "Éxito");
                        }
                        else
                        {
                            ShowErrorMessage("No se puede crear el envío, el usuario no posee una dirección asignada");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("No se puede crear el envío");
                return;
            }
        }


        private void ChangeState(object parameter)
        {
            if (parameter is Tuple<PEDIDO, string> tuple)
            {
                var pedido = tuple.Item1;
                var estado = tuple.Item2;

                try
                {
                    using (Entities dbContext = new Entities())
                    {
                        var pedidoActualizado = dbContext.PEDIDOes.FirstOrDefault(p => p.ID_PEDIDO == pedido.ID_PEDIDO);
                        if (pedidoActualizado != null)
                        {
                            // Actualizar el estado del pedido con el valor seleccionado
                            pedidoActualizado.ESTADO_PEDIDO = estado;

                            // Guardar los cambios en la base de datos
                            dbContext.SaveChanges();

                            MessageBox.Show("Cambios realizados exitosamente", "Exito");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Ha ocurrido un error al cambiar el estado del pedido");
            }
        }



        private void ShowErrorMessage(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }));
        }
    }
}