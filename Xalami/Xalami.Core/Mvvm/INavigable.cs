using System.Threading.Tasks;

namespace Xalami.Core.Mvvm
{
    public interface INavigable
    {
        Task Activated(NavigationType navType);

        Task Deactivated();
    }
}
