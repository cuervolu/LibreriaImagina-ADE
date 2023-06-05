using Notifications.Wpf;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using WpfMessageBoxLibrary;

namespace SistemaLibreriaImagina.ViewModels
{
    internal class InventarioViewModel : ObservableObject
    {
        private ObservableCollection<LIBRO> libros;
        private bool isLoading;

        private int currentPage = 1;
        private int pageSize = 10; // Tamaño de página predeterminado

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

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                LoadLibros();
            }
        }

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                LoadLibros();
            }
        }

        private RelayCommand previousPageCommand;
        private RelayCommand nextPageCommand;

        public RelayCommand PreviousPageCommand
        {
            get
            {
                if (previousPageCommand == null)
                {
                    previousPageCommand = new RelayCommand(param => CurrentPage--, param => CurrentPage > 1);
                }
                return previousPageCommand;
            }
        }

        public RelayCommand NextPageCommand
        {
            get
            {
                if (nextPageCommand == null)
                {
                    nextPageCommand = new RelayCommand(param => CurrentPage++, param => Libros.Count >= PageSize);
                }
                return nextPageCommand;
            }
        }

        private RelayCommand deleteBookCommand;

        public RelayCommand DeleteBookCommand
        {
            get
            {
                if (deleteBookCommand == null)
                {
                    deleteBookCommand = new RelayCommand(DeleteBook);
                }
                return deleteBookCommand;
            }
        }

        private RelayCommand modifyBookCommand;

        public RelayCommand ModifyBookCommand
        {
            get
            {
                if (modifyBookCommand == null)
                {
                    modifyBookCommand = new RelayCommand(ModifyBook);
                }
                return modifyBookCommand;
            }
        }

        private void DeleteBook(object parameter)
        {
            if (parameter is long id_libro)
            {
                var result = WpfMessageBox.Show("Confirmar eliminación", "¿Estás seguro de que deseas eliminar el libro?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        isLoading = true;
                        BookService.EliminarLibro(id_libro);

                        LoadLibros();
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Error al eliminar el libro: " + ex.Message);
                    }
                    finally
                    {
                        isLoading = false;
                    }
                }
            }
        }



        private void ModifyBook(object parameter)
        {
            // Obtener el libro seleccionado desde el parámetro
            if (parameter is LIBRO libro)
            {
                var idLibro = libro.ID_LIBRO;

                // Crear una instancia de la nueva ventana
                var modificarLibroView = new ModificarLibroView(idLibro);

                // Bloquear la ventana anterior
                var mainWindow = Application.Current.MainWindow;
                mainWindow.IsEnabled = false;

                // Mostrar la nueva ventana como diálogo modal
                modificarLibroView.Owner = mainWindow;
                modificarLibroView.ShowDialog();

                // Desbloquear la ventana anterior cuando se cierre la nueva ventana
                mainWindow.IsEnabled = true;
            }
        }

        private bool isApplicationClosing = false;

        public InventarioViewModel()
        {
            // Registrar el evento Application.Exit para indicar que la aplicación se está cerrando
            Application.Current.Exit += Current_Exit;

            LoadLibros();
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            isApplicationClosing = true;
        }

        private void LoadLibros()
        {
            try
            {
                IsLoading = true; // Mostrar el ProgressRing

                var startIndex = (CurrentPage - 1) * PageSize;
                var response = BookService.GetBookList(startIndex, PageSize);

                if (response != null)
                {
                    Libros = new ObservableCollection<LIBRO>(response);
                }
                else if (!isApplicationClosing) // Verificar si la aplicación se está cerrando antes de mostrar el mensaje de error
                {
                    // Mostrar mensaje de error
                    ShowErrorMessage("No se pueden cargar los libros.");
                }

                OnPropertyChanged(nameof(CurrentPage)); // Actualizar la propiedad CurrentPage
            }
            catch (Exception ex)
            {
                if (!isApplicationClosing) // Verificar si la aplicación se está cerrando antes de mostrar el mensaje de error
                {
                    // Mostrar mensaje de error
                    ShowErrorMessage("Ocurrió un error al cargar los libros: " + ex.Message);
                }
            }
            finally
            {
                IsLoading = false; // Ocultar el ProgressRing
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
