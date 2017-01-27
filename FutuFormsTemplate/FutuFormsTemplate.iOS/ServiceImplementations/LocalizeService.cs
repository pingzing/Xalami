using Foundation;
using FutuFormsTemplate.AppResources;
using FutuFormsTemplate.iOS.ServiceImplementations;
using FutuFormsTemplate.Services.DependencyInterfaces;
using System.Globalization;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(LocalizeService))]
namespace FutuFormsTemplate.iOS.ServiceImplementations
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
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = iOSToDotnetLanguage(pref);
            }
            if (_ci == null)
            {
                try
                {
                    _ci = new CultureInfo(netLanguage);
                }                
                catch (CultureNotFoundException)
                {
                    // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                    // fallback to first characters, in this case "en"
                    try
                    {
                        var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                        _ci = new CultureInfo(fallback);
                    }
                    catch (CultureNotFoundException)
                    {
                        // iOS language not valid .NET culture, falling back to English
                        _ci = new CultureInfo("en");
                    }
                }
            }
            return _ci;
        }
        string iOSToDotnetLanguage(string iOSLanguage)
        {
            var netLanguage = iOSLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (iOSLanguage)
            {
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms"; // closest supported
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
                case "pt":
                    netLanguage = "pt-PT"; // fallback to Portuguese (Portugal)
                    break;
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
