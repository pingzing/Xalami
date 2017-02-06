using System.Threading.Tasks;

namespace Xalami.Mvvm
{
    public interface INavigable
    {
        Task Activated(NavigationType navType);

        Task Deactivated();
    }
}
