using FutuFormsTemplate.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutuFormsTemplate.Services
{

    //todo: xml doooooc
    public interface INavigationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="animated"></param>
        /// <returns></returns>
        Task GoBackAsync(bool animated);

        Task NavigateToViewModelAsync<T>(bool animated = true) where T : INavigable;
        Task NavigateToViewModelAsync(Type vmType, bool animated = true);
        Task NavigateToViewModelAsync(Type vmType, object parameter, bool animated = true);
        Task NavigateToPageAsync<T>(bool animated = true) where T : Page;
        Task NavigateToViewModelAsync<T>(object parameter, bool animated = true) where T : INavigable;
        Task NavigateToPageAsync<T>(object parameter, bool animated = true) where T : Page;

        void ClearBackStack();

        bool CanGoBack { get; }

        event EventHandler<CanGoBackChangedHandlerArgs> CanGoBackChanged;
    }
}
