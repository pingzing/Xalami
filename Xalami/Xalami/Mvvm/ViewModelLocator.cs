using Xalami.Core.Services;
using Xalami.Core.ViewModels;
using Xalami.Core.Views;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;
using Xalami.Core.Services.DependencyInterfaces;

namespace Xalami.Core.Mvvm
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            //Set MVVM Light's SimpleIoc as the global ServiceLocator.
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Register (and initialize, if necessary) your services here
            SimpleIoc.Default.Register<INavigationService>(InitializeNavigationService);
            SimpleIoc.Default.Register<ILocalizeService>(() => DependencyService.Get<ILocalizeService>());

            //Register your ViewModels here
            SimpleIoc.Default.Register<MainViewModel>();
        }

        private INavigationService InitializeNavigationService()
        {
            NavigationService navService = new NavigationService(((App)Application.Current).MainNavigationHost)
                .Configure(typeof(MainViewModel), typeof(MainPage));

            return navService;
        }
    }
}
