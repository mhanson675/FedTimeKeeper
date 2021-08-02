using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLeavePageView : ContentPage
    {
        private string leaveType;

        public AddLeavePageView()
        {
            InitializeComponent();

            LoadLeaveTypes();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Leave Saved", $"{leaveType} leave has been scheduled.", "Ok");
            Navigation.PopToRootAsync();
        }

        private void LoadLeaveTypes()
        {
            var leaveList = new List<String> { "Annual", "Time-off Award", "Sick" };
            LeaveTypePicker.ItemsSource = leaveList;
        }

        private void LeaveTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedLeaveType = ((Picker)sender).SelectedIndex;

            if (selectedLeaveType != -1)
            {
                leaveType = (string)LeaveTypePicker.ItemsSource[selectedLeaveType];
            }

            SaveButton.IsEnabled = CheckCanSave();
        }

        private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            SaveButton.IsEnabled = CheckCanSave();
        }

        private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            SaveButton.IsEnabled = CheckCanSave();
        }

        private void HoursTakenEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = CheckCanSave();
        }

        private bool CheckCanSave()
        {
            if (LeaveTypePicked() && HoursRequestedEntered() && ValidDateRange())
            {
                return true;
            }

            return false;
        }

        private bool LeaveTypePicked()
        {
            return LeaveTypePicker.SelectedIndex != -1;
        }

        private bool HoursRequestedEntered()
        {
            return HoursTakenEntry.Text?.Length > 0;
        }

        private bool ValidDateRange()
        {
            return StartDatePicker.Date <= EndDatePicker.Date;
        }
    }
}