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
        #region Properties

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
            set { isLoading = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        private RelayCommand adminSwitchCommand;
        public RelayCommand AdminSwitchCommand => adminSwitchCommand ?? (adminSwitchCommand = new RelayCommand(AdminSwitchExecute));

        private RelayCommand staffSwitchCommand;
        public RelayCommand StaffSwitchCommand => staffSwitchCommand ?? (staffSwitchCommand = new RelayCommand(StaffSwitchExecute));

        private RelayCommand activeSwitchCommand;
        public RelayCommand ActiveSwitchCommand => activeSwitchCommand ?? (activeSwitchCommand = new RelayCommand(ActiveSwitchExecute));

        private RelayCommand changeRolCommand;
        public RelayCommand ChangeRolCommand => changeRolCommand ?? (changeRolCommand = new RelayCommand(ChangeRol));

        private RelayCommand createUserCommand;
        public RelayCommand CreateUserCommand => createUserCommand ?? (createUserCommand = new RelayCommand(OpenCrearUsuario));

        #endregion

        #region Constructor

        public UsersViewModel()
        {
            Tipos = new List<string>
            {
                "Admin",
                "Repartidor",
                "Técnico",
                "Cliente",
                "Empleado",
                "Encargado de Bodega"
            };
            LoadUsers();
        }

        #endregion

        #region Methods

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
                                // Casos de cambio de rol
                                // ...

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

        private void AdminSwitchExecute(object parameter)
        {
            if (parameter is Tuple<USUARIO, bool> tuple && tuple.Item1 is USUARIO usuario && tuple.Item2 is bool isAdmin)
            {
                try
                {
                    bool res = UsersService.UpdateAdminStatus(usuario.ID, isAdmin);
                    if (res)
                    {
                        var notificationManager = new NotificationManager();
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "¡Bien hecho!",
                            Message = "Se hizo el cambio con éxito",
                            Type = NotificationType.Success
                        });

                        // Actualizar solo el usuario modificado en lugar de volver a cargar todos los usuarios
                        int index = Usuarios.IndexOf(usuario);
                        if (index != -1)
                        {
                            Usuarios[index] = usuario;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
        }

        private void StaffSwitchExecute(object parameter)
        {
            if (parameter is Tuple<USUARIO, bool> tuple && tuple.Item1 is USUARIO usuario && tuple.Item2 is bool isStaff)
            {
                try
                {
                    bool res = UsersService.UpdateStaffStatus(usuario.ID, isStaff);
                    if (res)
                    {
                        var notificationManager = new NotificationManager();
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "¡Bien hecho!",
                            Message = "Se hizo el cambio con éxito",
                            Type = NotificationType.Success
                        });

                        // Actualizar solo el usuario modificado en lugar de volver a cargar todos los usuarios
                        int index = Usuarios.IndexOf(usuario);
                        if (index != -1)
                        {
                            Usuarios[index] = usuario;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
        }

        private void ActiveSwitchExecute(object parameter)
        {
            if (parameter is Tuple<USUARIO, bool> tuple && tuple.Item1 is USUARIO usuario && tuple.Item2 is bool isActive)
            {
                try
                {
                    bool res = UsersService.UpdateActiveStatus(usuario.ID, isActive);
                    if (res)
                    {
                        var notificationManager = new NotificationManager();
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "¡Bien hecho!",
                            Message = "Se hizo el cambio con éxito",
                            Type = NotificationType.Success
                        });

                        // Actualizar solo el usuario modificado en lugar de volver a cargar todos los usuarios
                        int index = Usuarios.IndexOf(usuario);
                        if (index != -1)
                        {
                            Usuarios[index] = usuario;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
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
