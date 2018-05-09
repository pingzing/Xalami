using Xalami.Core.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace Xalami.Core.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.BindingContext = ServiceLocator.Current.GetInstance<MainViewModel>();
            InitializeComponent();
        }
    }
}
