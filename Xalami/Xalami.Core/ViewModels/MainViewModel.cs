using System;
using GalaSoft.MvvmLight.Command;
using Xalami.Core.Mvvm;
using Xalami.Core.Services;

namespace Xalami.Core.ViewModels
{
    public class MainViewModel : NavigableViewModelBase
    {
        public string UserEnteredText { get; set; }

        public RelayCommand NavigateToSecondPageCommand { get; set; }

        public MainViewModel(INavigationService navService) : base(navService)
        {
            NavigateToSecondPageCommand = new RelayCommand(NavigateToSecondPage);
        }

        private void NavigateToSecondPage()
        {
            _navigationService.NavigateToViewModelAsync<SecondaryViewModel>(UserEnteredText);
        }
    }
}
