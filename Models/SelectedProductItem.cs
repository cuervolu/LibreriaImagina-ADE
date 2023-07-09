using SistemaLibreriaImagina.Core;

namespace SistemaLibreriaImagina.Models
{
    public class SelectedProductItem : ObservableObject
    {
        private LIBRO libro;
        private int cantidad;

        public LIBRO Libro
        {
            get { return libro; }
            set
            {
                libro = value;
                OnPropertyChanged();
            }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                cantidad = value;
                OnPropertyChanged();
                UpdatePrecioTotal();
            }
        }

        public decimal PrecioTotal { get; private set; }

        private void UpdatePrecioTotal()
        {
            PrecioTotal = Libro.PRECIO_UNITARIO * Cantidad;
            OnPropertyChanged(nameof(PrecioTotal));
        }
    }

}
