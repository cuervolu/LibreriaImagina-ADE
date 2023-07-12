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
    /// <summary>
    /// ViewModel para la vista de inventario, que muestra una lista de libros y permite realizar acciones como eliminar, modificar y crear libros.
    /// </summary>

    internal class InventarioViewModel : ObservableObject
    {
        #region Propiedades

        private ObservableCollection<LIBRO> libros;
        private bool isLoading;
        private int currentPage = 1;
        private int pageSize = 10; // Tamaño de página predeterminado


        public ObservableCollection<LIBRO> Libros
        {
            get { return libros; }
            set
            {
                if (libros != value)
                {
                    libros = value;
                    OnPropertyChanged(nameof(Libros));
                }
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }


        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                    LoadLibros();
                }
            }
        }

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (pageSize != value)
                {
                    pageSize = value;
                    OnPropertyChanged(nameof(PageSize));
                    LoadLibros();
                }
            }
        }

        #endregion

        #region Comandos

        private RelayCommand previousPageCommand;
        private RelayCommand nextPageCommand;
        private RelayCommand deleteBookCommand;
        private RelayCommand modifyBookCommand;
        private RelayCommand crearLibroCommand;

        public RelayCommand PreviousPageCommand
        {
            get
            {
                if (previousPageCommand == null)
                {
                    previousPageCommand = new RelayCommand(PreviousPage, CanExecutePreviousPage);
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
                    nextPageCommand = new RelayCommand(NextPage, CanExecuteNextPage);
                }
                return nextPageCommand;
            }
        }

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

        public RelayCommand CrearLibroCommand
        {
            get
            {
                if (crearLibroCommand == null)
                {
                    crearLibroCommand = new RelayCommand(CreateBook);
                }
                return crearLibroCommand;
            }
        }

        #endregion

        #region Constructor y eventos

        private bool isApplicationClosing = false;

        public InventarioViewModel()
        {
            Application.Current.Exit += Current_Exit;
            LoadLibros();
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            isApplicationClosing = true;
        }

        #endregion

        #region Métodos privados

        private void DeleteBook(object parameter)
        {
            if (parameter is long id_libro)
            {
                var result = WpfMessageBox.Show("Confirmar eliminación", "¿Estás seguro de que deseas eliminar el libro?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        IsLoading = true;
                        BookService.EliminarLibro(id_libro);
                        LoadLibros();
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Error al eliminar el libro: " + ex.Message);
                    }
                    finally
                    {
                        IsLoading = false;
                    }
                }
            }
        }

        private void CreateBook(object parameter)
        {
            var crearLibroView = new CrearLibroView();
            crearLibroView.ShowDialog();
            LoadLibros();
        }

        private void ModifyBook(object parameter)
        {
            if (parameter is LIBRO libro)
            {
                var idLibro = libro.ID_LIBRO;
                var modificarLibroView = new ModificarLibroView(idLibro);
                var mainWindow = Application.Current.MainWindow;
                mainWindow.IsEnabled = false;
                modificarLibroView.Owner = mainWindow;
                modificarLibroView.ShowDialog();
                mainWindow.IsEnabled = true;
                LoadLibros();
            }
        }

        private bool CanExecutePreviousPage(object parameter)
        {
            return CurrentPage > 1;
        }

        private bool CanExecuteNextPage(object parameter)
        {
            return Libros.Count >= PageSize;
        }

        private void PreviousPage(object parameter)
        {
            CurrentPage--;
        }

        private void NextPage(object parameter)
        {
            CurrentPage++;
        }

        private void LoadLibros()
        {
            try
            {
                IsLoading = true;
                var startIndex = (CurrentPage - 1) * PageSize;
                var response = BookService.GetBookList(startIndex, PageSize);

                if (response != null)
                {
                    Libros = new ObservableCollection<LIBRO>(response);
                }
                else if (!isApplicationClosing)
                {
                    ShowErrorMessage("No se pueden cargar los libros.");
                }

                OnPropertyChanged(nameof(CurrentPage));
            }
            catch (Exception ex)
            {
                if (!isApplicationClosing)
                {
                    ShowErrorMessage("Ocurrió un error al cargar los libros: " + ex.Message);
                }
            }
            finally
            {
                IsLoading = false;
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

        #endregion
    }
}
