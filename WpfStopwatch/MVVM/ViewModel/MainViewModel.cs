using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfStopwatch.Core;

namespace WpfStopwatch.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand Page1ViewCommand { get; set; }
        public RelayCommand Page2ViewCommand { get; set; }
        public RelayCommand Page3ViewCommand { get; set; }
        public Page1ViewModel Page1VM { get; set; }
        public Page2ViewModel Page2VM { get; set; }
        public Page3ViewModel Page3VM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();    
            }
        }

        public MainViewModel()
        {
            Page1VM = new Page1ViewModel();
            Page2VM = new Page2ViewModel();
            Page3VM = new Page3ViewModel();


            CurrentView = Page2VM;

            Page1ViewCommand = new RelayCommand(o =>
            {
                CurrentView = Page1VM;
            });

            Page2ViewCommand = new RelayCommand(o =>
            {
                CurrentView = Page2VM;
            });

            Page3ViewCommand = new RelayCommand(o =>
            {
                CurrentView = Page3VM;
            });
        }
    }
}
