using Notifications.Wpf;
using Prism.Commands;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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

        // Constructor
        public POSViewModel()
        {
            LoadLibros();
            AddProductCommand = new DelegateCommand(AddProduct, CanAddProduct);
            selectedProducts = new ObservableCollection<SelectedProductItem>();
            Cantidad = 1; // Inicializar la cantidad en 1 por defecto
        }

        // Propiedades públicas
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
