using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Windows.Controls;
using System.Windows;

namespace ESM.Modules.Export.Views
{
    /// <summary>
    /// Interaction logic for ReportView
    /// </summary>
    public partial class ReportView : UserControl
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    DateTime startDate = startTimePicker.SelectedDate.Value;
        //    DateTime endDate = endTimePicker.SelectedDate.Value;
        //    bool showLaptop = laptopCheckBox.IsChecked.Value;
        //    bool showSmartphone = smartphoneCheckBox.IsChecked.Value;
        //    bool showPC = pcCheckBox.IsChecked.Value;
        //    bool showCPU = cpuCheckBox.IsChecked.Value;
        //    bool showVGA = vgaCheckBox.IsChecked.Value;
        //    bool showMonitor = monitorCheckBox.IsChecked.Value;
        //    bool showHarddisk = harddiskCheckBox.IsChecked.Value;

        //    if (!DateTime.TryParse(startTimePicker.Text, out DateTime startTime) ||
        //           !DateTime.TryParse(endTimePicker.Text, out DateTime endTime) ||
        //           startTime > endTime)
        //    {
        //        MessageBox.Show("Invalid date range!");
        //        return;
        //    }
        //    SeriesCollection series = new SeriesCollection();
        //    if (showLaptop)
        //    {
        //        series.Add(new LineSeries
        //        {
        //            Title = "Laptop",
        //            Values = new ChartValues<double> { 10, 9, 26, 38, 35, 40, 45 },
        //            DataLabels = true
        //        });
        //    }
        //    if (showSmartphone)
        //    {
        //        series.Add(new LineSeries
        //        {
        //            Title = "Smartphone",
        //            Values = new ChartValues<double> { 5, 15, 12, 20, 45, 30, 65 },
        //            DataLabels = true
        //        });
        //    }
        //    if (showPC)
        //    {
        //        series.Add(new LineSeries
        //        {
        //            Title = "PC",
        //            Values = new ChartValues<double> { 8, 18, 28, 25, 48, 30, 40 },
        //            DataLabels = true
        //        });
        //    }
        //    if (showCPU)
        //    {
        //        series.Add(new LineSeries
        //        {
        //            Title = "CPU",
        //            Values = new ChartValues<double> { 9, 13, 49, 39, 40, 52, 43 },
        //        });
        //    }
        //    if (showVGA)
        //    {
        //        series.Add(new LineSeries
        //        {
        //            Title = "VGA",
        //            Values = new ChartValues<double> { 7, 17, 27, 37, 47, 36, 30 },
        //            DataLabels = true
        //        });
        //    }
        //    if (showMonitor)
        //    {
        //        series.Add(new LineSeries
        //        {
        //            Title = "Monitor",
        //            Values = new ChartValues<double> { 4, 14, 24, 28, 44, 54, 40 },
        //            DataLabels = true
        //        });
        //    }
        //    if (showHarddisk)
        //    {
        //        series.Add(new LineSeries
        //        {
        //            Title = "Harddisk",
        //            Values = new ChartValues<double> { 6, 16, 20, 25, 30, 36, 24 },
        //            DataLabels = true
        //        });
        //    }
        //    Hiển thị biểu đồ trên ChartPlotter
        //    ChartPlotter.Series = series;
        //    ChartPlotter.LegendLocation = LegendLocation.Right;
        //}
    }
}