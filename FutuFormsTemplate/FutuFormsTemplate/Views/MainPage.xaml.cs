using FutuFormsTemplate.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace FutuFormsTemplate.Views
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
