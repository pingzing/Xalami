using System.Globalization;

namespace Xalami.Services.DependencyInterfaces
{
    public interface ILocalizeService
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
