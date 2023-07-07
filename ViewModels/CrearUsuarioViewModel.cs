using MahApps.Metro.Controls.Dialogs;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SistemaLibreriaImagina.ViewModels
{
    internal class CrearUsuarioViewModel : ObservableObject
    {
        private IDialogCoordinator dialogCoordinator;
        private USUARIO usuario;
        private readonly AuthenticationService authenticationService;

        public USUARIO Usuario
        {
            get { return usuario; }
            set
            {
                if (usuario != value)
                {
                    usuario = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand crearUsuarioCommand;

        public RelayCommand CrearUsuarioCommand
        {
            get
            {
                if (crearUsuarioCommand == null)
                    crearUsuarioCommand = new RelayCommand(CrearUsuario);

                return crearUsuarioCommand;
            }
        }

        public CrearUsuarioViewModel()
        {
            this.authenticationService = new AuthenticationService();
            this.dialogCoordinator = DialogCoordinator.Instance;
            SelectedTipoUsuario = new ComboBoxItem { Content = "Seleccione rol...", IsSelected = true };
        }

        private bool isFormValid;

        public bool IsFormValid
        {
            get { return isFormValid; }
            set
            {
                isFormValid = value;
                OnPropertyChanged();
            }
        }

        private bool isTipoUsuarioInvalid;

        public bool IsTipoUsuarioInvalid
        {
            get { return isTipoUsuarioInvalid; }
            set
            {
                isTipoUsuarioInvalid = value;
                OnPropertyChanged();
            }
        }

        private ComboBoxItem selectedTipoUsuario;

        public ComboBoxItem SelectedTipoUsuario
        {
            get { return selectedTipoUsuario; }
            set
            {
                selectedTipoUsuario = value;
                if (selectedTipoUsuario?.Content.ToString() == "Seleccione rol...")
                {
                    IsTipoUsuarioInvalid = true;
                    IsFormValid = false;
                }
                else
                {
                    IsTipoUsuarioInvalid = false;
                }

                OnPropertyChanged(nameof(SelectedTipoUsuario));
            }
        }

        private string rut;

        public string Rut
        {
            get { return rut; }
            set
            {
                rut = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        private string rutErrorMessage;

        public string RutErrorMessage
        {
            get { return rutErrorMessage; }
            set
            {
                rutErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        private string nombreErrorMessage;

        public string NombreErrorMessage
        {
            get { return nombreErrorMessage; }
            set
            {
                nombreErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private string usuarioErrorMessage;

        public string UsuarioErrorMessage
        {
            get { return usuarioErrorMessage; }
            set
            {
                usuarioErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set
            {
                apellido = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        private string apellidoErrorMessage;

        public string ApellidoErrorMessage
        {
            get { return apellidoErrorMessage; }
            set
            {
                apellidoErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private string correo;

        public string Correo
        {
            get { return correo; }
            set
            {
                correo = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        private string correoErrorMessage;

        public string CorreoErrorMessage
        {
            get { return correoErrorMessage; }
            set
            {
                correoErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private string telefono;

        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        private string telefonoErrorMessage;

        public string TelefonoErrorMessage
        {
            get { return telefonoErrorMessage; }
            set
            {
                telefonoErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private void ValidateField(string value, string propertyName, Dictionary<string, string> fieldErrors)
        {
            if (string.IsNullOrEmpty(value))
            {
                fieldErrors[propertyName] = $"El {propertyName.ToLower()} es requerido";
            }
            else if (value.Length > 50)
            {
                fieldErrors[propertyName] = $"El {propertyName.ToLower()} debe tener como máximo 50 caracteres";
            }
            else if (propertyName == nameof(Correo) && !ValidateEmail(value))
            {
                fieldErrors[propertyName] = "Correo electrónico inválido";
            }
            else if (propertyName == nameof(Telefono) && !ValidatePhoneNumber(value))
            {
                fieldErrors[propertyName] = "Teléfono inválido";
            }
        }

        private void ValidateForm()
        {
            Dictionary<string, string> fieldErrors = new Dictionary<string, string>();

            ValidateField(Apellido, nameof(Apellido), fieldErrors);
            ValidateField(Nombre, nameof(Nombre), fieldErrors);
            ValidateField(Correo, nameof(Correo), fieldErrors);
            ValidateField(Telefono, nameof(Telefono), fieldErrors);
            ValidateField(Username, nameof(Username), fieldErrors);

            ValidateRut(Rut, fieldErrors);

            IsTipoUsuarioInvalid = SelectedTipoUsuario?.Content.ToString() == "Seleccione rol...";

            // Actualizar los mensajes de error y la validez de los campos
            ApellidoErrorMessage = GetErrorMessage(fieldErrors, nameof(Apellido));
            NombreErrorMessage = GetErrorMessage(fieldErrors, nameof(Nombre));
            CorreoErrorMessage = GetErrorMessage(fieldErrors, nameof(Correo));
            TelefonoErrorMessage = GetErrorMessage(fieldErrors, nameof(Telefono));
            UsuarioErrorMessage = GetErrorMessage(fieldErrors, nameof(Username));
            RutErrorMessage = GetErrorMessage(fieldErrors, "Rut");

            // Verificar si hay mensajes de error
            bool isValid = fieldErrors.Count == 0;

            // Actualizar la propiedad IsFormValid
            IsFormValid = isValid;
        }

        private string GetErrorMessage(Dictionary<string, string> fieldErrors, string propertyName)
        {
            if (fieldErrors.ContainsKey(propertyName))
            {
                return fieldErrors[propertyName];
            }
            return null;
        }

        private void ValidateRut(string value, Dictionary<string, string> fieldErrors)
        {
            if (string.IsNullOrEmpty(value))
            {
                fieldErrors["Rut"] = "El RUT es requerido";
            }
            else if (value.Length < 12)
            {
                fieldErrors["Rut"] = "El RUT debe tener 12 caracteres";
            }
        }

        private bool ValidateEmail(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return Regex.IsMatch(value, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        }

        private bool ValidatePhoneNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return Regex.IsMatch(value, @"^\d{9}$");
        }

        public async void CrearUsuario(object parameter)
        {
            if (IsFormValid)
            {
                // Mostrar mensaje de confirmación utilizando el servicio de diálogos
                var result = await dialogCoordinator.ShowMessageAsync(this, "Confirmación", "¿Desea guardar los cambios?",
                    MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    try
                    {
                        await authenticationService.CreateUser(Rut, Nombre, Apellido, Username, Correo, Telefono, SelectedTipoUsuario?.Content.ToString());

                        // Mostrar mensaje de éxito utilizando el servicio de diálogos
                        var successDialogResult = await dialogCoordinator.ShowMessageAsync(this, "Éxito", "Usuario creado exitosamente");

                        // Obtener la referencia a la ventana actual
                        var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                        // Cerrar la ventana si se hizo clic en "Aceptar" en el mensaje de éxito
                        if (successDialogResult == MessageDialogResult.Affirmative && window != null)
                        {
                            window.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Mostrar mensaje de fallo utilizando el servicio de diálogos
                        await dialogCoordinator.ShowMessageAsync(this, "Error", $"{ex.Message}");
                    }
                }
            }
            else
            {
                // Mostrar mensaje de error utilizando el servicio de diálogos
                string errorMessage = GetFormErrorMessage();
                await dialogCoordinator.ShowMessageAsync(this, "Error", errorMessage);
            }
        }

        private string GetFormErrorMessage()
        {
            string errorMessage = "Por favor, complete los siguientes campos:\n";

            if (string.IsNullOrEmpty(Username))
                errorMessage += "- Nombre de usuario\n";

            if (string.IsNullOrEmpty(Apellido))
                errorMessage += "- Apellido\n";

            if (string.IsNullOrEmpty(Nombre))
                errorMessage += "- Nombre\n";

            if (string.IsNullOrEmpty(Correo))
                errorMessage += "- Correo electrónico\n";

            if (string.IsNullOrEmpty(Telefono))
                errorMessage += "- Teléfono\n";

            if (SelectedTipoUsuario?.Content.ToString() == "Seleccione rol...")
                errorMessage += "- Tipo de usuario\n";

            if (string.IsNullOrEmpty(Rut))
                errorMessage += "- RUT\n";

            return errorMessage;
        }
    }
}