using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStopwatch.MVVM.ViewModel
{
    internal class Page1ViewModel
    {
        public List<string> NameCollection { get; set; }

        public Page1ViewModel()
        {
            NameCollection = new List<string>()
            {
                "GET",
                "PUT",
                "POST",
                "DELETE"
            };
        }
    }
}
