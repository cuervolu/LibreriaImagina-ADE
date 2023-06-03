using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLibreriaImagina.ViewModels
{
    internal class RestockViewModel : ObservableObject
    {
        private ObservableCollection<LIBRO> libros;
        private bool isLoading;
        public RelayCommand GetDataCommand { get; set; }

        private bool isMessageVisible;

        public bool IsMessageVisible
        {
            get { return isMessageVisible; }
            set
            {
                isMessageVisible = value;
                OnPropertyChanged();
            }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
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

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool isDataGridVisible;

        public bool IsDataGridVisible
        {
            get { return isDataGridVisible; }
            set
            {
                isDataGridVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> categorias;

        public ObservableCollection<string> Categorias
        {
            get { return categorias; }
            set
            {
                categorias = value;
                OnPropertyChanged();
            }
        }

        private string selectedCategoria;

        public string SelectedCategoria
        {
            get { return selectedCategoria; }
            set
            {
                selectedCategoria = value;
                OnPropertyChanged();
                GetDataCommand.RaiseCanExecuteChanged();
            }
        }

        public RestockViewModel()
        {
            _ = LoadCategoriesAsync();

            GetDataCommand = new RelayCommand(async o =>
            {
                IsLoading = true;
                var apiResponse = await BookService.GetLibrosFromAPI(selectedCategoria);

                if (apiResponse.Error != null)
                {
                    // Mostrar el mensaje de error
                    Libros = new ObservableCollection<LIBRO>();
                    IsDataGridVisible = false; // Ocultar el DataGrid
                    IsMessageVisible = true; // Mostrar el mensaje de error
                    Message = apiResponse.Error;
                }
                else
                {
                    if (apiResponse.Data != null && apiResponse.Data.Any())
                    {
                        Libros = new ObservableCollection<LIBRO>(apiResponse.Data);
                        IsDataGridVisible = true; // Mostrar el DataGrid cuando se cargen los datos
                        IsMessageVisible = false; // Ocultar el mensaje
                    }
                    else
                    {
                        // Mostrar el mensaje cuando no hay datos
                        Libros = new ObservableCollection<LIBRO>();
                        IsDataGridVisible = false; // Ocultar el DataGrid
                        IsMessageVisible = true; // Mostrar el mensaje
                        Message = "No se encontraron libros disponibles en la categoría seleccionada.";
                    }
                }

                IsLoading = false;
            }, o => !string.IsNullOrEmpty(SelectedCategoria));
        }

        public async Task LoadCategoriesAsync()
        {
            var categoriasList = await BookService.GetCategoryListAsync();

            Categorias = categoriasList != null ? new ObservableCollection<string>(categoriasList) : new ObservableCollection<string>();
        }
    }
}