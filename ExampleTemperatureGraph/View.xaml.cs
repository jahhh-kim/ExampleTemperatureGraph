using Arction.Wpf.BindableCharting;
using Arction.Wpf.BindableCharting.SeriesXY;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using System.Net.Http;
using System.Windows;
using System.Windows.Media;

namespace ExampleTemperatureGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View : Window
    {
        //   string path = "C:\\라이트닝차트\\WPFSB\\ExampleTemperatureGraph\\ExampleTemperatureGraph\\op10-1-train-0001.csv";
        //   List<double> DL_LoadSpindel = new List<double>();
        private LightningChartUltimate _zoomBarChart;

        public View()
        {
            _zoomBarChart = null;

            InitializeComponent();

         
            //var csvdata = CsvRead.readCSVWithHeader(path, true, "/", ",", true);

            //foreach (var item in csvdata)
            //{
            //    var _loadspindle = item;
            //    foreach (var item2 in _loadspindle)                   
            //    {
            //        if (item2.Key == "LoadSpindle")
            //        {
            //            double loadspindle = Convert.ToDouble(item2.Value);
            //            DL_LoadSpindel.Add(loadspindle);
            //            Console.WriteLine(loadspindle);
            //        }
            //    }
            //}

        }


        private void AxisY_MouseClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MessageBox.Show("짜잔");
        }
    }
}

