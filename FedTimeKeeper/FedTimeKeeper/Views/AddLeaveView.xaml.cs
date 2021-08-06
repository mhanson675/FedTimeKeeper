using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.ViewModels;
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
    public partial class AddLeaveView : ContentPage
    {
        public AddLeaveView()
        {
            InitializeComponent();

            BindingContext = App.GetViewModel<AddLeaveViewModel>();
            //BindingContext = App.GetViewModel<AddLeaveViewModel>();
        }

        //private void SaveButton_Clicked(object sender, EventArgs e)
        //{
        //    var newLeave = GetEnteredLeave();

        //    scheduledLeaveService.SaveLeave(newLeave);

        //    DisplayAlert("Leave Saved", $"{leaveType} leave has been scheduled.", "Ok");
        //    Navigation.PopToRootAsync();
        //}

        //private ScheduledLeave GetEnteredLeave()
        //{
        //    var newLeave = new ScheduledLeave();

        //    newLeave.StartDate = StartDatePicker.Date;
        //    newLeave.EndDate = EndDatePicker.Date;

        //    if (double.TryParse(HoursTakenEntry.Text, out double hours))
        //    {
        //        newLeave.HoursTaken = hours;
        //    }

        //    switch (leaveType)
        //    {
        //        case ANNUAL:
        //            newLeave.Type = LeaveType.Annual;
        //            break;
        //        case SICK:
        //            newLeave.Type = LeaveType.Sick;
        //            break;
        //        case TIMEOFF:
        //            newLeave.Type = LeaveType.Timeoff;
        //            break;
        //    }

        //    return newLeave;
        //}

        //private void LeaveTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var selectedLeaveType = ((Picker)sender).SelectedIndex;

        //    if (selectedLeaveType != -1)
        //    {
        //        leaveType = (string)LeaveTypePicker.ItemsSource[selectedLeaveType];
        //    }

        //    SaveButton.IsEnabled = CheckCanSave();
        //}

        //private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    SaveButton.IsEnabled = CheckCanSave();
        //}

        //private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    SaveButton.IsEnabled = CheckCanSave();
        //}

        //private void HoursTakenEntry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    SaveButton.IsEnabled = CheckCanSave();
        //}

        //private bool CheckCanSave()
        //{
        //    return LeaveTypePicked() && HoursRequestedEntered() && ValidDateRange();
        //}

        //private bool LeaveTypePicked()
        //{
        //    return LeaveTypePicker.SelectedIndex != -1;
        //}

        //private bool HoursRequestedEntered()
        //{
        //    return HoursTakenEntry.Text?.Length > 0;
        //}

        //private bool ValidDateRange()
        //{
        //    return StartDatePicker.Date <= EndDatePicker.Date;
        //}
    }
}