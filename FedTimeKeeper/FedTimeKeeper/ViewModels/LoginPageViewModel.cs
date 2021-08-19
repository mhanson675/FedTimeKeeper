using FedTimeKeeper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FedTimeKeeper.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IUserService userService;
        private readonly INavigationService navigation;

        public ICommand LoginCommand => new Command(OnLoginCommand);
        public LoginPageViewModel(IUserService userService, INavigationService navigation)
        {
            this.userService = userService;
            this.navigation = navigation;
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private void OnLoginCommand()
        {
            if (IsValidLogin())
            {
                navigation.DisplayAlertMessage("Logging In", $"Logging in: {UserName}", "Ok");
                
            }
            else
            {
                navigation.DisplayAlertMessage("Login Failed", "Login Failed. The username or password was invalid.", "Ok");
            }
        }

        private bool IsValidLogin()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
