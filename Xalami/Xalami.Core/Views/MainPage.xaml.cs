using GalaSoft.MvvmLight.Ioc;
using Xalami.Core.ViewModels;
using Xamarin.Forms;

namespace Xalami.Core.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.BindingContext = SimpleIoc.Default.GetInstance<MainViewModel>();
            InitializeComponent();
        }
    }
}
