using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Xalami.Core.Mvvm;
using Xalami.Core.Services;

namespace Xalami.Core.ViewModels
{
    public class SecondaryViewModel : NavigableViewModelBase
    {
        private string _passedString;
        public string PassedString
        {
            get => _passedString;
            set => Set(ref _passedString, value);
        }

        public RelayCommand GoBackCommand { get; set; }

        public SecondaryViewModel(INavigationService navService) : base(navService)
        {
            GoBackCommand = new RelayCommand(GoBack);
        }

        public override Task Activated(NavigationType navType)
        {
            PassedString = (string)Parameter;
            return Task.CompletedTask;
        }

        private void GoBack()
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBackAsync(true);
            }
        }
    }
}
