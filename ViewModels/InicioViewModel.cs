using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Notifications.Wpf;
using SistemaLibreriaImagina.Core;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SistemaLibreriaImagina.ViewModels
{
    internal class InicioViewModel : ObservableObject
    {
        private int cantidadLibros;

        public int CantidadLibros
        {
            get { return cantidadLibros; }
            set
            {
                cantidadLibros = value;
                OnPropertyChanged();
            }
        }

        private int cantidadPedidos;

        public int CantidadPedidos
        {
            get { return cantidadPedidos; }
            set
            {
                cantidadPedidos = value;
                OnPropertyChanged();
            }
        }

        private USUARIO usuario;

        public USUARIO Usuario
        {
            get { return usuario; }
            set { usuario = value; OnPropertyChanged(); }
        }

        public InicioViewModel()
        {
            Usuario = UsuarioGlobal.Instancia.Usuario;
            obtenerCantidadDeLibros();
            obtenerCantidadDePedidos();
            generatePieChart();
        }

        private int obtenerCantidadDeLibros()
        {
            try
            {
                var response = BookService.GetAmountBooks();
                cantidadLibros = response;
                CantidadLibros = cantidadLibros; // Utilizar el setter de la propiedad para notificar el cambio
                return cantidadLibros;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"No se puede obtener la cantidad de libros: {ex}");
                cantidadLibros = 0;
                CantidadLibros = cantidadLibros; // Utilizar el setter de la propiedad para notificar el cambio
                return cantidadLibros;
            }
        }

        private int obtenerCantidadDePedidos() //Pedidos en validación
        {
            try
            {
                var response = OrderService.GetAmountOrders();
                cantidadPedidos = response;
                CantidadPedidos = cantidadPedidos; // Utilizar el setter de la propiedad para notificar el cambio
                return cantidadPedidos;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"No se puede obtener la cantidad de libros: {ex}");
                cantidadPedidos = 0;
                CantidadLibros = cantidadPedidos; // Utilizar el setter de la propiedad para notificar el cambio
                return cantidadPedidos;
            }
        }

        public void generatePieChart()
        {
            // Obtener los datos del grÃ¡fico circular desde OrderService
            var (values, labels) = OrderService.GetOrderStatusCount();

            List<PieSeries<double>> series = new List<PieSeries<double>>();

            for (int i = 0; i < values.Length; i++)
            {
                double value = values[i];
                string label = labels[i];
                SolidColorPaint color = statusColors[i % statusColors.Count];

                var pieSeries = new PieSeries<double>
                {
                    Values = new[] { value },
                    Name = label,
                    DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30)),
                    DataLabelsFormatter = p => $"{p.PrimaryValue} / {(p.StackedValue != null ? p.StackedValue.Total : 0)} ({(p.StackedValue != null ? p.StackedValue.Share : 0):P2})",
                    TooltipLabelFormatter = p => $"{p.PrimaryValue:C2}",
                    Fill = color
                };

                series.Add(pieSeries);
            }

            Series = series;
        }

        public List<SolidColorPaint> statusColors = new List<SolidColorPaint>
        {
            new SolidColorPaint(new SKColor(20, 110, 137, 255)),
            new SolidColorPaint(new SKColor(31, 139, 172, 255)),
            new SolidColorPaint(new SKColor(100, 180, 205, 255)),
            new SolidColorPaint(new SKColor(130, 194, 203, 255)),
            new SolidColorPaint(new SKColor(107, 155, 167, 255))
        };

        private void ShowErrorMessage(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show(new NotificationContent
                {
                    Title = "Error",
                    Message = message,
                    Type = NotificationType.Error
                });
            }));
        }

        public IEnumerable<ISeries> Series { get; set; }

        public LabelVisual Title { get; set; } =
            new LabelVisual
            {
                Text = "Número de Pedidos en Base a Estado",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };
    }
}