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

namespace MobileApp.CardView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardIncome : ContentView
    {
        public CardIncome()
        {
            InitializeComponent();
            showChart();
        }

        public void showChart()
        {
            List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Color=SKColor.Parse("#FF1943"),
                Label ="January",
                ValueLabel = "200"
            },
            new Entry(400)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "March",
                ValueLabel = "400"
            },
            new Entry(-100)
            {
                Color =  SKColor.Parse("#00CED1"),
                Label = "Octobar",
                ValueLabel = "-100"
            },
            };

            SalesChart.Chart = new LineChart() { Entries = entries };
        }
    }
}