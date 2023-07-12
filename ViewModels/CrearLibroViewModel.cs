using MahApps.Metro.Controls.Dialogs;
using SistemaLibreriaImagina.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SistemaLibreriaImagina.ViewModels
{
    public class CrearLibroViewModel : ObservableObject
    {
        #region Atributos del Libro

        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set { SetProperty(ref titulo, value); }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { SetProperty(ref descripcion, value); }
        }

        private string autor;
        public string Autor
        {
            get { return autor; }
            set { SetProperty(ref autor, value); }
        }

        private decimal precioUnitario = 1000;
        public decimal PrecioUnitario
        {
            get { return precioUnitario; }
            set { SetProperty(ref precioUnitario, value); }
        }

        private string editorial;
        public string Editorial
        {
            get { return editorial; }
            set { SetProperty(ref editorial, value); }
        }

        private long cantidadDisponible = 1;
        public long CantidadDisponible
        {
            get { return cantidadDisponible; }
            set { SetProperty(ref cantidadDisponible, value); }
        }

        private DateTime fechaPublicacion = DateTime.Now;
        public DateTime FechaPublicacion
        {
            get { return fechaPublicacion; }
            set { SetProperty(ref fechaPublicacion, value); }
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


        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set
            {
                SetProperty(ref thumbnail, value);

                if (Uri.TryCreate(value, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    ThumbnailErrorMessage = null; // Limpia el mensaje de error
                }
                else
                {
                    ThumbnailErrorMessage = "La URL de la imagen no es válida."; // Establece el mensaje de error
                }
            }
        }

        private string portada;
        public string Portada
        {
            get { return portada; }
            set
            {
                SetProperty(ref portada, value);

                if (Uri.TryCreate(value, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    PortadaErrorMessage = null; // Limpia el mensaje de error
                }
                else
                {
                    PortadaErrorMessage = "La URL de la imagen no es válida."; // Establece el mensaje de error
                }
            }
        }


        private string isbn;
        public string ISBN
        {
            get { return isbn; }
            set { SetProperty(ref isbn, value); }
        }

        private string selectedCategoria;
        public string SelectedCategoria
        {
            get { return selectedCategoria; }
            set { SetProperty(ref selectedCategoria, value); }
        }

        #endregion


        #region Mensajes de error

        private string thumbnailErrorMessage;
        public string ThumbnailErrorMessage
        {
            get { return thumbnailErrorMessage; }
            set { SetProperty(ref thumbnailErrorMessage, value); }
        }

        private string portadaErrorMessage;
        public string PortadaErrorMessage
        {
            get { return portadaErrorMessage; }
            set { SetProperty(ref portadaErrorMessage, value); }
        }

        private string categoriaErrorMessage;
        public string CategoriaErrorMessage
        {
            get { return categoriaErrorMessage; }
            set { SetProperty(ref categoriaErrorMessage, value); }
        }

        private string fechaPublicacionErrorMessage;
        public string FechaPublicacionErrorMessage
        {
            get { return fechaPublicacionErrorMessage; }
            set { SetProperty(ref fechaPublicacionErrorMessage, value); }
        }

        private string precioUnitarioErrorMessage;
        public string PrecioUnitarioErrorMessage
        {
            get { return precioUnitarioErrorMessage; }
            set { SetProperty(ref precioUnitarioErrorMessage, value); }
        }


        private string tituloErrorMessage;
        public string TituloErrorMessage
        {
            get { return tituloErrorMessage; }
            set { SetProperty(ref tituloErrorMessage, value); }
        }

        private string editorialErrorMessage;
        public string EditorialErrorMessage
        {
            get { return editorialErrorMessage; }
            set { SetProperty(ref editorialErrorMessage, value); }
        }

        private string autorErrorMessage;
        public string AutorErrorMessage
        {
            get { return autorErrorMessage; }
            set { SetProperty(ref autorErrorMessage, value); }
        }

        private string descripcionErrorMessage;
        public string DescripcionErrorMessage
        {
            get { return descripcionErrorMessage; }
            set { SetProperty(ref descripcionErrorMessage, value); }
        }

        private string cantidadDisponibleErrorMessage;
        public string CantidadDisponibleErrorMessage
        {
            get { return cantidadDisponibleErrorMessage; }
            set { SetProperty(ref cantidadDisponibleErrorMessage, value); }
        }
        #endregion

        #region Otros Atributos
        private IDialogCoordinator dialogCoordinator;
        private readonly BookService bookService;
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        private ObservableCollection<string> categorias;
        public ObservableCollection<string> Categorias
        {
            get { return categorias; }
            set { SetProperty(ref categorias, value); }
        }

        private RelayCommand guardarCambiosCommand;

        public RelayCommand GuardarCambiosCommand
        {
            get
            {
                if (guardarCambiosCommand == null)
                    guardarCambiosCommand = new RelayCommand(CreateBook);

                return guardarCambiosCommand;
            }
            set
            {
                guardarCambiosCommand = value;
                OnPropertyChanged(); // Asegúrate de que se notifique el cambio de la propiedad
            }
        }

        #endregion

        #region Constructor

        public CrearLibroViewModel()
        {
            _ = LoadCategoriesAsync();
            bookService = new BookService();
        }

        #endregion

        #region Métodos

        public async Task LoadCategoriesAsync()
        {
            var categoriasList = await BookService.GetCategoryListAsync();

            Categorias = categoriasList != null ? new ObservableCollection<string>(categoriasList) : new ObservableCollection<string>();

            // Obtener la instancia del servicio de diálogos
            dialogCoordinator = DialogCoordinator.Instance;
        }

        public bool Validate()
        {
            bool isValid = true;

            string[] mandatoryFields = { nameof(Titulo), nameof(Descripcion), nameof(Autor), nameof(Editorial), nameof(Thumbnail), nameof(Portada), nameof(SelectedCategoria) };

            foreach (string fieldName in mandatoryFields)
            {
                if (string.IsNullOrWhiteSpace((string)this.GetType().GetProperty(fieldName).GetValue(this)))
                {
                    isValid = false;
                    OnPropertyChanged(fieldName + "ErrorMessage");
                }
            }

            if (CantidadDisponible <= 0)
            {
                isValid = false;
                OnPropertyChanged(nameof(CantidadDisponible));
            }

            if (ISBN.Length <= 0 || ISBN.Length > 20)
            {
                isValid = false;
                OnPropertyChanged(nameof(ISBN));
            }

            if (FechaPublicacion.Year <= 1900)
            {
                isValid = false;
                OnPropertyChanged(nameof(FechaPublicacion));
            }

            if (PrecioUnitario <= 0)
            {
                isValid = false;
                OnPropertyChanged(nameof(PrecioUnitario));
            }

            return isValid;
        }

        public async void CreateBook(object parameter)
        {
            // Mostrar mensaje de confirmación utilizando el servicio de diálogos
            var result = await dialogCoordinator.ShowMessageAsync(this, "Confirmación", "¿Desea guardar los cambios?",
                MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                try
                {
                    isLoading = true;
                    Dictionary<string, object> libroData = new Dictionary<string, object>();

                    libroData.Add("nombre_libro", Titulo);
                    libroData.Add("descripcion", Descripcion);
                    libroData.Add("autor", Autor);
                    libroData.Add("editorial", Editorial);
                    libroData.Add("precio_unitario", PrecioUnitario);
                    libroData.Add("cantidad_disponible", CantidadDisponible);
                    libroData.Add("thumbnail", Thumbnail);
                    libroData.Add("portada", Portada);
                    libroData.Add("categoria", SelectedCategoria);
                    libroData.Add("fecha_publicacion", FechaPublicacion.ToString("yyyy-MM-dd"));
                    libroData.Add("isbn", ISBN);
                    bool res = await bookService.CrearLibro(libroData);
                    isLoading = false;
                    if (res)
                    {
                        await dialogCoordinator.ShowMessageAsync(this, "Éxito", "El libro se ha creado correctamente.");
                        // Cerrar la ventana
                        CerrarVentana = true;
                    }
                    else
                    {
                        await dialogCoordinator.ShowMessageAsync(this, "Error", "Ha ocurrido un error al crear el libro.");
                    }
                }
                catch (Exception ex)
                {
                    await dialogCoordinator.ShowMessageAsync(this, "Error", $"No se pudo crear el libro. Error: {ex.Message}");
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.EndsWith("ErrorMessage"))
            {
                // Actualizar el mensaje de error correspondiente
                string errorPropertyName = propertyName.Substring(0, propertyName.Length - "ErrorMessage".Length);
                string errorMessage = null;

                switch (errorPropertyName)
                {
                    case nameof(Titulo):
                        errorMessage = string.IsNullOrWhiteSpace(Titulo) ? "El campo Título es obligatorio." : null;
                        break;
                    case nameof(Descripcion):
                        errorMessage = string.IsNullOrWhiteSpace(Descripcion) ? "El campo Descripción es obligatorio." : null;
                        break;
                    case nameof(Autor):
                        errorMessage = string.IsNullOrWhiteSpace(Autor) ? "El campo Autor es obligatorio." : null;
                        break;
                    case nameof(Editorial):
                        errorMessage = string.IsNullOrWhiteSpace(Editorial) ? "El campo Editorial es obligatorio." : null;
                        break;
                    case nameof(Thumbnail):
                        errorMessage = string.IsNullOrWhiteSpace(Thumbnail) ? "El campo Thumbnail es obligatorio." : null;
                        break;
                    case nameof(Portada):
                        errorMessage = string.IsNullOrWhiteSpace(Portada) ? "El campo Portada es obligatorio." : null;
                        break;
                    case nameof(SelectedCategoria):
                        errorMessage = string.IsNullOrWhiteSpace(SelectedCategoria) ? "Debe seleccionar una categoría." : null;
                        break;
                    case nameof(FechaPublicacion):
                        errorMessage = (FechaPublicacion == default(DateTime)) ? "El campo Fecha de Publicación es obligatorio." : null;
                        break;
                    case nameof(PrecioUnitario):
                        errorMessage = (PrecioUnitario <= 0) ? "El campo Precio Unitario debe ser mayor que cero." : null;
                        break;
                }

                // Actualizar el mensaje de error
                GetType().GetProperty(propertyName).SetValue(this, errorMessage);
            }
        }
        #endregion
    }
}
