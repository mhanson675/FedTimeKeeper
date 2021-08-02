using FedTimeKeeper.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationPageView : ContentPage
    {
        public NavigationPageView()
        {
            InitializeComponent();
        }

        private async void HomePageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageView());
        }

        private async void AddLeaveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddLeavePageView());
        }

        private async void ViewLeaveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LeaveInformationPageView());
        }

        private async void ScheduledLeaveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduledLeavePageView());
        }

        private async void AppSettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView());
        }
    }
}