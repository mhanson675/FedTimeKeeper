using FedTimeKeeper.Models;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.ViewModels
{
    public class LeaveInformationViewModel : BaseViewModel
    {
        private readonly ILeaveSummaryService leaveSummaryService;
        
        public LeaveSummaryModel Annual { get; set; }
        public LeaveSummaryModel UseOrLose { get; set; }
        public LeaveSummaryModel Sick { get; set; }
        public LeaveSummaryModel TimeOff { get; set; }

        public LeaveInformationViewModel(ILeaveSummaryService leaveSummaryService)
        {
            this.leaveSummaryService = leaveSummaryService;
            LoadData();
        }

        private void LoadData()
        {
            LoadAnnualLeaveSummary();
            LoadSickLeaveSummary();
            LoadUseOrLoseSummary();
            LoadTimeOffAwardSummary();
        }

        private void LoadTimeOffAwardSummary()
        {
            var timeOffSummary = leaveSummaryService.GetTimeOffAwardSummary();

            TimeOff = new LeaveSummaryModel
            {
                Type = timeOffSummary.Type.ToString(),
                BeginningBalance = timeOffSummary.BeginningBalance.ToString("F"),
                Earned = timeOffSummary.Earned.ToString("F"),
                Used = timeOffSummary.Used.ToString("F"),
                EndingBalance = timeOffSummary.EndingBalance.ToString("F")
            };
        }

        private void LoadUseOrLoseSummary()
        {
            var useOrLoseSummary = leaveSummaryService.GetUseOrLoseSummary();

            UseOrLose = new LeaveSummaryModel
            {
                Type = useOrLoseSummary.Type.ToString(),
                BeginningBalance = "N/A",
                Earned = "N/A",
                Used = "N/A",
                EndingBalance = useOrLoseSummary.EndingBalance.ToString("F")
            };
        }

        private void LoadSickLeaveSummary()
        {
            var sickSummary = leaveSummaryService.GetSickLeaveSummary();

            Sick = new LeaveSummaryModel
            {
                Type = sickSummary.Type.ToString(),
                BeginningBalance = sickSummary.BeginningBalance.ToString("F"),
                Earned = sickSummary.Earned.ToString("F"),
                Used = sickSummary.Used.ToString("F"),
                EndingBalance = sickSummary.EndingBalance.ToString("F")
            };
        }

        private void LoadAnnualLeaveSummary()
        {
            var annualSummary = leaveSummaryService.GetAnnualLeaveSummary();

            Annual = new LeaveSummaryModel
            {
                Type = annualSummary.Type.ToString(),
                BeginningBalance = annualSummary.BeginningBalance.ToString("F"),
                Earned = annualSummary.Earned.ToString("F"),
                Used = annualSummary.Used.ToString("F"),
                EndingBalance = annualSummary.EndingBalance.ToString("F")
            };
        }
    }
}
