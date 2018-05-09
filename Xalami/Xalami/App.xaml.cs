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
        public ViewModelLocator Locator;

        public NavigationHost MainNavigationHost { get; set; }

        public App()
        {
            InitializeComponent();
            MainNavigationHost = new NavigationHost();
            Locator = new ViewModelLocator();

            MainPage = new MainPage();
        }        

        protected override void OnStart()
        {
            // Handle when your app starts
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
