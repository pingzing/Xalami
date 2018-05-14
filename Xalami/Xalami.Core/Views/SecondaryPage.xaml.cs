using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xalami.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xalami.Core.Views
{    
    public partial class SecondaryPage : ContentPage
    {
        public SecondaryPage(string passedString)
        {
            InitializeComponent();

            ((SecondaryViewModel)this.BindingContext).Parameter = passedString;
        }
    }
}