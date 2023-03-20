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
            "#E6E3E2",


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
            seriesCollection = new SeriesCollection
            {
              
                new PieSeries
                {
                    Title = "Errors",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    Fill = (Brush) new BrushConverter().ConvertFrom(ColorsHEX[0]),
                    Margin = new Thickness(-15 - 15 -15 -15),
                    StrokeThickness = 0,
                    Stroke = (Brush)new BrushConverter().ConvertFrom(ColorsHEX[0]),
                    PushOut = 0
                },
               
                new PieSeries
                {
                    Title = "Fill ",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    Fill = (Brush) new BrushConverter().ConvertFrom(ColorsHEX[3]),
                    Margin = new Thickness(-15 - 15 -15 -15),
                    StrokeThickness = 0,
                    Stroke = (Brush)new BrushConverter().ConvertFrom(ColorsHEX[3]),
                },
            
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("aaaaaaaaaa");
        }

        public SeriesCollection seriesCollection { get; set; }
        public ISeries[] Series { get; set; }
        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            foreach (var series in seriesCollection)
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
            var c = seriesCollection.Count > 0 ? seriesCollection[0].Values.Count : 5;

            var vals = new ChartValues<ObservableValue>();

            for (var i = 0; i < c; i++)
            {
                vals.Add(new ObservableValue(r.Next(0, 10)));
            }

            int HexId = GetHexID(r);
            seriesCollection.Add(new PieSeries
            {
                Values = vals,
                Fill = (Brush) new BrushConverter().ConvertFrom(ColorsHEX[HexId]),
                Margin = new Thickness(-15 - 15 -15 -15),
                StrokeThickness = 0,
                Stroke = (Brush)new BrushConverter().ConvertFrom(ColorsHEX[HexId]),
                PushOut = -20,
                
            });
        }

        private void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        {
            if (seriesCollection.Count > 0)
                seriesCollection.RemoveAt(0);
        }

        private void AddValueOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            foreach (var series in seriesCollection)
            {
                if (series.Title.StartsWith("E"))
                {
                    series.Values.Add(new ObservableValue(1));
                }
                else
                {
                    series.Values.Add(new ObservableValue(10));
                }
            }
        }

        private void RemoveValueOnClick(object sender, RoutedEventArgs e)
        {
            foreach (var series in seriesCollection)
            {
                if (series.Values.Count > 0)
                    series.Values.RemoveAt(0);
            }
        }

        private void RestartOnClick(object sender, RoutedEventArgs e)
        {
            //Chart.Update(true, true);
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

        private void dragMe(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }
    }
}
