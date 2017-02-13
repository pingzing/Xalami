using System.Threading.Tasks;

namespace ${ProjectName}.Mvvm
{
    public interface INavigable
    {
        Task Activated(NavigationType navType);

        Task Deactivated();
    }
}

