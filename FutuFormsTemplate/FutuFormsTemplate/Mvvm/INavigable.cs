using System.Threading.Tasks;

namespace FutuFormsTemplate.Mvvm
{
    public interface INavigable
    {
        Task Activated(NavigationType navType);

        Task Deactivated();
    }
}
