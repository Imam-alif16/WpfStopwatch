using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfStopwatch.MVVM.View
{
    /// <summary>
    /// Interaction logic for Page2View.xaml
    /// </summary>
    public partial class Page2View : UserControl
    {
        private const string _startStopwatchDisplay = "00:00:00:00";
        private Timer _timer;

        int h, m, s, ms;

        public Page2View()
        {
            InitializeComponent();

            StopwatchDisplay.Text = _startStopwatchDisplay;

            _timer = new Timer(interval:1);
            _timer.Elapsed += OnTimerElapse;
        }

        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ms += 1;
                if (ms == 100)
                {
                    ms = 0;
                    s += 1;
                }
                if (s == 60)
                {
                    s = 0;
                    m += 1;
                }
                if (m == 60)
                {
                    m = 0;
                    h += 1;

                }

                StopwatchDisplay.Text = String.Format("{0}:{1}:{2}:{3}", h.ToString().ToString().PadLeft(2, '0'), m.ToString().ToString().PadLeft(2, '0'), s.ToString().ToString().PadLeft(2, '0'), ms.ToString().ToString().PadLeft(2, '0'));
            });
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }
    }
}
