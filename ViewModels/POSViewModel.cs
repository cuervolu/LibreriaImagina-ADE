using MahApps.Metro.Controls.Dialogs;
using Notifications.Wpf;
using Prism.Commands;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLibreriaImagina.ViewModels
{
    public class POSViewModel : ObservableObject
    {
        // Propiedades privadas
        private ObservableCollection<LIBRO> libros;
        private LIBRO selectedProduct;
        private string selectedProductName;
        private ObservableCollection<SelectedProductItem> selectedProducts;
        private bool isLibrosLoaded;
        private int cantidad;
        private decimal subtotal;
        private decimal precioTotal;
        private string rut;
        private ProgressDialogController _progressDialog;
        private IDialogCoordinator _dialogCoordinator;
        private bool isPaymentEnabled;

        // Constructor
        public POSViewModel()
        {
            _dialogCoordinator = new DialogCoordinator();
            LoadLibros();
            AddProductCommand = new DelegateCommand(AddProduct, CanAddProduct);
            CreatePaymentCommand = new DelegateCommand(Pagar, CanCreatePayment);
            selectedProducts = new ObservableCollection<SelectedProductItem>();
            Cantidad = 1; // Inicializar la cantidad en 1 por defecto
        }

        // Propiedades públicas
        public string Rut
        {
            get { return rut; }
            set
            {
                rut = value;
                OnPropertyChanged();
                CreatePaymentCommand.RaiseCanExecuteChanged();
            }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                if (value < 1) // Verifica si el valor es menor a 1
                {
                    cantidad = 1; // Establece el valor predeterminado en 1
                }
                else
                {
                    cantidad = value;
                }

                OnPropertyChanged();
                UpdatePrecioTotal();
            }
        }

        public decimal PrecioTotal
        {
            get { return precioTotal; }
            set
            {
                precioTotal = value;
                OnPropertyChanged();
            }
        }

        public decimal Subtotal
        {
            get { return subtotal; }
            set
            {
                subtotal = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LIBRO> Libros
        {
            get { return libros; }
            set
            {
                libros = value;
                OnPropertyChanged();
            }
        }

        public LIBRO SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
                AddProductCommand.RaiseCanExecuteChanged();
            }
        }

        public string SelectedProductName
        {
            get { return selectedProductName; }
            set
            {
                selectedProductName = value;
                OnPropertyChanged();
                UpdateSelectedProduct();
            }
        }

        public ObservableCollection<SelectedProductItem> SelectedProducts
        {
            get { return selectedProducts; }
            set
            {
                selectedProducts = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand AddProductCommand { get; private set; }
        public DelegateCommand CreatePaymentCommand { get; private set; }

        // Métodos privados
        private void LoadLibros()
        {
            try
            {
                Libros = BookService.GetBooks();
                isLibrosLoaded = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private bool CanAddProduct()
        {
            return isLibrosLoaded && SelectedProduct != null;
        }

        private void AddProduct()
        {
            if (SelectedProduct != null)
            {
                if (Cantidad > SelectedProduct.CANTIDAD_DISPONIBLE)
                {
                    // Mostrar mensaje de error si la cantidad es mayor a la disponible
                    ShowErrorMessage("La cantidad solicitada supera la disponibilidad del libro.");
                    return;
                }

                var selectedProductItem = new SelectedProductItem
                {
                    Libro = SelectedProduct,
                    Cantidad = Cantidad
                };

                SelectedProducts.Add(selectedProductItem);
                UpdatePrecioTotal();
                CreatePaymentCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanCreatePayment()
        {
            return isLibrosLoaded && SelectedProducts.Any() && !string.IsNullOrEmpty(Rut) && PrecioTotal > 0;
        }


        private async void Pagar()
        {
            try
            {
                // Realizar el pago utilizando el servicio de pedidos
                await ShowProgressDialog("Realizando pago...", async () =>
                {
                    await RealizarPago();
                });
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error durante el pago: {ex.Message}");
            }
        }

        private void UpdateSelectedProduct()
        {
            SelectedProduct = Libros.FirstOrDefault(libro => libro.NOMBRE_LIBRO == SelectedProductName);
            AddProductCommand.RaiseCanExecuteChanged();
        }

        private void UpdatePrecioTotal()
        {
            Subtotal = SelectedProducts.Sum(item => item.Libro.PRECIO_UNITARIO * item.Cantidad);
            PrecioTotal = Subtotal * 1.19m; // Agregar el IVA (19%)
            CreatePaymentCommand.RaiseCanExecuteChanged();
        }


        private async Task ShowProgressDialog(string message, Action loadingAction)
        {
            _progressDialog = await _dialogCoordinator.ShowProgressAsync(this, "Cargando", message, true);
            _progressDialog.SetIndeterminate();
            _progressDialog.Canceled += ProgressDialogCanceled;
            try
            {
                loadingAction.Invoke(); // Ejecuta la operación de carga
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que ocurra durante la carga
                ShowErrorMessage($"Error durante la carga: {ex.Message}");
            }

            await _progressDialog.CloseAsync();
        }


        private async Task RealizarPago()
        {
            await Console.Out.WriteLineAsync($"El rut es: {Rut}. El precio Total es {PrecioTotal}");

            try
            {
                if (string.IsNullOrEmpty(Rut))
                {
                    ShowErrorMessage("El campo 'Rut' no puede estar vacío.");
                    return;
                }

                if (Rut.Length != 12)
                {
                    ShowErrorMessage("El campo 'Rut' debe tener 9 caracteres.");
                    return;
                }

                string resultadoPago = await OrderService.CreatePayment(Rut, PrecioTotal);

                if (!string.IsNullOrEmpty(resultadoPago))
                {
                    var successDialogResult = await _dialogCoordinator.ShowMessageAsync(this, "Éxito", "Pago realizado exitosamente");

                    // Limpiar los productos seleccionados y reiniciar los valores
                    SelectedProducts.Clear();
                    Cantidad = 1;
                    Rut = string.Empty;
                    UpdatePrecioTotal();
                }
                else
                {
                    await _dialogCoordinator.ShowMessageAsync(this, "Error", $"No se pudo realizar el pago para RUT: {Rut}");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }


        private void ProgressDialogCanceled(object sender, EventArgs e)
        {
            _progressDialog.CloseAsync();
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
