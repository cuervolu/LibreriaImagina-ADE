using MahApps.Metro.Controls.Dialogs;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using Slugify;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SistemaLibreriaImagina.ViewModels
{
    public class ModificarLibroViewModel : ObservableObject
    {
        private bool isLoading;
        private LIBRO libro;
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

        public LIBRO Libro
        {
            get { return libro; }
            set
            {
                libro = value;
                OnPropertyChanged();
            }
        }

        private string slug;

        public string Slug
        {
            get { return slug; }
            set
            {
                slug = value;
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
            }
        }

        public ModificarLibroViewModel(long id_libro)
        {
            _ = LoadCategoriesAsync();
            LoadBookAsync(id_libro);

            // Suscribir al evento PropertyChanged del ViewModel
            PropertyChanged += ModificarLibroViewModel_PropertyChanged;
        }

        private void ModificarLibroViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Libro.NOMBRE_LIBRO))
            {
                UpdateSlug();
            }
        }

        public async Task LoadBookAsync(long id_libro)
        {
            IsLoading = true;

            try
            {
                var libroResponse = await BookService.ObtenerLibroPorId(id_libro);

                if (libroResponse != null)
                {
                    Libro = (LIBRO)libroResponse;
                    UpdateSlug();
                    selectedCategoria = Libro.CATEGORIA;
                }
                else
                {
                    // Mostrar mensaje de error o tomar otra acción apropiada
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error o tomar otra acción apropiada
                await dialogCoordinator.ShowMessageAsync(this, "Error", $"No se pudo modificar el libro. Error: {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void UpdateSlug()
        {
            // Generar el nuevo slug basado en el nuevo valor del nombre del libro
            string nuevoSlug = GenerateSlug(Libro.NOMBRE_LIBRO);

            // Actualizar el valor del slug en el objeto libro
            slug = nuevoSlug;
            Libro.SLUG = nuevoSlug;
        }

        private string GenerateSlug(string text)
        {
            // Lógica para generar el slug a partir del título
            SlugHelper slugHelper = new SlugHelper();
            string slug = slugHelper.GenerateSlug(text);
            return slug;
        }

        public async Task LoadCategoriesAsync()
        {
            var categoriasList = await BookService.GetCategoryListAsync();

            Categorias = categoriasList != null ? new ObservableCollection<string>(categoriasList) : new ObservableCollection<string>();

            // Obtener la instancia del servicio de diálogos
            dialogCoordinator = DialogCoordinator.Instance;
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

        private async void CancelarCambios(object parameter)
        {

            // Mostrar mensaje de confirmación utilizando el servicio de diálogos
            var result = await dialogCoordinator.ShowMessageAsync(this, "Confirmación", "¿Desea dejar sin guardar los cambios?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative) CerrarVentana = true;
        }

        private async void GuardarCambios(object parameter)
        {
            // Mostrar mensaje de confirmación utilizando el servicio de diálogos
            var result = await dialogCoordinator.ShowMessageAsync(this, "Confirmación", "¿Desea guardar los cambios?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                // Llamada al servicio para modificar el libro
                BookService.ModificarLibro(Libro.ID_LIBRO, Libro.NOMBRE_LIBRO, Libro.DESCRIPCION, Libro.AUTOR, Libro.EDITORIAL,
                    Libro.PRECIO_UNITARIO, Libro.CANTIDAD_DISPONIBLE, Libro.PORTADA, Libro.FECHA_PUBLICACION,
                   selectedCategoria, Libro.ISBN, Libro.SLUG, Libro.THUMBNAIL, out string resultado);

                // Verificar el resultado y mostrar mensajes de éxito o fallo utilizando el servicio de diálogos
                if (resultado == "Libro modificado correctamente")
                {
                    // Mostrar mensaje de éxito utilizando el servicio de diálogos
                    await dialogCoordinator.ShowMessageAsync(this, "Éxito", "El libro se ha modificado correctamente.");

                    // Cerrar la ventana
                    CerrarVentana = true;
                }
                else
                {
                    // Mostrar mensaje de fallo utilizando el servicio de diálogos
                    await dialogCoordinator.ShowMessageAsync(this, "Error", $"No se pudo modificar el libro. Error: {resultado}");
                }
            }
        }
    }
}