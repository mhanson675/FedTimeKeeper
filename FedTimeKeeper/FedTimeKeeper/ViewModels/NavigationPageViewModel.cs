﻿using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FedTimeKeeper.ViewModels
{
    public class NavigationPageViewModel : BaseViewModel
    {
        private readonly INavigationService navigation;

        public ICommand LoginButtonCommand => new Command(Login);
        public ICommand ScheduledLeaveCommand => new Command(ScheduledLeave);
        public ICommand ScheduleNewLeaveCommand => new Command(ScheduleNewLeave);
        public ICommand ViewLeaveInformationCommand => new Command(ViewLeaveInformation);
        public ICommand AppSettingsCommand => new Command(AppSettings);
        
        //TODO: Remove??
        public ICommand ClearPreferencesCommand => new Command(ClearPreferences);

        public NavigationPageViewModel(INavigationService navigation)
        {
            this.navigation = navigation;
        }

        private void Login(object obj)
        {
            navigation.NavigateTo(ViewNames.LoginPageView);
        }

        private void ScheduledLeave(object obj)
        {
            navigation.NavigateTo(ViewNames.ScheduledLeaveView);
        }

        private void ScheduleNewLeave(object obj)
        {
            navigation.NavigateTo(ViewNames.AddLeaveView);
        }

        private void ViewLeaveInformation(object obj)
        {
            navigation.NavigateTo(ViewNames.LeaveInformationView);
        }

        private void AppSettings(object obj)
        {
            navigation.NavigateTo(ViewNames.SettingsView);
        }

        //TODO: Remove
        private void ClearPreferences(object obj)
        {
            Preferences.Clear();
        }
    }
}
