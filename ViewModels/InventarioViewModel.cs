using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SistemaLibreriaImagina.ViewModels
{
    class InventarioViewModel : ObservableObject
    {
        private ObservableCollection<LIBRO> libros;
        private bool isLoading;

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

        public InventarioViewModel()
        {

            LoadLibros();
        }

        private void LoadLibros()
        {
            try
            {
                IsLoading = true; // Mostrar el ProgressRing

                var response = BookService.GetBookList();

                if (response != null)
                {
                    Libros = new ObservableCollection<LIBRO>(response);
                }
                else
                {
                    // Mostrar mensaje de error
                    ShowErrorMessage("No se pueden cargar los libros.");
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                ShowErrorMessage("Ocurrió un error al cargar los libros: " + ex.Message);
            }
            finally
            {
                IsLoading = false; // Ocultar el ProgressRing
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
