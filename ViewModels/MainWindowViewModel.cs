using SistemaLibreriaImagina.Core;

namespace SistemaLibreriaImagina.ViewModels
{
    class MainWindowViewModel : ObservableObject
    {
        public RelayCommand InicioViewCommand { get; set; }
        public RelayCommand InventarioViewCommand { get; set; }

        public InicioViewModel InicioVM { get; set; }
        public InventarioViewModel InventarioVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            InicioVM = new InicioViewModel();
            InventarioVM = new InventarioViewModel();
            CurrentView = InicioVM;

            InicioViewCommand = new RelayCommand(o =>
            {
                CurrentView = InicioVM;
            });

            InventarioViewCommand = new RelayCommand(o =>
            {
                CurrentView = InventarioVM;
            });

        }
    }
}
