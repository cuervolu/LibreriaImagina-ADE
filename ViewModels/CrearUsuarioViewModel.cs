using MahApps.Metro.Controls.Dialogs;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;

namespace SistemaLibreriaImagina.ViewModels
{
    internal class CrearUsuarioViewModel : ObservableObject
    {
        private IDialogCoordinator dialogCoordinator;

        private USUARIO usuario;

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

        private bool _isFormValid;
        public bool IsFormValid
        {
            get { return _isFormValid; }
            set
            {
                _isFormValid = value;
                OnPropertyChanged(nameof(IsFormValid));
            }
        }

        private bool _isTipoUsuarioInvalid;

        public bool IsTipoUsuarioInvalid
        {
            get { return _isTipoUsuarioInvalid; }
            set
            {
                _isTipoUsuarioInvalid = value;
                OnPropertyChanged(nameof(IsTipoUsuarioInvalid));
            }
        }

        private string _selectedTipoUsuario;

        public string SelectedTipoUsuario
        {
            get { return _selectedTipoUsuario; }
            set
            {
                _selectedTipoUsuario = value;
                if (_selectedTipoUsuario == "Seleccione rol...")
                {
                    IsTipoUsuarioInvalid = true;
                    IsFormValid = false;
                }
                else
                {
                    IsTipoUsuarioInvalid = false;
                }

                // Notificar que la propiedad ha cambiado
                OnPropertyChanged(nameof(SelectedTipoUsuario));
            }
        }


        // Constructor
        public CrearUsuarioViewModel()
        {
            Usuario = new USUARIO();
        }


    }
}