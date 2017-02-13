using ${ProjectName}.Mvvm;
using ${ProjectName}.Services;

namespace ${ProjectName}.ViewModels
{
    public class MainViewModel : NavigableViewModelBase
    {
        public MainViewModel(INavigationService navService) : base(navService)
        {
            //Add any other services as constructor arguments and cache them locally here.
        }
    }
}

