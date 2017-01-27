using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FutuFormsTemplate.Mvvm
{

    //TODO: XMLDoc everywheeeere
    public partial class NavigationHost : NavigationPage, IWakeUp
    {
        /// <summary>
        /// Indicates whether there is another page beneath the current page in the Back Stack.
        /// </summary>
        public bool CanGoBack => this.Navigation.NavigationStack.Count > 1;

        /// <summary>
        /// Fires when <see cref="CanGoBack"/> changes.
        /// </summary>
        public event EventHandler<CanGoBackChangedHandlerArgs> CanGoBackChanged;

        public NavigationHost()
        {
            InitializeComponent();
            this.Popped += MainNavigationHost_Popped;
            this.Pushed += MainNavigationHost_Pushed;
        }

        private void MainNavigationHost_Popped(object sender, NavigationEventArgs e)
        {
            INavigable vm = e.Page.BindingContext as INavigable;
            if (vm != null)
            {
                vm.Deactivated();
            }
            INavigable newTopVm = Navigation.NavigationStack.LastOrDefault()?.BindingContext as INavigable;
            if (newTopVm != null)
            {
                newTopVm.Activated(NavigationType.Back);
            }
            RaiseCanGoBackChanged();
        }

        private void MainNavigationHost_Pushed(object sender, NavigationEventArgs e)
        {
            if (Navigation.NavigationStack.Count >= 2)
            {
                int secondToLastIndex = Navigation.NavigationStack.Count - 2;
                INavigable previousVm = Navigation.NavigationStack[secondToLastIndex].BindingContext as INavigable;
                if (previousVm != null)
                {
                    previousVm.Deactivated();
                }
            }
            INavigable vm = e.Page.BindingContext as INavigable;
            if (vm != null)
            {
                vm.Activated(NavigationType.Forward);
            }
            RaiseCanGoBackChanged();
        }

        private void RaiseCanGoBackChanged()
        {
            CanGoBackChanged?.Invoke(this, new CanGoBackChangedHandlerArgs(CanGoBack));
        }

        public async Task NavigateToAsync(Page destination, bool animated = true)
        {
            await this.PushAsync(destination, animated);
        }

        public async Task GoBackAsync(bool animated = true)
        {
            await this.PopAsync(animated);
        }

        public async Task GoHomeAsync(bool animated = true)
        {
            await this.PopToRootAsync(animated);
        }

        public async Task WakeUp()
        {
            var activeVm = Navigation.NavigationStack.LastOrDefault()?.BindingContext as INavigable;
            await activeVm?.Activated(NavigationType.Forward);
        }

        /// <summary>
        /// Removes all <see cref="Page"/>s from the NavigationStack except for the currently-displayed page.
        /// </summary>
        public void ClearBackStack()
        {
            if (this.Navigation.NavigationStack.Count > 1)
            {
                while (this.Navigation.NavigationStack.Count > 1)
                {
                    this.Navigation.RemovePage(this.Navigation.NavigationStack[0]);
                }
            }
            RaiseCanGoBackChanged();
        }
    }

    public class CanGoBackChangedHandlerArgs
    {
        public bool NewCanGoBack { get; private set; }

        public CanGoBackChangedHandlerArgs(bool newCanGoBack)
        {
            NewCanGoBack = newCanGoBack;
        }
    }
}
