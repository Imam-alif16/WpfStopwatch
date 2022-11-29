using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfStopwatch.MVVM.View;
using WpfStopwatch.MVVM.ViewModel;

namespace WpfStopwatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CertMaker.rootCertExists())
            {
                CertMaker.removeFiddlerGeneratedCerts();
            }

            if (FiddlerApplication.oProxy != null)
            {
                if (FiddlerApplication.oProxy.IsAttached)
                {
                    FiddlerApplication.oProxy.Detach();
                }
            }
        }

    }
}
