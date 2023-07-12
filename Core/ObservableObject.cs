using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
/// <summary>
/// Clase base para objetos observables que implementa la interfaz INotifyPropertyChanged.
/// Proporciona funcionalidad para notificar cambios en las propiedades a los suscriptores.
/// </summary>
public class ObservableObject : INotifyPropertyChanged
{
    /// <summary>
    /// Evento que se desencadena cuando una propiedad ha cambiado su valor.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Invoca el evento PropertyChanged para notificar a los suscriptores que una propiedad ha cambiado su valor.
    /// </summary>
    /// <param name="name">Nombre de la propiedad que ha cambiado. Se obtiene automáticamente.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    /// <summary>
    /// Establece el valor de una propiedad y notifica a los suscriptores si ha cambiado.
    /// </summary>
    /// <typeparam name="T">Tipo de datos de la propiedad.</typeparam>
    /// <param name="field">Referencia a la variable de respaldo de la propiedad.</param>
    /// <param name="value">Nuevo valor de la propiedad.</param>
    /// <param name="propertyName">Nombre de la propiedad. Se obtiene automáticamente.</param>
    /// <returns>True si el valor de la propiedad ha cambiado y se notificó a los suscriptores; de lo contrario, False.</returns>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }


}
