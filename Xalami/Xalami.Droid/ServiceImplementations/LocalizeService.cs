using Xalami.Services.DependencyInterfaces;
using System.Globalization;
using Java.Util;
using Xalami.Droid.ServiceImplementations;
using System.Threading;
using Xalami.AppResources;

[assembly: Xamarin.Forms.Dependency(typeof(LocalizeService))]
namespace Xalami.Droid.ServiceImplementations
{
    public class LocalizeService : ILocalizeService
    {
        private static CultureInfo _ci = null;

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var androidLocale = Locale.Default;
            netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));

            if (_ci == null)
            {
                try
                {
                    _ci = new CultureInfo(netLanguage);
                }
                catch (CultureNotFoundException e1)
                {
                    // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                    // fallback to first characters, in this case "en"
                    try
                    {
                        var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                        _ci = new CultureInfo(fallback);
                    }
                    catch (CultureNotFoundException e2)
                    {
                        // iOS language not valid .NET culture, falling back to English
                        _ci = new CultureInfo("en");
                    }
                }
            }
            return _ci;
        }
        string AndroidToDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (androidLanguage)
            {
                case "ms-BN":   // "Malaysian (Brunei)" not supported .NET culture
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms"; // closest supported
                    break;
                case "in-ID":  // "Indonesian (Indonesia)" has different code in  .NET
                    netLanguage = "id-ID"; // correct code for .NET
                    break;
                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }
            return netLanguage;
        }
        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (two chars, usually);
            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }
            return netLanguage;
        }
    }
}
