﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using SistemaLibreriaImagina.View;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SistemaLibreriaImagina.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        // Evento para notificar cambios en las propiedades
        public event PropertyChangedEventHandler PropertyChanged;

        public long user_id;

        private USUARIO usuarioData;

        public USUARIO UsuarioData
        {
            get { return usuarioData; }
            set { usuarioData = value; OnPropertyChanged(); }
        }


        private string usuario;
        private SecureString contrasena;
        private string mensaje;


        private string token;
        public string Token
        {
            get { return token; }
            set
            {
                if (token != value)
                {
                    token = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged();
                }
            }
        }


        // Propiedad Usuario
        public string Usuario
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

        // Propiedad Contrasena
        public SecureString Contrasena
        {
            get { return contrasena; }
            set
            {
                if (contrasena != value)
                {
                    contrasena = value;
                    OnPropertyChanged();
                }
            }
        }

        // Propiedad Mensaje
        public string Mensaje
        {
            get { return mensaje; }
            set
            {
                if (mensaje != value)
                {
                    mensaje = value;
                    OnPropertyChanged();
                }
            }
        }

        // Comando para el botón de ingreso
        public ICommand IngresarCommand { get; private set; }

        public LoginViewModel()
        {
            IngresarCommand = new RelayCommand(Ingresar, CanIngresar);
        }

        private bool CanIngresar(object parameter)
        {
            // Verificar si se cumplen las condiciones para habilitar el comando de ingreso
            return !string.IsNullOrEmpty(usuario) && contrasena != null;
        }

        private async void Ingresar(object parameter)
        {
            IsLoading = true;
            HttpResponseMessage response = await VerificarCredenciales();
            if (response != null)
            {
                try
                {
                    usuarioData = await GetUserInfo(user_id);
                    // Asignar la información del usuario a UsuarioGlobal
                    UsuarioGlobal.Instancia.Usuario = usuarioData;
                    IsLoading = false;
                    MainWindow mainWindow = new MainWindow(usuarioData, user_id, Token);
                    mainWindow.Show();
                    CloseLoginWindow();
                }
                catch (Exception ex)
                {
                    IsLoading = false;
                    MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
                    await metroWindow.ShowMessageAsync("Error", ex.Message);
                }
            }
            else
            {
                IsLoading = false;
                // Mostrar el mensaje de la respuesta HTTP utilizando MahApps.Metro
                MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
                await metroWindow.ShowMessageAsync("Error", Mensaje);

            }
        }


        private async Task<HttpResponseMessage> VerificarCredenciales()
        {
            try
            {
                // Convertir SecureString a string para realizar la comparación
                string contrasenaString = new System.Net.NetworkCredential(string.Empty, contrasena).Password;

                HttpResponseMessage response = await AuthenticationService.AuthenticateAsync(usuario, contrasenaString);

                if (response.IsSuccessStatusCode)
                {
                    // Obtener el contenido de la respuesta como una cadena JSON
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserializar la cadena JSON en un objeto dynamic utilizando Newtonsoft.Json
                    dynamic responseData = JsonConvert.DeserializeObject(jsonResponse);

                    // Obtener el token de la respuesta y asignarlo a la propiedad Token
                    Token = responseData.token;

                    // Obtener el objeto usuario de la respuesta y asignarlo a la propiedad user
                    user_id = JsonConvert.DeserializeObject(responseData.user.ToString());

                    return response;
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    Mensaje = "El usuario no tiene permisos para entrar al sistema";
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Mensaje = "Credenciales inválidas";
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    Mensaje = "Error interno del servidor: " + response.ReasonPhrase;
                }
                else
                {
                    Mensaje = "Error al verificar las credenciales";
                }

                return null;
            }
            catch (Exception ex)
            {
                // Manejar la excepción de manera adecuada (p. ej., mostrar un mensaje de error, registrar la excepción, etc.)
                Console.WriteLine("Error al verificar credenciales: " + ex.Message);
                Mensaje = "Error del sistema: " + ex.Message;
                return null;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Invocar el evento de cambio de propiedad cuando se actualiza una propiedad
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private async Task<USUARIO> GetUserInfo(long user_id)
        {
            try
            {
                UsuarioData = await UsersService.GetCurrentUserInfo(user_id);
                return UsuarioData;
            }
            catch (Exception ex)
            {
                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                await mainWindow.ShowMessageAsync("Error", "Se produjo un error al cargar la información del usuario: " + ex.Message);
                return null;
            }
        }


        private void CloseLoginWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LoginView loginView)
                {
                    loginView.Close();
                    break;
                }
            }
        }
    }
}

