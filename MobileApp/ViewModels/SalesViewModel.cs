using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MobileApp.ViewModels
{
   
    public class SalesViewModel : BaseViewModel
    {
        //private bool isIncome;
        //private bool isOutcome;

        //public bool IsVendor { get => isIncome; set => SetProperty(ref isIncome, value); }
        //public bool IsUser { get => isOutcome; set => SetProperty(ref isOutcome, value); }
        public SalesViewModel(bool income , bool outcome)
        {
            if (income == true)
            {
                IsIncome = true;
                IsOutcome = false;
                BorderIncome = Color.Green;
                BorderOutcome = Color.Beige;

            } else if(outcome == true)
            {
                IsIncome = false;
                IsOutcome = true;
                BorderIncome = Color.Beige;
                BorderOutcome = Color.Red;
            }
        }
    }
}
