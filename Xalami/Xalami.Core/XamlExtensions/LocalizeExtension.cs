using Xalami.Core.AppResources;
using Xalami.Core.Services.DependencyInterfaces;
using System;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xalami.Core.XamlExtensions
{
    [ContentProperty("Text")]
    public class LocalizeExtension : IMarkupExtension
    {        
        private readonly CultureInfo _ci;

        public string Text { get; set; }

        public LocalizeExtension()
        {
            if (Device.RuntimePlatform != Device.UWP)
            {
                _ci = DependencyService.Get<ILocalizeService>().GetCurrentCultureInfo();
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return "";
            }

            //Retrieve the existing ResourceManager rather than a new one, because on Windows platforms,
            //we've done some Reflection hackery to make it work correctly.
            //See the WindowsRuntimeResourceManager class in the Windows platform project for details.
            ResourceManager manager = Strings.ResourceManager;

            string translation;
            try
            {
                translation = manager.GetString(Text, _ci);

                if (translation == null)
                {
#if DEBUG
                    throw new ArgumentException($"Key '{Text}' was not found in resources {typeof(Strings).FullName} for culture {_ci?.Name ?? "Unknown culture"}", nameof(Text));
#else
                translation = "#" + Text; //Displays the key, which is shown to the user.
#endif
                }
            }
            catch (MissingManifestResourceException ex)
            {
#if DEBUG
                ArgumentException argEx = new ArgumentException($"Key '{Text}' was not found in resources {typeof(Strings).FullName} for culture {_ci?.Name ?? "Unknown culture"}", nameof(Text), ex);
                throw argEx;
#else
                translation = "#" + Text; //Displays the key, which is shown to the user.
#endif
            }
            return translation;
        }
    }
}
