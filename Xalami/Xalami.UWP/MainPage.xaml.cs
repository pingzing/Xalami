namespace Xalami.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Core.App coreApp = new Core.App();

            RegisterPlatformServices();

            LoadApplication(coreApp);
        }

        // Register any platform-specific implementations of services here.
        private void RegisterPlatformServices()
        {

        }
    }
}
