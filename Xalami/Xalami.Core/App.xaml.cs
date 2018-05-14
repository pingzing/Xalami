using Xalami.Core.Mvvm;
using Xalami.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xalami.Core
{    
    public partial class App : Application
    {
        private bool _initialized;

        public ViewModelLocator Locator;
        public NavigationHost MainNavigationHost { get; set; }

        public App()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;

            InitializeComponent();
            MainNavigationHost = new NavigationHost();
            Locator = (ViewModelLocator)Current.Resources["Locator"];

            MainPage = MainNavigationHost;
        }        

        protected override async void OnStart()
        {
            await MainNavigationHost.PushAsync(new MainPage());
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }        
    }
}
