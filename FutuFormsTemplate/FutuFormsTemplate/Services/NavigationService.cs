using FutuFormsTemplate.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutuFormsTemplate.Services
{
    public class NavigationService : INavigationService
    {
        private readonly List<Type> _knownPageTypes = new List<Type>();
        private readonly Dictionary<Type, Type> _viewmodelPageAssociations = new Dictionary<Type, Type>();
        private readonly NavigationHost _navHost;

        private readonly SemaphoreSlim _knownPagesLock = new SemaphoreSlim(1);
        private readonly SemaphoreSlim _vmAssociationsLock = new SemaphoreSlim(1);

        public bool CanGoBack => _navHost.CanGoBack;

        public event EventHandler<CanGoBackChangedHandlerArgs> CanGoBackChanged;

        public NavigationService(NavigationHost navigation)
        {
            _navHost = navigation;
            _navHost.CanGoBackChanged += navHost_CanGoBackChanged;
        }

        private void navHost_CanGoBackChanged(object sender, CanGoBackChangedHandlerArgs e)
        {
            CanGoBackChanged?.Invoke(sender, e);
        }

        public Type CurrentPageKey
        {
            get
            {
                lock (_knownPageTypes)
                {
                    if (_navHost.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = _navHost.CurrentPage.GetType();

                    return _knownPageTypes.Contains(pageType)
                        ? _knownPageTypes.First(p => p == pageType)
                        : null;
                }
            }
        }

        public async Task GoBackAsync(bool animated = true)
        {
            await _navHost.GoBackAsync(animated);
        }

        /// <summary>
        /// Navigates to the <see cref="Page"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Page"/> to navigate to.</typeparam>
        /// <param name="animated"></param>
        public async Task NavigateToPageAsync<T>(bool animated = true) where T : Page
        {
            await NavigateToAsync(typeof(T), animated);
        }

        /// <summary>
        /// Navigates to the <see cref="Page"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Page"/> to navigate to.</typeparam>
        /// <param name="animated"></param>
        public async Task NavigateToPageAsync<T>(object parameter, bool animated = true) where T : Page
        {
            await NavigateToAsync(typeof(T), parameter, animated);
        }

        /// <summary>
        /// Navigates to the <see cref="INavigable"/> ViewModel.
        /// </summary>
        /// <typeparam name="T">The type of the ViewModel to navigate to.</typeparam>
        /// <param name="animated">Whether or not the page transition should be animated.</param>
        public async Task NavigateToViewModelAsync<T>(bool animated = true) where T : INavigable
        {
            Type vmType = typeof(T);
            await NavigateToViewModelAsync(vmType);
        }

        /// <summary>
        /// Navigates to the <see cref="INavigable"/> ViewModel.
        /// </summary>
        /// <typeparam name="T">The type of the ViewModel to navigate to.</typeparam>
        /// <param name="parameter">An optional parameter to send to the ViewModel.</param>
        /// <param name="animated">Whether or not the page transition should be animated.</param>
        public async Task NavigateToViewModelAsync<T>(object parameter, bool animated = true)
            where T : INavigable
        {
            Type vmType = typeof(T);
            await NavigateToViewModelAsync(vmType, parameter, animated);
        }

        /// <summary>
        /// Navigates to the <see cref="INavigable"/> ViewModel of <see cref="Type"/>.
        /// </summary>
        /// <param name="vmType">The type of the ViewModel to navigate to.</param>
        /// <param name="animated">Whether or not the page transition should be animated.</param>
        public async Task NavigateToViewModelAsync(Type vmType, bool animated = true)
        {
            await _vmAssociationsLock.WaitAsync();            
            {
                Type destPage = null;
                if (!_viewmodelPageAssociations.TryGetValue(vmType, out destPage))
                {
                    throw new ArgumentException(
                        $"No such ViewModel: {vmType}. Did you forget to call NavigationService.Configure?",
                        nameof(vmType));
                }

                await NavigateToAsync(destPage, animated);
            }
            _vmAssociationsLock.Release();
        }

        /// <summary>
        /// Navigates to the <see cref="INavigable"/> ViewModel of <see cref="Type"/>.
        /// </summary>
        /// <param name="vmType">The type of the ViewModel to navigate to.</param>
        /// /// <param name="parameter">An optional parameter to send to the ViewModel.</param>
        /// <param name="animated">Whether or not the page transition should be animated.</param>
        public async Task NavigateToViewModelAsync(Type vmType, object parameter, bool animated = true)
        {
            await _vmAssociationsLock.WaitAsync();
            {
                Type destPage = null;
                if (!_viewmodelPageAssociations.TryGetValue(vmType, out destPage))
                {
                    throw new ArgumentException(
                        $"No such ViewModel: {vmType}. Did you forget to call NavigationService.Configure?",
                        nameof(vmType));
                }

                await NavigateToAsync(destPage, parameter, animated);
            }
            _vmAssociationsLock.Release();
        }

        private async Task NavigateToAsync(Type pageKey, object parameter, bool animated = true)
        {            
            await _knownPagesLock.WaitAsync();
            {
                if (_knownPageTypes.Contains(pageKey))
                {
                    var type = pageKey;

                    if (parameter == null)
                    {
                        throw new ArgumentException("Navigational parameters cannot be null. Did you mean to use the method overload without a navigational parameter?");
                    }

                    ConstructorInfo constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(
                            c =>
                            {
                                var p = c.GetParameters();
                                return p.Length == 1 && p[0].ParameterType == parameter.GetType();
                            });

                    object[] parameters =
                    {
                        parameter
                    };

                    if (constructor == null)
                    {
                        throw new InvalidOperationException("No suitable constructor found for page " + pageKey);
                    }

                    var page = constructor.Invoke(parameters) as Page;
                    await _navHost.NavigateToAsync(page, animated);
                }
                else
                {
                    throw new ArgumentException(
                        $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?",
                        nameof(pageKey));
                }
            }
            _knownPagesLock.Release();
        }

        private async Task NavigateToAsync(Type pageKey, bool animated = true)
        {
            await _knownPagesLock.WaitAsync();
            {
                if (_knownPageTypes.Contains(pageKey))
                {
                    var type = pageKey;

                    ConstructorInfo constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(c => !c.GetParameters().Any());

                    object[] parameters = { };

                    if (constructor == null)
                    {
                        throw new InvalidOperationException("No suitable constructor found for page " + pageKey);
                    }

                    var page = constructor.Invoke(parameters) as Page;
                    await _navHost.NavigateToAsync(page, animated);
                }
                else
                {
                    throw new ArgumentException(
                        $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?",
                        nameof(pageKey));
                }
            }
            _knownPagesLock.Release();
        }

        /// <summary>
        /// Associates ViewModel <see cref="Type"/>s with <see cref="Type"/>s of <see cref="Page"/>s.
        /// </summary>
        /// <param name="pageType">The Page type to associate with a viewmodel.</param>
        /// <param name="vmType">The viewmodel to associate the page with.</param>
        public NavigationService Configure(Type vmType, Type pageType)
        {
            lock (_knownPageTypes)
            {
                if (!_knownPageTypes.Contains(pageType))
                {
                    _knownPageTypes.Add(pageType);
                }
            }

            if (vmType != null)
            {
                lock (_viewmodelPageAssociations)
                {
                    if (!_viewmodelPageAssociations.ContainsKey(vmType))
                    {
                        _viewmodelPageAssociations.Add(vmType, pageType);
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// Removes all <see cref="Page"/>s from the NavigationStack except for the currently-displayed page.
        /// </summary>
        public void ClearBackStack()
        {
            _navHost.ClearBackStack();
        }
    }
}
