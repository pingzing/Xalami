using Xalami.ExtensionMethods;
using Xalami.Services;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;

namespace Xalami.Mvvm
{
    public class NavigableViewModelBase : ViewModelBase, INavigable
    {
        protected INavigationService _navigationService;

        /// <summary>
        /// An all-purpose container for passing in navigation parameters from the View upon initialization.
        /// </summary>
        public object Parameter { get; set; }

        public NavigableViewModelBase(INavigationService navService)
        {
            _navigationService = navService;
        }

        /// <summary>
        /// Fires when the ViewModel is navigated to via the NavigationService.
        /// If you don't want to mark your overrides as async, return a 
        /// CompletedTask using the <see cref="LocalTaskExtensions.CompletedTask"/>.
        /// </summary>
        /// <returns></returns>
        public virtual async Task Activated(NavigationType navType)
        {
            await LocalTaskExtensions.CompletedTask;
        }

        /// <summary>
        /// Fires when the ViewModel is navigated from via the NavigationService.        
        /// If you don't want to mark your overrides as async, return a 
        /// CompletedTask using the <see cref="LocalTaskExtensions.CompletedTask"/>.
        /// </summary>
        public virtual async Task Deactivated()
        {
            await LocalTaskExtensions.CompletedTask;
        }
    }
}
