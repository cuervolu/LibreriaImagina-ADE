using Notifications.Wpf;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using SistemaLibreriaImagina.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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


        private RelayCommand editOrderCommand;

        public RelayCommand EditOrderCommand
        {
            get
            {
                if (editOrderCommand == null)
                {
                    editOrderCommand = new RelayCommand(EditOrder);
                }
                return editOrderCommand;
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


        private void EditOrder(object parameter)
        {
            if (parameter is PEDIDO pedido)
            {

                var idPedido = pedido.ID_PEDIDO;

                // Crear una instancia de la nueva ventana
                var modificarPedidoView = new ModificarPedidoView(idPedido);

                // Bloquear la ventana anterior
                var mainWindow = Application.Current.MainWindow;
                mainWindow.IsEnabled = false;

                // Mostrar la nueva ventana como diálogo modal
                modificarPedidoView.Owner = mainWindow;
                modificarPedidoView.ShowDialog();
                LoadOrders();

                // Desbloquear la ventana anterior cuando se cierre la nueva ventana
                mainWindow.IsEnabled = true;
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
                            if (pedidoActualizado.ESTADO_PEDIDO == estado) return;
                            // Actualizar el estado del pedido con el valor seleccionado
                            pedidoActualizado.ESTADO_PEDIDO = estado;

                            // Guardar los cambios en la base de datos
                            dbContext.SaveChanges();
                            var notificationManager = new NotificationManager();
                            notificationManager.Show(new NotificationContent
                            {
                                Title = "¡Bien hecho!",
                                Message = "Cambios realizados exitosamente",
                                Type = NotificationType.Success
                            });

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
            var notificationManager = new NotificationManager();

            notificationManager.Show(new NotificationContent
            {
                Title = "Error",
                Message = message,
                Type = NotificationType.Error
            });
        }
    }
}