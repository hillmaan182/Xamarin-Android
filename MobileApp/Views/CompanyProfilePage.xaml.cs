using MobileApp.Services;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Param", "Param")]
    public partial class CompanyProfilePage : ContentPage
    {
        VendorService vs;
        int? id;

        string param;
        public string Param
        {
            set
            {
                param = Uri.UnescapeDataString(value ?? string.Empty);
            }
            get
            {
                return param;
            }
        }

        public CompanyProfilePage()
        {
            id = ((App)App.Current).vendorID;
            InitializeComponent();
            vs = new VendorService();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            int? vendorId;
            if (id != null)
            {
                vendorId = id;
            }else
            {
                vendorId = Convert.ToInt32(param);
            }
            showVendorProfile(vendorId);
        }
        private void showVendorProfile(int? idvendor)
        {
            var res = vs.GetVendorById(idvendor).Result;
            //lvVendor.ItemsSource = res;
            string vname = "PT Aquamarine Divindo Inspection";
            string pname = "PT Indrayasa Migasa";
            var db = getContext();
            var query = from q in db.Vendor
                        where q.ID == idvendor
                        select new { q.ID, q.VendorName, q.VendorAddress, q.VendorEmail, q.VendorPhone, q.VendorAbout , q.VendorMission ,
                            img1 = q.VendorName == vname ? "icon_affiliates1.png" : q.VendorName == pname ? "icon_affil1.png" : null,
                            img2 = q.VendorName == vname ? "icon_affiliates2.png" : q.VendorName == pname ? "icon_affil2.png" : null,
                            img3 = q.VendorName == vname ? "icon_affiliates3.png" : q.VendorName == pname ? "icon_affil2.png" : null,
                            img4 = q.VendorName == vname ? "icon_certificate1.jpg" : q.VendorName == pname ? "icon_exp1.jpg" : null,
                            img5 = q.VendorName == vname ? "icon_certificate2.jpg" : q.VendorName == pname ? "icon_exp2.jpg" : null,
                            img6 = q.VendorName == vname ? "icon_certificate3.jpg" : q.VendorName == pname ? "icon_exp3.jpg" : null,
                            img7 = q.VendorName == vname ? "icon_profservice1.jpg" : q.VendorName == pname ? "icon_supp1.jpg" : null,
                            img8 = q.VendorName == vname ? "icon_profservice2.jpg" : q.VendorName == pname ? "icon_supp2.jpg" : null,
                            img9 = q.VendorName == vname ? "icon_profservice1.jpg" : q.VendorName == pname ? "icon_supp3.jpg" : null,
                            img10 = q.VendorName == vname ? "icon_affiliates3.png" : null,
                            img11 = q.VendorName == vname ? "icon_affiliates3.png" : null,
                            img12 = q.VendorName == vname ? "icon_affiliates3.png" : null,
                        };

            lvVendor.ItemsSource = query.ToList();

        }
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }

    }
}