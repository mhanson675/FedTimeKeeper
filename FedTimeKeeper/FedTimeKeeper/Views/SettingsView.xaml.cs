using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FedTimeKeeper.Utilities.Constants;

namespace FedTimeKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            LoadLeaveAccrualData();
            LoadCurrentSettings();
        }

        private void LoadLeaveAccrualData()
        {
            var accrualRates = new List<int> { 4, 6, 8 };
            LeaveAccrualPicker.ItemsSource = accrualRates;
        }

        private void LoadCurrentSettings()
        {
            LoadStartingBalances();
            LoadCurrentLeaveRate();
            LoadFirstDate();
            LoadLastDate();
        }

        private void LoadStartingBalances()
        {
            var annualLeaveBalance = double.Parse(Preferences.Get(annualLeaveKey, "0.00"));
            var sickLeaveBalance = double.Parse(Preferences.Get(sickLeaveKey, "0.00"));
            var timeOffBalance = double.Parse(Preferences.Get(timeOffKey, "0.00"));

            StartAnnualLeaveBalanceEntry.Text = annualLeaveBalance.ToString("F");
            StartSickLeaveBalanceEntry.Text = sickLeaveBalance.ToString("F");
            StartTimeOffBalanceEntry.Text = timeOffBalance.ToString("F");
        }

        private void LoadCurrentLeaveRate()
        {
            var leaveRate = int.Parse(Preferences.Get(leaveRateKey, "8"));
            var index = LeaveAccrualPicker.ItemsSource.IndexOf(leaveRate);
            LeaveAccrualPicker.SelectedIndex = index;
        }

        private void LoadFirstDate()
        {
            var firstDate = Preferences.Get(firstDateKey, new DateTime(DateTime.Now.Year, 01, 01));
            FirstPayPeriodDatePicker.Date = firstDate;
        }

        private void LoadLastDate()
        {
            var lastDate = Preferences.Get(lastDateKey, DateTime.Now);
            LastPayPeriodDatePicker.Date = lastDate;
        }

        private void LeaveAccrualPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSetting = ((Picker)sender).SelectedIndex;
            if (selectedSetting != -1)
            {
                var chosenRate = LeaveAccrualPicker.ItemsSource[selectedSetting];

                Preferences.Set(leaveRateKey, chosenRate.ToString());
            }
        }

        private void FirstPayPeriodDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var selectedDate = ((DatePicker)sender).Date;

            Preferences.Set(firstDateKey, selectedDate);
        }

        private void LastPayPeriodDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var selectedDate = ((DatePicker)sender).Date;

            Preferences.Set(lastDateKey, selectedDate);
        }

        private void StartAnnualLeaveBalanceEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = ((Entry)sender).Text;
            double.TryParse(entry, out double startingBalance);

            Preferences.Set(annualLeaveKey, startingBalance);
        }

        private void StartSickLeaveBalanceEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = ((Entry)sender).Text;
            double.TryParse(entry, out double startingBalance);

            Preferences.Set(sickLeaveKey, startingBalance);
        }

        private void StartTimeOffBalanceEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = ((Entry)sender).Text;
            double.TryParse(entry, out double startingBalance);

            Preferences.Set(timeOffKey, startingBalance);
        }
    }
}