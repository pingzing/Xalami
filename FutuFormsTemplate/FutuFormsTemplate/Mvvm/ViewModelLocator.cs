using FutuFormsTemplate.Services;
using FutuFormsTemplate.ViewModels;
using FutuFormsTemplate.Views;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutuFormsTemplate.Mvvm
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            //Set MVVM Light's SimpleIoc as the global ServiceLocator.
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            //Register (and initialize, if necessary) your services here
            SimpleIoc.Default.Register<INavigationService>(InitializeNavigationService);

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
