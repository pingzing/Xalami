using System.Globalization;

namespace Xalami.Core.Services.DependencyInterfaces
{
    public interface ILocalizeService
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
