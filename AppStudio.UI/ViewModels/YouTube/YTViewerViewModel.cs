using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AppStudio.Data;
using AppStudio.Services;
using Microsoft.Phone.Shell;

namespace AppStudio.Data.YouTube
{
    public class YTViewerViewModel : BindableBase
    {
        private YTHelper _selectedItem;

        public bool IsGoToSourceVisible
        {
            get { return true; }
        }

        public bool NetworkAvailable
        {
            get { return InternetConnection.IsInternetAvailable(); }
        }

        public void GoToSource()
        {
            if (!String.IsNullOrEmpty(SelectedItem.ExternalUrl) && Uri.IsWellFormedUriString(SelectedItem.ExternalUrl, UriKind.Absolute))
            {
                NavigationServices.NavigateTo(new Uri(SelectedItem.ExternalUrl, UriKind.Absolute));
            }
        }

        public ICommand GoToSourceCommand
        {
            get { return new DelegateCommand(GoToSource); }
        }

        public YTHelper SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
    }
}
