using System.Globalization;

namespace FutuFormsTemplate.Services.DependencyInterfaces
{
    public interface ILocalizeService
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
