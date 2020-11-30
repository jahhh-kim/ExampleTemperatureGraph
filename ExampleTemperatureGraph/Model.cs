using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Threading;

namespace ExampleTemperatureGraph
{
    public class Model
    {

        private double m_dLatestTemperature = 50.0;
        private Random m_random = new Random();
        public bool UseNan = false;

        List<double> DL_LoadSpindel = new List<double>();


        public Model()
        {
            string path = "C:\\라이트닝차트\\WPFSB\\ExampleTemperatureGraph\\ExampleTemperatureGraph\\op10-1-train-0001.csv";
            var csvdata = CsvRead.readCSVWithHeader(path, true, "/", ",", true);

            foreach (var item in csvdata)
            {
                var _loadspindle = item;
                foreach (var item2 in _loadspindle)
                {
                    if (item2.Key == "LoadSpindle")
                    {
                        double loadspindle = Convert.ToDouble(item2.Value);
                        DL_LoadSpindel.Add(loadspindle);
                        Console.WriteLine(loadspindle);
                    }
                }
            }
        }

        int idx = 0;

        public double CalculateTemperature2()
        {
            double dY;

            dY = DL_LoadSpindel[idx];
            idx++;

            return dY;
        }

        public double CalculateTemperature()
        {
            double dY;
            if (UseNan == true)
            {
                // Add NaN for Y value
                dY = double.NaN;
            }
            else
            {
                // Use the latest value and generate some difference to it.
                dY = m_dLatestTemperature + (m_random.NextDouble() - 0.5) * 10.0;

                // Limit the value between 100...
                if (dY > 100)
                {
                    dY = 100;
                }

                // ... and 0.
                if (dY < 0)
                {
                    dY = 0;
                }

                // Update the latest values.
                m_dLatestTemperature = dY;
            }
            //Return temperature
            return dY;
        }
    }
}