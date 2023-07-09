using Notifications.Wpf;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using SistemaLibreriaImagina.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SistemaLibreriaImagina.ViewModels
{
    public class UsersViewModel : ObservableObject
    {
        private ObservableCollection<USUARIO> usuarios;

        public ObservableCollection<USUARIO> Usuarios
        {
            get { return usuarios; }
            set { usuarios = value; OnPropertyChanged(); }
        }
        private List<string> tipos;

        public List<string> Tipos
        {
            get { return tipos; }
            set { tipos = value; OnPropertyChanged(); }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand changeRolCommand;

        public RelayCommand ChangeRolCommand
        {
            get
            {
                if (changeRolCommand == null)
                {
                    changeRolCommand = new RelayCommand(ChangeRol);
                }
                return changeRolCommand;
            }
        }

        private RelayCommand createUserCommand;

        public RelayCommand CreateUserCommand
        {
            get
            {
                if (createUserCommand == null)
                {
                    createUserCommand = new RelayCommand(OpenCrearUsuario);
                }
                return createUserCommand;
            }
        }



        public UsersViewModel()
        {

            Tipos = new List<string>
            {
                "Admin",
                "Repartidor",
                "Técnico",
                "Cliente",
                "Encargado de Bodega"
            };
            LoadUsers();
        }

        public async void LoadUsers()
        {
            try
            {
                IsLoading = true;
                var response = await UsersService.GetUsersList();

                if (response != null)
                {
                    IsLoading = false;
                    Usuarios = new ObservableCollection<USUARIO>(response);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ocurrió un error al cargar los usuarios: " + ex.Message);
            }
        }

        private void ChangeRol(object parameter)
        {
            if (parameter is Tuple<USUARIO, string> tuple)
            {
                var usuario = tuple.Item1;
                var rol = tuple.Item2;

                try
                {
                    using (Entities dbContext = new Entities())
                    {
                        var usuarioActualizado = dbContext.USUARIOs.FirstOrDefault(u => u.ID == usuario.ID);
                        if (usuarioActualizado != null)
                        {
                            if (rol == usuarioActualizado.TIPO_USUARIO) return;
                            usuarioActualizado.TIPO_USUARIO = rol;

                            switch (rol)
                            {
                                case "Admin":
                                    usuarioActualizado.IS_STAFF = true;
                                    usuarioActualizado.IS_SUPERUSER = true;
                                    break;
                                case "Técnico":
                                    usuarioActualizado.IS_STAFF = true;
                                    usuarioActualizado.IS_SUPERUSER = false;
                                    break;
                                case "Repartidor":
                                    usuarioActualizado.IS_STAFF = true;
                                    usuarioActualizado.IS_SUPERUSER = false;
                                    break;
                                case "Cliente":
                                    usuarioActualizado.IS_STAFF = false;
                                    usuarioActualizado.IS_SUPERUSER = false;
                                    break;
                                case "Encargado de Bodega":
                                    usuarioActualizado.IS_STAFF = true;
                                    usuarioActualizado.IS_SUPERUSER = false;
                                    break;
                                case "Empleado":
                                    usuarioActualizado.IS_STAFF = true;
                                    usuarioActualizado.IS_SUPERUSER = false;
                                    break;
                                default:
                                    usuarioActualizado = null;
                                    break;
                            }

                            dbContext.SaveChanges();

                            var notificationManager = new NotificationManager();
                            notificationManager.Show(new NotificationContent
                            {
                                Title = "¡Bien hecho!",
                                Message = "Se hizo el cambio con éxito",
                                Type = NotificationType.Success
                            });

                            // Volver a cargar los usuarios
                            LoadUsers();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Ha ocurrido un error al cambiar el rol del usuario");
            }
        }

        private void OpenCrearUsuario(object parameter)
        {
            // Lógica para abrir la nueva ventana de creación de usuario
            CrearUsuarioView crearUsuarioView = new CrearUsuarioView();
            crearUsuarioView.ShowDialog();
            LoadUsers();
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
