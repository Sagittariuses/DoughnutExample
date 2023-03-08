using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace DoughnutExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region LiveChart.WPF and 2

        List<string> ColorsHEX = new List<string>()
        {
            "#EA5D5F",
            "#E5A119",
            "#27AE60",

            //errors hex
            "#9BB1DB",
            "#1D499F",
            "#BB6BD9",
            "#2D9CDB",
            "#515151",
            "#C81862",
            "#4774CB",
        };

        List<int> ExistingHEX = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Chrome",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                },
                new PieSeries
                {
                    Title = "Mozilla",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                },
                new PieSeries
                {
                    Title = "Opera",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                },
                new PieSeries
                {
                    Title = "Explorer",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                }
            };


            Series  = new ISeries[]
            {
                new PieSeries<double> { Values = new List<double> { 2 }, InnerRadius = 50},
                new PieSeries<double> { Values = new List<double> { 2 }, InnerRadius = 50},
                new PieSeries<double> { Values = new List<double> { 2 }, InnerRadius = 50},
                new PieSeries<double> { Values = new List<double> { 2 }, InnerRadius = 50},
            };
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public ISeries[] Series { get; set; }
        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                foreach (var observable in series.Values.Cast<ObservableValue>())
                {
                    observable.Value = r.Next(1, 100);
                }
            }
        }

        private void AddSeriesOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            var c = SeriesCollection.Count > 0 ? SeriesCollection[0].Values.Count : 5;

            var vals = new ChartValues<ObservableValue>();

            for (var i = 0; i < c; i++)
            {
                vals.Add(new ObservableValue(r.Next(0, 10)));
            }

            int HexId = GetHexID(r);
            SeriesCollection.Add(new PieSeries
            {
                Values = vals,
                Fill = (Brush) new BrushConverter().ConvertFrom(ColorsHEX[HexId]),
                Margin = new Thickness(-15 - 15 -15 -15),
                StrokeThickness = 0,
                Stroke = (Brush)new BrushConverter().ConvertFrom(ColorsHEX[HexId]),
                
                
            });
        }

        private void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        {
            if (SeriesCollection.Count > 0)
                SeriesCollection.RemoveAt(0);
        }

        private void AddValueOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            foreach (var series in SeriesCollection)
            {
                series.Values.Add(new ObservableValue(r.Next(1, 10)));
            }
        }

        private void RemoveValueOnClick(object sender, RoutedEventArgs e)
        {
            foreach (var series in SeriesCollection)
            {
                if (series.Values.Count > 0)
                    series.Values.RemoveAt(0);
            }
        }

        private void RestartOnClick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true, true);
        }
        
        int GetHexID(Random r)
        {
            while (true)
            {
                bool Exist = false;
                int HexID = r.Next(0, ColorsHEX.Count-1);
                foreach (var item in ExistingHEX)
                {
                    if (Exist == true)
                        break;
                    Exist = item == HexID;
                }
               
                if (!Exist)
                {
                    ExistingHEX.Add(HexID);

                    return HexID;

                }

            }


        }
        #endregion

    }
}
