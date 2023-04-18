using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Windows.Controls;
using System.Windows;
using LiveCharts.Wpf.Charts.Base;

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
            myChart.AxisY.Add(new() { MinValue = 0 });
        }
    }
}