using Arction.Wpf.SemibindableCharting;
using Arction.Wpf.SemibindableCharting.SeriesXY;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ExampleTemperatureGraph
{
    public class ViewModel: DependencyObject
    {
        private DelegateCommand m_addNanCommand = null;
        private Model _model = null;

        public static readonly int DateOriginYear = 2015;
        public static readonly int DateOriginMonth = 1;
        public static readonly int DateOriginDay = 1;
        #region Dependency Properties
        /// <summary>
        /// Identifies IsEnabled dependency property
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(
                "IsEnabled",
                typeof(bool),
                typeof(ViewModel),
                new PropertyMetadata(true)
            );
        /// <summary>
        /// Identifies Points dependency property
        /// </summary>
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register(
                "Points",
                typeof(SeriesPoint[]),
                typeof(ViewModel)
            );
        /// <summary>
        /// Identifies ScrollPosition dependency property
        /// </summary>
        public static readonly DependencyProperty ScrollPositionProperty =
            DependencyProperty.Register(
                "ScrollPosition",
                typeof(double),
                typeof(ViewModel)
            );
        /// <summary>
        /// Identifies XMaximum dependency property
        /// </summary>
        public static readonly DependencyProperty XMaximumProperty =
            DependencyProperty.Register(
                "XMaximum",
                typeof(double),
                typeof(ViewModel)
            );
        /// <summary>
        /// Identifies XMinimum dependency property
        /// </summary>
        public static readonly DependencyProperty XMinimumProperty =
            DependencyProperty.Register(
                "XMinimum",
                typeof(double),
                typeof(ViewModel)
            );
        
        public bool IsRunning
        {
            get { return IsEnabled; }
        }

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set 
            { SetValue(IsEnabledProperty, value); }
        }

        public SeriesPoint[] Points
        {
            get { return GetValue(PointsProperty) as SeriesPoint[]; }
            set { SetValue(PointsProperty, value); }
        }

        public double ScrollPosition
        {
            get { return (double)GetValue(ScrollPositionProperty); }
            set { SetValue(ScrollPositionProperty, value); }
        }

        public double XMaximum
        {
            get { return (double)GetValue(XMaximumProperty); }
            set { SetValue(XMaximumProperty, value); }
        }

        public double XMinimum
        {
            get { return (double)GetValue(XMinimumProperty); }
            set { SetValue(XMinimumProperty, value); }
        }
 


        #endregion
        public void Start()
        {
      
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        public void Stop()
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }
        
        #region Constructor
        public ViewModel()
        {
            m_addNanCommand = new DelegateCommand(AddNanMethod);
            _model = new Model();
            double dMin = DateTimeToAxisValue(DateTime.Now);
            double dMax = dMin + 30.0;
            XMaximum = dMax;
            XMinimum = dMin;
            Start();
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (IsEnabled)
            {
                GenerateData();
            }
        }


        #endregion
        public ICommand AddNanCommand
        {
            get { return m_addNanCommand; }
        }

        private void AddNanMethod(object obj)
        {
             ThreadPool.QueueUserWorkItem(
                new WaitCallback(
                    delegate (object o)
                    {
                        _model.UseNan = true;
                        Thread.Sleep(1000);
                        _model.UseNan = false;
                    }
                )
            );
        }

        List<SeriesPoint> pointList = new List<SeriesPoint>();

        ///데이터 발생
        ///
        /// <summary>
        /// Generate data
        /// </summary>
        private void GenerateData()
        {
            double x = DateTimeToAxisValue(DateTime.Now);
            var seriesPoint = new SeriesPoint() { X = x, Y = _model.CalculateTemperature2() };
            pointList.Add(seriesPoint);
            Points = pointList.ToArray();
            ScrollPosition = x;
        }

        /// <summary>
        /// Convert DateTime value to double.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public double DateTimeToAxisValue(DateTime dateTime)
        {
            DateTime dt = new DateTime(DateOriginYear, DateOriginMonth, DateOriginDay);
            return (double)(dateTime.Ticks - dt.Ticks) / (double)TimeSpan.TicksPerSecond;
        }
    }
}

