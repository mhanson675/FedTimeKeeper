using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface INavigationService
    {
        Page MainPage { get; }

        void Configure(string key, Type pageType);
        void GoBack();
        void NavigateTo(string pageKey, object parameter = null);
        void DisplayAlert(string title, string message, string cancel);
    }
}
