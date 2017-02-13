using ${ProjectName}.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace ${ProjectName}.Views
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

