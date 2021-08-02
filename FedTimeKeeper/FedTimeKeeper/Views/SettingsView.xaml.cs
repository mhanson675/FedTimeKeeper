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
            LoadStartingBalance();
            LoadLeaveCurrentLeaveRate();
            LoadFirstDate();
            LoadLastDate();
        }

        private void LoadStartingBalance()
        {
            var startingBalance = double.Parse(Preferences.Get(startingLeaveKey, "0.0"));
            StartLeaveBalanceEntry.Text = startingBalance.ToString();
        }

        private void LoadLeaveCurrentLeaveRate()
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

        private void StartLeaveBalanceEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = ((Entry)sender).Text;
            double.TryParse(entry, out double startingBalance);

            Preferences.Set(startingLeaveKey, startingBalance);
        }
    }
}