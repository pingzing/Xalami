using Xalami.Core.Services;
using Xalami.Core.ViewModels;
using Xalami.Core.Views;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;

namespace Xalami.Core.Mvvm
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public SecondaryViewModel SecondaryViewModel => SimpleIoc.Default.GetInstance<SecondaryViewModel>();

        public ViewModelLocator()
        {                        
            //Register (and initialize, if necessary) your services here
            SimpleIoc.Default.Register<INavigationService>(InitializeNavigationService);            

            //Register your ViewModels here
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SecondaryViewModel>();
        }

        private INavigationService InitializeNavigationService()
        {
            NavigationService navService = new NavigationService(((App)Application.Current).MainNavigationHost)
                .Configure(typeof(MainViewModel), typeof(MainPage))
                .Configure(typeof(SecondaryViewModel), typeof(SecondaryPage));

            return navService;
        }
    }
}
