using MahApps.Metro.Controls.Dialogs;
using Notifications.Wpf;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaLibreriaImagina.ViewModels
{
    public class ModificarPedidoViewModel : ObservableObject
    {
        private bool isLoading;

        private IDialogCoordinator dialogCoordinator;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<USUARIO> repartidores;

        public ObservableCollection<USUARIO> Repartidores
        {
            get { return repartidores; }
            set
            {
                repartidores = value;
                OnPropertyChanged();
            }
        }

        private USUARIO selectedRepartidor;

        public USUARIO SelectedRepartidor
        {
            get { return selectedRepartidor; }
            set
            {
                selectedRepartidor = value;
                OnPropertyChanged();
            }
        }


        private PEDIDO pedido;

        public PEDIDO Pedido
        {
            get { return pedido; }
            set { pedido = value; OnPropertyChanged(); }
        }

        public event EventHandler CerrarVentanaRequested;

        private bool cerrarVentana;

        public bool CerrarVentana
        {
            get { return cerrarVentana; }
            set
            {
                cerrarVentana = value;
                CerrarVentanaRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private RelayCommand cancelarCambiosCommand;
        public RelayCommand CancelarCambiosCommand
        {
            get
            {
                if (cancelarCambiosCommand == null)
                    cancelarCambiosCommand = new RelayCommand(CancelarCambios);

                return cancelarCambiosCommand;
            }
        }

        private RelayCommand guardarCambiosCommand;

        public RelayCommand GuardarCambiosCommand
        {
            get
            {
                if (guardarCambiosCommand == null)
                    guardarCambiosCommand = new RelayCommand(GuardarCambios);

                return guardarCambiosCommand;
            }
            set
            {
                guardarCambiosCommand = value;
                OnPropertyChanged(); // Asegúrate de que se notifique el cambio de la propiedad
            }
        }

        public ModificarPedidoViewModel(long id_pedido)
        {
            _ = LoadRepartidoresAsync();
            obtenerPedido(id_pedido);
        }


        private void obtenerPedido(long id_pedido)
        {
            try
            {
                pedido = OrderService.GetPEDIDO(id_pedido);
                if (pedido == null) Debug.WriteLine("Pedido es null");
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }
        }

        public async Task LoadRepartidoresAsync()
        {
            var repartidoresList = await UsersService.GetRepartidores();

            Repartidores = repartidoresList != null ? new ObservableCollection<USUARIO>(repartidoresList) : new ObservableCollection<USUARIO>();

            // Obtener la instancia del servicio de diálogos
            dialogCoordinator = DialogCoordinator.Instance;
        }

        private async void CancelarCambios(object parameter)
        {

            // Mostrar mensaje de confirmación utilizando el servicio de diálogos
            var result = await dialogCoordinator.ShowMessageAsync(this, "Confirmación", "¿Desea dejar sin guardar los cambios?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative) CerrarVentana = true;
        }

        private async void GuardarCambios(object parameter)
        {
            Debug.WriteLine(parameter.ToString());
            if (parameter is PEDIDO pedido)
            {
                // Mostrar mensaje de confirmación utilizando el servicio de diálogos
                var result = await dialogCoordinator.ShowMessageAsync(this, "Confirmación", "¿Desea guardar los cambios?",
                MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    CreateShipment();
                }
            }
            else
            {
                Debug.WriteLine("Ni una wea");
            }
        }


        private void CreateShipment()
        {
            try
            {
                using (var dbContext = new Entities())
                {
                    var id_pedido = pedido.ID_PEDIDO;
                    DateTime fecha_envio = DateTime.Now;
                    //Ver si el cliente posee una dirección
                    USUARIO cliente = dbContext.USUARIOs.FirstOrDefault(u => u.ID == pedido.CLIENTE_ID);
                    if (cliente.DIRECCION_ID != null)
                    {
                        long direccion_id = (long)cliente.DIRECCION_ID;

                        var response = OrderService.GenerateShipment(fecha_envio, direccion_id, id_pedido, selectedRepartidor.ID);

                        // Convertir la respuesta XML a una cadena con formato
                        XDocument xmlDoc = XDocument.Parse(response.OuterXml);
                        string formattedXml = xmlDoc.ToString();
                        var notificationManager = new NotificationManager();
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "¡Bien hecho!",
                            Message = "Se creo el envío con éxito",
                            Type = NotificationType.Success
                        });
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
