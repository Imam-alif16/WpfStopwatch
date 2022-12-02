using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Fiddler;

namespace WpfStopwatch.MVVM.View
{
    /// <summary>
    /// Interaction logic for Page2View.xaml
    /// </summary>
    public partial class Page2View : UserControl
    {
        private const string _startStopwatchDisplay = "00:00:00.00";
        private Timer _timer;

        int h, m, s, ms;
        string Firstline { get; set; }

        

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

                StopwatchDisplay.Text = String.Format("{0}:{1}:{2}.{3}", h.ToString().ToString().PadLeft(2, '0'), m.ToString().ToString().PadLeft(2, '0'), s.ToString().ToString().PadLeft(2, '0'), ms.ToString().ToString().PadLeft(2, '0'));
            });
        }

        private void Starts()
        {
            if (!FiddlerApplication.IsStarted())
            {
                FiddlerCoreStartupSettings startupSettings =
                    new FiddlerCoreStartupSettingsBuilder()
                        .ListenOnPort(8888)
                        .RegisterAsSystemProxy()
                        .DecryptSSL()
                        .AllowRemoteClients()
                        .Build();

                FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete;
                FiddlerApplication.Startup(startupSettings);
            }
            else
            {

            }
        }

        private void FiddlerApplication_AfterSessionComplete(Session oSession)
        {
            if (oSession.RequestMethod == "CONNECT")
            {
                return;
            }
            if (oSession.RequestMethod == null || oSession.oRequest == null || oSession.oRequest.headers == null)
            {
                return;
            }

            string Headers = oSession.oRequest.headers.ToString();
            //string Body = Encoding.UTF8.GetString(oSession.RequestBody);
            //Firstline = oSession.RequestMethod + " " + oSession.fullUrl + " " + oSession.oRequest.headers.HTTPVersion;
            //Firstline = oSession.RequestMethod + " " + oSession.fullUrl;
            Firstline = oSession.fullUrl;
            int at = Headers.IndexOf("\r\n");
            if (at < 0)
            {
                return;
            }
            //string Output = Firstline + Environment.NewLine + Headers.Substring(at + 1);
            string Output = Firstline;
            //if (Body!= null)
            //{
            //    Output += Body + Environment.NewLine;
            //}
            //appentext(Output);
            Console.WriteLine(Output);
            Comparetext(Firstline);
            Comparestop(Firstline);
        }

        private void Stops()
        {
            if (!FiddlerApplication.IsStarted())
            {

            }
            else
            {
                FiddlerApplication.Shutdown();
            }
        }

        private void Installcert()
        {
            if (!CertMaker.rootCertExists())
            {
                CertMaker.createRootCert();
                CertMaker.trustRootCert();
            }
        }

        private void Removecert()
        {
            if (CertMaker.rootCertExists())
            {
                CertMaker.removeFiddlerGeneratedCerts();
            }
        }

        private bool Comparetext(string value)
        {
            if (value == "https://akademik.uin-suka.ac.id/")
            {
                _timer.Start();
                
            }
            return true;
        }

        private async void Comparestop(string value)
        {
            await Task.Run(async () =>
            {
                if (Comparetext(Firstline) == true)
                {
                    if (value == "https://akademik.uin-suka.ac.id/upbi/daftarplacement")
                    {
                        _timer.Stop();

                    }
                    else
                    {
                        await Task.Delay(5000);
                        Comparestop(Firstline);
                    }
                }
            });
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Starts();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Stops();
        }

        private void Install_Click(object sender, RoutedEventArgs e)
        {
            Installcert();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Removecert();
        }

        //private void Start_Click(object sender, RoutedEventArgs e)
        //{
        //    _timer.Start();
        //}

        //private void Stop_Click(object sender, RoutedEventArgs e)
        //{
        //    _timer.Stop();
        //}
    }
}
