using Android.App;
using Android.Content.PM;
using Android.OS;
using Xalami.Core;
using GalaSoft.MvvmLight.Ioc;
using Xalami.Core.Services.DependencyInterfaces;
using Xalami.Droid.ServiceImplementations;

namespace Xalami.Droid
{
    [Activity(Label = "Xalami", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            var coreApp = new App();

            RegisterPlatformServices();

            LoadApplication(new App());
        }

        // Register any platform-specific implementations of services here.
        private void RegisterPlatformServices()
        {
            SimpleIoc.Default.Register<ILocalizeService, LocalizeService>();
        }
    }
}

