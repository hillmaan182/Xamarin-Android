using MobileApp.Models;
using MobileApp.Services;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserService user;
        VendorService vendor;
        ProductService ps;
        ShipyardService ss;
        public LoginPage()
        {
            user = new UserService();
            vendor = new VendorService();
            ps = new ProductService();
            ss = new ShipyardService();

            addInitial();
            //addInitial2();

            InitializeComponent();
            //this.BindingContext = new LoginViewModel();
        }
        public LoginPage(User obj)
        {
            InitializeComponent();
            user = new UserService();
            if (obj != null)
            {
                txtUsername.Text = obj.Password;
                txtPassword.Text = obj.Password;
            }
        }
        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//RegisterPage");
            ((App)App.Current).userLoggedIn = "";
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            int val = user.LoginUser(txtUsername.Text, txtPassword.Text).Result;
            
            if (val == 1)
            {
                int? vendor = user.GetVendorUser(txtUsername.Text, txtPassword.Text);
                var res = user.GetDataUserByEmail(txtUsername.Text);

                ((App)App.Current).vendorName = res.FirstOrDefault().Username;

                ((App)App.Current).vendorID = vendor;
                //((App)App.Current).userLoggedIn = txtUsername.Text;
                ((App)App.Current).userLoggedIn = res.FirstOrDefault().Username;
                ((App)App.Current).IsUserLoggedIn = true;
                if (res.FirstOrDefault().Role == "Vendor")
                {
                    MessagingCenter.Send<LoginPage>(this, (res.FirstOrDefault().Role == "Vendor") ? "vendor" : "user");
                    int id = user.GetUserId(txtUsername.Text, txtPassword.Text);
                    ((App)App.Current).userID = id;
                    await Shell.Current.GoToAsync($"//{nameof(VendorHomePage)}");
                } else if (res.FirstOrDefault().Role == "Shipyard")
                {
                    MessagingCenter.Send<LoginPage>(this, (res.FirstOrDefault().Role == "Shipyard") ? "vendor" : "user");
                    await Shell.Current.GoToAsync($"//{nameof(VendorHomePage)}");
                } else
                {

                    MessagingCenter.Send<LoginPage>(this, (res.FirstOrDefault().Role == "User") ? "user" : "vendor");
                    int id = user.GetUserId(txtUsername.Text, txtPassword.Text);

                    var resShipyard = ss.GetShipyardByUserId(id).Result;
                    int shipyardId = resShipyard.FirstOrDefault().ID;


                    ((App)App.Current).userID = id;
                    ((App)App.Current).shipyardID = shipyardId;
                    ((App)App.Current).userName = resShipyard.FirstOrDefault().ShipyardName;
                    await Shell.Current.GoToAsync($"//{nameof(UserHomePage)}");
                }

                
            } else if (val == 2){
                int id = user.GetUserId(txtUsername.Text, txtPassword.Text);
                ((App)App.Current).userID = id;
                await Shell.Current.GoToAsync($"//{nameof(VerificationPage)}");
            }
            else
            {
                await DisplayAlert("Login Failed!", "username / password incorrect", "OK");
            }
        }

        public void addInitial()
        {

            Vendor obj = new Vendor();
            obj.VendorName = "Vendor Product A";
            obj.VendorAddress = "Jalan A no 1";
            obj.VendorPhone = "02112345678";
            obj.VendorEmail = "vendorA@gmail.com";
            obj.Type = "Product";
            obj.VendorAbout = "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate";
            obj.VendorMission = "Happy Live Laugh";
            vendor.InsertVendor(obj);

            //
            Products objP = new Products();
            objP.ProductName = "Product 1";
            objP.ProductPrice = 5000;
            objP.ProductSisa = 10;
            objP.ProductCategory = "Product";
            objP.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objP.ProductSpecification = "x:10 y:5";
            objP.VendorID = obj.ID;
            objP.ProductSeen = 0;
            ps.InsertProduct(objP);
            

            Products objP2 = new Products();
            objP2.ProductName = "Product 2";
            objP2.ProductPrice = 5000;
            objP2.ProductSisa = 10;
            objP2.ProductCategory = "Product";
            objP2.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objP2.ProductSpecification = "x:10 y:5";
            objP2.ProductSeen = 0;
            objP2.VendorID = obj.ID;
            ps.InsertProduct(objP2);

            Products objP3 = new Products();
            objP3.ProductName = "Product 3";
            objP3.ProductPrice = 5000;
            objP3.ProductSisa = 10;
            objP3.ProductCategory = "Product";
            objP3.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objP3.ProductSpecification = "x:10 y:5";
            objP3.ProductSeen = 0;
            objP3.VendorID = obj.ID;
            ps.InsertProduct(objP3);
            //

            User objUser = new User();
            objUser.Username = "vendorp1";
            objUser.Email = "vendorp1@gmail.com";
            objUser.Password = "vendorp1";
            objUser.Role = "Vendor";
            objUser.IsVerified = true;
            objUser.VerifiedCode = "1234";
            objUser.VendorID = obj.ID;
            //objUser.Vendor = obj;
            user.InsertUser(objUser);

            Vendor obj2 = new Vendor();
            obj2.VendorName = "Vendor Service A";
            obj2.VendorAddress = "Jalan A no 1";
            obj2.VendorPhone = "02112345678";
            obj2.VendorEmail = "vendorB@gmail.com";
            obj2.Type = "Service";
            obj2.VendorAbout = "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate";
            obj2.VendorMission = "Happy Live Laugh";
            vendor.InsertVendor(obj2);

            //
            Products objS = new Products();
            objS.ProductName = "Service 1";
            objS.ProductPrice = 5000;
            objS.ProductSisa = 10;
            objS.ProductCategory = "Service";
            objS.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objS.ProductSpecification = "x:10 y:5";
            objS.ProductSeen = 0;
            objS.VendorID = obj2.ID;
            ps.InsertProduct(objS);


            Products objS2 = new Products();
            objS2.ProductName = "Service 2";
            objS2.ProductPrice = 5000;
            objS2.ProductSisa = 10;
            objS2.ProductCategory = "Service";
            objS2.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objS2.ProductSpecification = "x:10 y:5";
            objS2.ProductSeen = 0;
            objS2.VendorID = obj2.ID;
            ps.InsertProduct(objS2);

            Products objS3 = new Products();
            objS3.ProductName = "Service 3";
            objS3.ProductPrice = 5000;
            objS3.ProductSisa = 10;
            objS3.ProductCategory = "Service";
            objS3.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objS3.ProductSpecification = "x:10 y:5";
            objS3.ProductSeen = 0;
            objS3.VendorID = obj2.ID;
            ps.InsertProduct(objS3);
            //

            User objUser2 = new User();
            objUser2.Username = "vendors1";
            objUser2.Email = "vendors1@gmail.com";
            objUser2.Password = "vendors1";
            objUser2.Role = "Vendor";
            objUser2.IsVerified = true;
            objUser2.VerifiedCode = "1234";
            //objUser2.Vendor = obj2;
            objUser2.VendorID = obj2.ID;
            user.InsertUser(objUser2);

            User objUser3 = new User();
            objUser3.Username = "user";
            objUser3.Email = "user@gmail.com";
            objUser3.Password = "user";
            objUser3.Role = "User";
            objUser3.IsVerified = true;
            objUser3.VerifiedCode = "1234";
            user.InsertUser(objUser3);

            Shipyard objShip1 = new Shipyard();
            objShip1.ShipyardName = "Shipyard A";
            objShip1.ShipyardAddress = "Ship St no 32";
            objShip1.ShipyardPhone = "02112345678";
            objShip1.ShipyardEmail = "shipA@gmail.com";
            objShip1.ShipyardAbout = "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate";
            objShip1.ShipyardMission = "Happy Live Laugh";
            objShip1.UserID = objUser3.ID;
            ss.InsertShipyard(objShip1);
            
        }

        public void addInitial2()
        {
            Vendor obj = new Vendor();
            obj.VendorName = "Vendor Product B";
            obj.VendorAddress = "Jalan A no 1";
            obj.VendorPhone = "02112345678";
            obj.VendorEmail = "vendorA@gmail.com";
            obj.Type = "Product";
            vendor.InsertVendor(obj);

            //
            Products objP = new Products();
            objP.ProductName = "Product B1";
            objP.ProductPrice = 5000;
            objP.ProductSisa = 10;
            objP.ProductCategory = "Product";
            objP.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objP.ProductSpecification = "x:10 y:5";
            objP.ProductSeen = 0;
            objP.VendorID = obj.ID;
            ps.InsertProduct(objP);


            Products objP2 = new Products();
            objP2.ProductName = "Product B2";
            objP2.ProductPrice = 5000;
            objP2.ProductSisa = 10;
            objP2.ProductCategory = "Product";
            objP2.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objP2.ProductSpecification = "x:10 y:5";
            objP2.ProductSeen = 0;
            objP2.VendorID = obj.ID;
            ps.InsertProduct(objP2);

            Products objP3 = new Products();
            objP3.ProductName = "Product B3";
            objP3.ProductPrice = 5000;
            objP3.ProductSisa = 10;
            objP3.ProductCategory = "Product";
            objP3.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objP3.ProductSpecification = "x:10 y:5";
            objP3.ProductSeen = 0;
            objP3.VendorID = obj.ID;
            ps.InsertProduct(objP3);
            //

            User objUser = new User();
            objUser.Username = "vendorp2";
            objUser.Email = "vendorp2@gmail.com";
            objUser.Password = "vendorp2";
            objUser.Role = "Vendor";
            objUser.IsVerified = true;
            objUser.VerifiedCode = "1234";
            objUser.VendorID = obj.ID;
            //objUser.Vendor = obj;
            user.InsertUser(objUser);

            Vendor obj2 = new Vendor();
            obj2.VendorName = "Vendor Service B";
            obj2.VendorAddress = "Jalan A no 1";
            obj2.VendorPhone = "02112345678";
            obj2.VendorEmail = "vendorB2@gmail.com";
            obj2.Type = "Service";
            vendor.InsertVendor(obj2);

            //
            Products objS = new Products();
            objS.ProductName = "Service B1";
            objS.ProductPrice = 5000;
            objS.ProductSisa = 10;
            objS.ProductCategory = "Service";
            objS.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objS.ProductSpecification = "x:10 y:5";
            objS.ProductSeen = 0;
            objS.VendorID = obj2.ID;
            ps.InsertProduct(objS);

            Products objS2 = new Products();
            objS2.ProductName = "Service B2";
            objS2.ProductPrice = 5000;
            objS2.ProductSisa = 10;
            objS2.ProductCategory = "Service";
            objS2.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objS2.ProductSpecification = "x:10 y:5";
            objS2.ProductSeen = 0;
            objS2.VendorID = obj2.ID;
            ps.InsertProduct(objS2);

            Products objS3 = new Products();
            objS3.ProductName = "Service B3";
            objS3.ProductPrice = 5000;
            objS3.ProductSisa = 10;
            objS3.ProductCategory = "Service";
            objS3.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            objS3.ProductSpecification = "x:10 y:5";
            objS3.ProductSeen = 0;
            objS3.VendorID = obj2.ID;
            ps.InsertProduct(objS3);
            //

            User objUser2 = new User();
            objUser2.Username = "vendors2";
            objUser2.Email = "vendors2@gmail.com";
            objUser2.Password = "vendors2";
            objUser2.Role = "Vendor";
            objUser2.IsVerified = true;
            objUser2.VerifiedCode = "1234";
            //objUser2.Vendor = obj2;
            objUser2.VendorID = obj2.ID;
            user.InsertUser(objUser2);
        }

    }
}