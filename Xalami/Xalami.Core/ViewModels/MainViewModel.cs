using Xalami.Core.Mvvm;
using Xalami.Core.Services;

namespace Xalami.Core.ViewModels
{
    public class MainViewModel : NavigableViewModelBase
    {
        public MainViewModel(INavigationService navService) : base(navService)
        {
            //Add any other services as constructor arguments and cache them locally here.
        }
    }
}
