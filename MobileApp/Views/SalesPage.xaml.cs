using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;

using Xamarin.Forms.Xaml;
using MobileApp.ViewModels;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesPage : ContentPage
    {

        public SalesPage()
        {
            InitializeComponent();
            this.BindingContext = new SalesViewModel(true, false);
        }

        private void incomeClicked(object sender, EventArgs e)
        {
            this.BindingContext = new SalesViewModel(true,false);
        }
        
        private void outcomeClicked(object sender, EventArgs e)
        {
            this.BindingContext = new SalesViewModel(false, true);
        }

    }
}