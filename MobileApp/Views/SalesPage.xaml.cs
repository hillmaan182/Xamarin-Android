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
        private int? vendorId;
        public SalesPage()
        {
            InitializeComponent();
            this.BindingContext = new SalesViewModel(true, false);
            vendorId = ((App)App.Current).vendorID;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SoldChart.IsVisible = false;
            //SalesChart3.IsVisible = false;
            getChart();
            getSoldChart(); 
        }

        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

        private void getChart()
        {
            int lastMonth = DateTime.Now.Month - 1;
            int nowMonth = DateTime.Now.Month;
            int nowYear = DateTime.Now.Year;

            var db = getContext();
            var before = from q in db.Transaction
                         where q.VendorID == vendorId && q.Status == "New Order" && q.BuyDate.Month == lastMonth && q.BuyDate.Year == nowYear
                         group q by new { q.Status }
                         into g
                         select new { Total = g.Count() == 0 ? 0 : g.Count() };


            if (before.ToList().Count() == 0)
            {
                var now = from q in db.Transaction
                          where q.VendorID == vendorId && q.Status == "New Order" && q.BuyDate.Month == nowMonth && q.BuyDate.Year == nowYear
                          group q by new { q.VendorID }
                        into g
                          select new { Total = g.Count(), Percent = g.Count() / 100 };

                lstNewOrder.ItemsSource = now.ToList();


                List<Entry> entries1 = new List<Entry>();
                entries1.Add(new Entry(0) { Color = SKColor.Parse("#FF1943"), Label = DateTime.Now.Month.ToString(), ValueLabel = "0" });
                foreach (var x in now.ToList())
                {
                    entries1.Add(new Entry(x.Total) { Color = SKColor.Parse("#FF1943"), Label = "Oct", ValueLabel = x.Total.ToString() });
                }

                NewOrderChart.Chart = new LineChart() { Entries = entries1 };
            }
            else
            {
                var now = from q in db.Transaction
                          where q.VendorID == vendorId && q.Status == "New Order" && q.BuyDate.Month == nowMonth && q.BuyDate.Year == nowYear
                          group q by new { q.VendorID }
                        into g
                          select new { Total = g.Count(), Percent = g.Count() / before.FirstOrDefault().Total };

                lstNewOrder.ItemsSource = now.ToList();

                List<Entry> entries1 = new List<Entry>();
                entries1.Add(new Entry(0) { Color = SKColor.Parse("#FF1943"), Label = DateTime.Now.Month.ToString(), ValueLabel = "0" });
                foreach (var x in now.ToList())
                {
                    entries1.Add(new Entry(x.Total) { Color = SKColor.Parse("#FF1943"), Label = "Oct", ValueLabel = x.Total.ToString() });
                }

                NewOrderChart.Chart = new LineChart() { Entries = entries1 };
            }

            //var res = db.Transaction.Where(x=> x.VendorID == vendorId && x.Status == "New Order").Select(x => new { x. }).ToList();
        }

        private void getSoldChart()
        {
            int lastMonth = DateTime.Now.Month - 1;
            int nowMonth = DateTime.Now.Month;
            int nowYear = DateTime.Now.Year;

            var db = getContext();
            var before = from q in db.Transaction
                         where q.VendorID == vendorId && q.Status == "Finished" && q.BuyDate.Month == lastMonth && q.BuyDate.Year == nowYear
                         group q by new { q.Status }
                         into g
                         select new { Total = g.Count() == 0 ? 0 : g.Count() };


            if (before.ToList().Count() == 0)
            {
                var now = from q in db.Transaction
                          where q.VendorID == vendorId && q.Status == "Finished" && q.BuyDate.Month == nowMonth && q.BuyDate.Year == nowYear
                          group q by new { q.VendorID }
                        into g
                          select new { Total = g.Count(), Percent = g.Count() / 100 };

                lstSold.ItemsSource = now.ToList();


                List<Entry> entries2 = new List<Entry>();
                entries2.Add(new Entry(0) { Color = SKColor.Parse("#FF1943"), Label = DateTime.Now.Month.ToString(), ValueLabel = "0" });
                foreach (var x in now.ToList())
                {
                    entries2.Add(new Entry(x.Total) { Color = SKColor.Parse("#FF1943"), Label = "Oct", ValueLabel = x.Total.ToString() });
                }

                SoldChart.Chart = new LineChart() { Entries = entries2 };
            }
            else
            {
                var now = from q in db.Transaction
                          where q.VendorID == vendorId && q.Status == "Finished" && q.BuyDate.Month == nowMonth && q.BuyDate.Year == nowYear
                          group q by new { q.VendorID }
                        into g
                          select new { Total = g.Count(), Percent = g.Count() / before.FirstOrDefault().Total };

                lstSold.ItemsSource = now.ToList();

                List<Entry> entries2 = new List<Entry>();
                entries2.Add(new Entry(0) { Color = SKColor.Parse("#FF1943"), Label = DateTime.Now.Month.ToString(), ValueLabel = "0" });
                foreach (var x in now.ToList())
                {
                    entries2.Add(new Entry(x.Total) { Color = SKColor.Parse("#FF1943"), Label = "Oct", ValueLabel = x.Total.ToString() });
                }

                SoldChart.Chart = new LineChart() { Entries = entries2 };
            }
        }

        //public void getSeenChart()
        //{
        //    int lastMonth = DateTime.Now.Month - 1;
        //    int nowMonth = DateTime.Now.Month;
        //    int nowYear = DateTime.Now.Year;

        //    var db = getContext();
        //    var before = from q in db.Product
        //                 where q.VendorID == vendorId  q.BuyDate.Month == lastMonth && q.BuyDate.Year == nowYear
        //                 group q by new { q.Status }
        //                 into g
        //                 select new { Total = g.Count() == 0 ? 0 : g.Count() };
        //}
        private void incomeClicked(object sender, EventArgs e)
        {
            this.BindingContext = new SalesViewModel(true, false);
        }

        private void outcomeClicked(object sender, EventArgs e)
        {
            this.BindingContext = new SalesViewModel(false, true);
        }

        //private void seenFrame_Tapped(object sender, EventArgs e)
        //{
        //    SalesChart1.IsVisible = true;
        //    SalesChart2.IsVisible = false;
        //    //SalesChart3.IsVisible = false;
        //}

        private void orderFrame_Tapped(object sender, EventArgs e)
        {
            NewOrderChart.IsVisible = true;
            SoldChart.IsVisible = false;
            //SalesChart3.IsVisible = false;
        }

        private void soldFrame_Tapped(object sender, EventArgs e)
        {
            NewOrderChart.IsVisible = false;
            SoldChart.IsVisible = true;
            //SalesChart3.IsVisible = true;
        }
    }
}