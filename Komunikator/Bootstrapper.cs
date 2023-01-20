using Caliburn.Micro;
using Komunikator.ViewModels;
using Komunikator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Komunikator
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()  //ctor + 2xtab 
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
           
        }

        
    }
}
