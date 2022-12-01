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
        TransactionService ts;
        public LoginPage()
        {
            user = new UserService();
            vendor = new VendorService();
            ps = new ProductService();
            ss = new ShipyardService();
            ts = new TransactionService();

            addInitial();
            //addInitial2();
            insertDBFull();
            insertDBFullService();
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
                }
                else if (res.FirstOrDefault().Role == "Shipyard")
                {
                    MessagingCenter.Send<LoginPage>(this, (res.FirstOrDefault().Role == "Shipyard") ? "vendor" : "user");
                    await Shell.Current.GoToAsync($"//{nameof(VendorHomePage)}");
                }
                else
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


            }
            else if (val == 2)
            {
                int id = user.GetUserId(txtUsername.Text, txtPassword.Text);
                ((App)App.Current).userID = id;
                await Shell.Current.GoToAsync($"//{nameof(VerificationPage)}");
            }
            else
            {
                await DisplayAlert("Login Failed!", "username / password incorrect", "OK");
            }
        }

        public void insertDBFull()
        {
            int vendor1 = initVendor("PT. PCM Kabel Indonesia", "http://pcmcable.com", "(021) 59371728", "Jl. Karet Raya No.288, Mekar Jaya, Kec. Sepatan, Kabupaten Tangerang, Banten 15520", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor1.png");
            ///

            int product1 = initProduct(vendor1, "Kabel Marine 2x2.5 mm", 636000, 1000, "Panjang: 50 meter; Kelas Tegangan: 300v ~ 500v / 450V ~ 750V / 600V ~ 1000V; Bahan konduktor: Tembaga; Insulasi: Karet EPR; Materi Pelindung: Karet (Neoprene CPE EPR EPDM)", "Produk ini bisa digunakan sebagai kabel berkemampuan besar untuk tenaga listrik tegangan tinggi, untuk alat-lat mekanik pada pertambangan. produk ini bisa bekerja pada suatu lingkungan luar yang buruk dan juga bisa dipakai sebagai instalasi tetap dan pada alat-alat pergerakan mekanik lainnya.", "icon_product1.png");
            int product2 = initProduct(vendor1, "Kabel Marine 3x4 mm", 4050000, 1000, "Panjang: 50 meter; Kelas Tegangan: 300v ~ 500v / 450V ~ 750V / 600V ~ 1000V; Bahan konduktor: Tembaga; Insulasi: Karet EPR; Materi Pelindung: Karet (Neoprene CPE EPR EPDM)", "Produk ini bisa digunakan sebagai kabel berkemampuan besar untuk tenaga listrik tegangan tinggi, untuk alat-lat mekanik pada pertambangan. produk ini bisa bekerja pada suatu lingkungan luar yang buruk dan juga bisa dipakai sebagai instalasi tetap dan pada alat-alat pergerakan mekanik lainnya.", "icon_product2.png");
            initUser(vendor1, "PT. PCM Kabel Indonesia", "pcmcable@gmail.com");
            initTrans(vendor1, product1, "New Order");
            initTrans(vendor1, product2, "Finished");
            initNowTrans(vendor1, product1, "New Order");
            initNowTrans(vendor1, product1, "Finished");
            initNowTrans(vendor1, product2, "New Order");
            initNowTrans(vendor1, product2, "Finished");
            initNowTrans(vendor1, product1, "New Order");
            initNowTrans(vendor1, product1, "Finished");
            //
            int vendor2 = initVendor("CV. Dua Sahabat Group", "http://jendelapintukapal.com", "(031)9978 0784", "Pergudangan Tritan Taman Blok C 29-30, Sidoarjo", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor2.png");

            //
            int product3 = initProduct(vendor2, "Pintu Kapal Watertight Steel", 18000000, 50, "Kunci:Hydraulic Lock; Jenis: Sliding; Ukuran: 1600x800; Tebal plat: 4mm; Kelas:BKI Class", "-", "icon_product3.png");
            int product4 = initProduct(vendor2, "Fan Axial Marine", 9500000, 100, "Diameter: 18 inch/450 mm; Speed: 2800 rpm; Air Flow: 5000 CMH; ST Press: 500 PA; Voltage: 380 volt", "-", "icon_product4.png");

            initUser(vendor2, "CV. Dua Sahabat Group", "jendelapintukapal@gmail.com");
            initTrans(vendor1, product3, "New Order");
            initTrans(vendor1, product4, "Finished");
            //

            int vendor3 = initVendor("PT. Mobaru Diamond", "admin@diamondglass.co.id", "+62 21 85935924", "Jl. Kesatrian No.18, Sidokerto", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor3.png");
            //
            int product5 = initProduct(vendor3, "Pintu Kapal Kedap Alumunium", 15700000, 20, "Ukuran: 1600x800; Jenis: Revolve; Dengan kaca; Tekanan maksimum: 0.07-0.15MPa; Rubber gasket", "-", "icon_product5.png");
            initUser(vendor3, "PT. Mobaru Diamond", "diamondglass@gmail.com");
            initNowTrans(vendor3, product5, "New Order");
            initNowTrans(vendor3, product5, "Finished");
            //
            int vendor4 = initVendor("PT. Sentral Elektra Marindo", "eservices@sentramarindo.com", "(031) 3299568", "Jalan Teluk Buli no. 6, Surabaya 60165", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor4.png");
            //
            initProduct(vendor4, "GPS Furono GP 39", 9500000, 1000, "Layar: LCD 4.2 inch warna; Saluran: SBAS (WAAS/EGNOS/MSAS); Kapasitas Memori: 3000 kapal, 10000 poin, 100 rute", "Furuno GP39 dengan presisi tinggi - layar LCD dengan warna yang tajam. GPS GP-39 secara akurat mencari dan andal berkat 12-saluran penerima GPS dikombinasikan dengan SBAS (WAAS / EGNOS / MSAS) terintegrasi dalam mesin.GP-39 mesin memiliki banyak modus yang berbeda display (Menggambar jalan, jalan raya Layar, Screen berperahu, data yang maritim, dan modus Pemantauan kustom pengguna 2 Satellite) pada layar LCD warna 4.2\"", "icon_product 6.png");
            initProduct(vendor4, "Radar Simrad R2009", 25000000, 100, "Layar: 9\" multicolor; Keypad & Rotary dial; MARPA target tracking; Anti sea-clutter technology", " R2009 adalah unit kontrol khusus untuk sistem radar Simrad yang memiliki layar potret 9 inci yang terintegrasi.Aksesori keselamatan dan navigasi ini ideal untuk kapal penjelajah lepas pantai sampai kapal pemancing ikan.R2009 menawarkan opsi pemasangan flush atau bracket - mount serbaguna dan operasi berbasis keypad yang intuitif untuk kontrol yang andal di semua kondisi    ", "icon_product 7.png");
            initProduct(vendor4, "Navtex Receiver Samyung SNX 300", 15700000, 100, "Receiving frequency : 518KHz(English), 490KHz or 4209.5KHz(Local); Receiving method : F1B NAVTEX broadcast; Receiving sensitivity : 2㎶ e.m.f(50Ω), 4% error rate; Frequency accuracy :± 1Hz; Antenna impedance : 50Ω(Active), Power 9V (inner supply)", "NAVTEX dirancang untuk menerima semua layanan informasi keselamatan laut yang dibuat dengan penyiaran surat baik dalam bahasa Inggris dan dalam bahasa lokal tentang Frekuensi Internasional: 518KHZ dan pada Frekuensi Lokal: 490KHZ atau 4209.5KHZ.NAVTEX menyediakan peringatan navigasi seperti keselamatan terkait pergerakan gunung es, peringatan meteorologis seperti topan dan gelombang tinggi.", "icon_product8.png");
            initProduct(vendor4, "Simrad GC50/80/85 Gyro Compass", 268000000, 10, "Maintenance  free gyro; 30k hour calculated MTTF (>70k observed); Compact, Expanded, and Dual control versions; HSC compliant (GC85); NMEA/IEC, 24V Step, and Analog ROT outputs; No required annual servicing", "Simrad GC50/80/85 Gyro Compass sepenuhnya disetujui IMO untuk standard dan high speed craft; dengan opsi untuk mengkonfigurasi sistem dual dan expanded gyro compass. Menggunakan teknologi terbaru memungkinkan akurasi, stabilitas, dan memastikan operasi yang efektif di bawah kondisi yang paling keras. Kompas gyro Simrad GC80 adalah solusi ideal untuk sistem DP canggih yang dibutuhkan di industri lepas pantai dan aplikasi serupa lainnya.", "icon_product9.png");
            initUser(vendor4, "PT. Sentral Elektra Marindo", "sentramarindo@gmail.com");

            ///
            int vendor5 = initVendor("PT. Matesu Gotty Abadi", "matesugotty@rocketmail.com", "(031) 397 5591", "Jl. Tridarma, No. 3 Kav. E-4, Gresik, Jawa Timur, Indonesia", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor5.png");
            //
            initProduct(vendor5, "Gas Oksigen 7 liter", 1100000, 1000, "Diameter luar: 140mm; Kapasitas air: 7L; Tekanan kerja: 150bar; Tekanan Uji: 250bar; Standar: GB5099; Tinggi: 595mm; Berat: 9.9kg; ", "-", "icon_product10.png");
            initProduct(vendor5, "Gas CO2 25 Kg", 2000000, 1000, "Tinggi Keseluruhan 188 cm; Tinggi Cylinder 144 cm; Panjang Selang 4 m; Diameter Cylinder 20 cm; Lebar Handle 40 CM; Tinggi Penyangga 40 cm", "-", "icon_product11.png");
            initUser(vendor5, "PT. Matesu Gotty Abadi", "matesugotty@gmail.com");
            //
            int vendor6 = initVendor("CV. Guliga Corp", "yaneware@yahoo.com", "(031) 3292517", "Jl. Perak Timur No.266c, Perak Utara, Kec. Pabean Cantikan, Kota SBY", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor6.png");
            //
            initProduct(vendor6, "Life Jacket - Children Size", 875000, 10000, "Kapasitas airbag inflator: 16.5L;  Intracapsular gas terkompresi Berat: 33g; Waktu inflasi 5 detik; Daya apung: 16kg; Waktu retensi gas intrakapsular: 5 hari; Tekanan atmosfer balon: 0,2 kg; Berat maksimum orang: 120kg", "Inflatable life jacket CO2 33gr untuk dewasa. Otomatis mengembang jika sudah melewat batas leher dan juga di sediakan tarik manual untuk antisipasi jika tidak fungsi otomatisnya tidak berfungsi", "icon_product12.png");
            initProduct(vendor6, "Life Jacket - Adult Size", 550000, 10000, "Bahan: Nylon; Berat maksimum orang: 30kg; ", "-", "icon_product13.png");
            initUser(vendor6, "CV. Guliga Corp", "yaneware@gmail.com");
            //
            int vendor7 = initVendor("PT. Makmur Marina Metalindo", "-", "(021) 8809243", "Jl. Arief Rahman Hakim No.51, Klampis Ngasem, Kec. Sukolilo, Kota SBY", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor7.png");
            //
            initProduct(vendor7, "Alumunium Anode", 580000, 10000, "Dimensi (PxLxT) mm: 450x150x55; Berat: 10 kg", "Aluminium anode yang diproduksi sesuai spesifikasi untuk pemanfaatannya sebagai anode korban (sacricifal anodes) dalam lingkungan marine (DNV RP-B401); aluminium dengan kemurnian 99.80%.", "icon_product14.png");
            initProduct(vendor7, "Zinc Anode", 490000, 10000, "Dimensi (PxLxT) mm: 3000x150x18; Berat: 6.5 kg", "-", "icon_product15.png");
            initUser(vendor7, "PT. Makmur Marina Metalindo", "makmurmarina@gmail.com");
            //
            int vendor8 = initVendor("PT. Sinar Tunggal Jaya", "sinartunggalteknik@gmail.com", "(031) 3554 818", "Jl. Demak No.243, Dupak, Kec. Krembangan, Kota SBY", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor8.png");
            //
            initProduct(vendor8, "Tali Tambang Polypropylene (PP)", 390000, 1000, "Diameter: 8 mm; Panjang 1 roll: 220 m", "-", "icon_product16.png");
            initProduct(vendor8, "Tali Tambang Plastik", 350000, 1000, "Diameter: 8 mm; Panjang 1 roll: 200 m", "-", "icon_product17.png");
            initUser(vendor8, "PT. Sinar Tunggal Jaya", "sinartunggalteknik@gmail.com");
            //
            int vendor9 = initVendor("CV. Anugerah Teknik Mandiri", "csu.atm@gmail.com", " +62 878 411 72 411", "Perum Bumi Mondoroko Raya, Krajan, Watugede, Kec. Singosari, Kabupaten Malang", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor9.png");
            //
            initProduct(vendor9, "Panel Sistem Blower", 2200000, 100, "Dimensi (PxLxT) mm: 30x40x20; 1 phase; 2 blower", "Panel ini berfungsi sebagai alat untuk menghidupkan blower secara otomatis dengan sistem kontrol temperatur. Jika switch di putar ke auto, maka blower akan hidup. Jika suhu sudah mencapai batas yang di tentukan maka blower akan berhenti, kemudian timer (intermiten) akan menghidupkan blower 2 secara otomatis sesuai setting waktu yang di tentukan untuk menjaga sirkulasi udara", "icon_product18.png");
            initProduct(vendor9, "Panel DOL Starter", 6000000, 100, "Dimensi (PxLxT): 40x60x20; 3 phase; Overloadth-t18 11a; Push button: 4 buah; Lampu indikator: 6 buah; Terminal blok 25A: 2 buah; Terminal Nol: 1 unit", "-", "icon_product19.png");
            initUser(vendor9, "CV. Anugerah Teknik Mandiri", "csu.atm@gmail.com");
            //
            int vendor10 = initVendor("Sakura Jaya Bearing", "-", "(031) 5358268", "Jalan Koblen E No.5, Bubutan, Kec. Bubutan, Kota SBY", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor10.png");
            //
            initProduct(vendor10, "Oring Viton 4x40", 8000, 10000, "Ukuran (IdxOdxThk mm): 40x48x4; Material rubber viton", "-", "icon_product20.png");
            initProduct(vendor10, "Oring Seal 11mm", 2500, 10000, "Material Viton; Kekerasan V75", "-", "icon_product21.png");
            initUser(vendor10, "Sakura Jaya Bearing", "sakurajaya@gmail.com");

            //
            int vendor11 = initVendor("UD Karya Mandiri", "-", "(031) 749 6569", "Jl. Margomulyo Industri Raya Blok E No.15, Greges, Asem Rowo, Surabaya", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor11.png");
            //
            initProduct(vendor11, "Kawat Las NSN 309", 50000, 1000, "Kawat las NSN-309 MoL Stainless Steel ukuran 4.0 x 350 mm; Code AWS : A5.4 E309MoL-16 and Code JIS Z 3221 D309MoL-16; Komposisi Logam las: Low Carbon 22%Cr, 12% Ni, 2.5% Mo Stainless Steel", "-", "icon_product22.png");
            initProduct(vendor11, "Dempul", 45000, 10000, "Merk: Alfagloss; 1kg + hardener", "-", "icon_product23.png");
            initProduct(vendor11, "Batu Gerinda", 30000, 10000, "Ukuran: 150×6×22.2mm 6 Inch; T27-A/WAF24RBF; Kecepatan putar: 72M/S", "Dapat digunakan untuk memoles atau mengasah besi, baja dan stainless steel", "icon_product24.png");
            initUser(vendor11, "UD Karya Mandiri", "karyamandiri@gmail.com");

            //
            int vendor12 = initVendor("CV Bintang Marina", "https://desty.page/BintangMarina", "+62 813 3509 8688", "Tenggulunan Maju, Tenggulunan, Candi, Sidoarjo Regency", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor12.png");
            //
            initProduct(vendor12, "Kursi Nahkoda", 8750000, 100, "Rangka Bangku Full aluminium Extrution, Kaki Bangku Dies Casting, bisa turun naik, mundur, muter 3maju60 derajat, Bahan kain MB Tech, Arm rest Alu Extrution", "-", "icon_product25.png");
            initProduct(vendor12, "Kursi Penumpang Eksekutif", 3400000, 100, "Rangka Bangku Aluminium extrution , Kaki bangku, Base dan Armrest Aluminium extrution, Kain MB Tech", "-", "icon_product26.png");
            initUser(vendor12, "CV Bintang Marina", "bintangmarina@gmail.com");

            //
            int vendor13 = initVendor("Mitra Bahari Marine", "-", "(031) 3283727", "Jl. Perak Bar. No.353, Perak Utara, Kec. Pabean Cantikan, Kota SBY", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor13.png");
            //
            initProduct(vendor13, "Tangga Pandu (Pilot Ladder)", 180000, 1000, "Bahan: Tali tambang manila 16mm; Jarak pijakan: 40 cm; Berat: 2 kg", "Tangga pandu digunakan untuk keperluan kapal untuk memudahkan crew kapal turun. Harga per meter", "icon_product27.png");
            initProduct(vendor13, "Mast Headlight ", 1300000, 100, "Voltage: 220v; Power: 60 watt; Housing Material: Polycarbonate; Bottom Material: Aluminum; Coverage: 225°", "Mast headlight ditempatkan di atas garis tengah depan dan belakang kapal untuk menunjukkan cahaya tak terputus yang menandakan bagian depan, belakang, dan sisi kapal. Tersedia dalam warna putih, merah, dan hijau", "icon_product28.png");
            initProduct(vendor13, "Bandul Tali Buangan", 115000, 1000, "Berat 450 gram", "-", "icon_product29.png");
            initUser(vendor13, "Mitra Bahari Marine", "mitrabahari@gmail.com");

            //
            int vendor14 = initVendor("UD Sumber Rejeki", "-", "(031) 5014195", "Gubeng Kertajaya IV D No.111, Kertajaya, Kec. Gubeng, Kota SBY", "Product", "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate", "Happy Live Laugh", "icon_vendor14.png");
            //
            initProduct(vendor14, "Gate Valve 5K", 300000, 10000, "Ukuran: 2 inch; Koneksi: Drat; Material: Kuningan; Merk: KITZ class 125", "-", "icon_product30.png");
            initProduct(vendor14, "Flange 5K", 60000, 10000, "Ukuran: 2 inch; Material: Carbon Steel; Ukuran (DxCxT mm): 130x105x12 ", "-", "icon_product31.png");
            initProduct(vendor14, "Elbow Galvanis", 150000, 10000, "Ukuran: 3 inch; Material: Besi Galvanis; Koneksi: Drat; Class 150", "-", "icon_product32.png");
            initProduct(vendor14, "Foot Klep", 90000, 10000, "Ukuran: 2 inch; Material: PVC; Koneksi: Drat dalam", "-", "icon_product33.png");
            initProduct(vendor14, "Ball Valve", 85000, 10000, "Ukuran: 1/2 inch; Material: Kuningan", "-", "icon_product34.png");
            initProduct(vendor14, "Reducer Galvanis", 35000, 10000, "Ukuran: 2 inch; Material: Malleable Iron; Koneksi: Drat", "-", "icon_product35.png");
            initUser(vendor14, "UD Sumber Rejeki", "sumberrejeki@gmail.com");

            //

            int vendor15 = initVendor("PT Indrayasa Migasa", "marketing.lubricants@indrayasamigasa.co.id", "(031) 99343333", "Jl. Simo Tambaan II No 68 T, Simomulyo, Kec. Sukomanunggal, Kota SBY", "Product", "Along with the development in our country Indonesia and for efforts to increase trade towards the era of globalization and welcome the free market (free trade). Based on the notarial deed of Soejipto, SH Number 83 dated 28 April 1992, PT. Indrayasa Migasa was established in accordance with the provisions in force in the Republic of Indonesia.", "Become the top and most trusted Pertamina Lubricant Distributor in providing total solutions for every customer's lubricant needs.", "icon_vendor15.png");
            initProduct(vendor15, "Oli Rored HAD 90", 200000, 10000, "Oli Rored HAD Merk Pertamina; Ukuran: 4 liter", "-", "icon_product36.png");
            initUser(vendor15, "PT Indrayasa Migasa", "indrayasamigasa@gmail.com");
        }

        public void insertDBFullService()
        {
            int service1 = initVendorService("PT Karunia Tata Prima Sejahtera", "-", "0812-3002-9400", "Jl. Pesapen Selatan No.34, Krembangan Sel., Kec. Krembangan, Kota SBY", "", "", "icon_subkon1.png");
            //
            initService(service1, "Scraping", 12000, 1, "per m2", "-", "icon_service1.png");
            initService(service1, "Water Jet", 15000, 1, "per m2", "-", "icon_service2.png");
            initService(service1, "Sand Blasting", 60000, 1, "per m2", "-", "icon_service3.png");

            initUser(service1, "PT Karunia Tata Prima Sejahtera", "karunia@gmail.com");

            //
            int service2 = initVendorService("PT MAS", "arihartanto@mas.co.id", "(031) 46836388", "Jl. Kenjeran No.306, RW.02, Gading, Kec. Tambaksari, Kota SBY", "", "", "icon_subkon2.png");
            //
            initService(service2, "Cat Primer", 8000, 1, "per m2/layer", "-", "icon_service4.png");
            initService(service2, "Cat Anti-Corrosion", 8000, 1, "per m2/layer", "-", "icon_service5.png");
            initService(service2, "Cat Anti-Fouling ", 8000, 1, "per m2/layer", "-", "icon_service6.png");
            initService(service2, "Cat Car Deck/Ramp Door", 8000, 1, "per m2/layer", "-", "icon_service7.png");
            initService(service2, "Pemasangan Anode", 120000, 1, "per buah", "-", "icon_service8.png");
            initUser(service2, "PT MAS", "arihartanto@mas.co.id");
            //
            int service3 = initVendorService("PT Aquamarine Divindo Inspection", "inspection@aquamarine.id", "(031) 8690099 ", "Jl. Raya Sedati Gede 88 Sedati, Juanda Airport, Sidoarjo", "PT Aquamarine Divindo Inspection is an Indonesian diving company with regional coverage offering general Marine Services and Construction. Established in 2007, years of experience as a safety diving and constructions contractors has brought us to be a recognized party which being important and integral part of offshore oil and gas industry growth in region.", "Become the top and most trusted Pertamina Lubricant Distributor in providing total solutions for every customer's lubricant needs.", "icon_subkon 3.png");
            //
            initService(service3, "Ultrasonic Testing", 25000, 1, "per titik", "-", "icon_service9.png");
            initService(service3, "Gambar dan Record UT", 2000000, 1, "-", "-", "icon_service10.png");
            initService(service3, "Penggantian Pelat", 30000, 1, "per kg", "-", "icon_service11.png");
            initUser(service3, "PT Aquamarine Divindo Inspection", "inspection@aquamarine.id");
            //
            int service4 = initVendorService("PT Cakra Muda Gemilang", "cmg@service.id", "(031) 99148550", "Jl. Puncak Indah Lontar II No.69, Babatan, Kec. Wiyung, Kota SBY", "", "", "icon_subkon4.png");
            //
            initService(service4, "Penggantian Fender Diameter 6 Inch", 1500000, 1, "per unit", "-", "icon_service12.png");
            initService(service4, "Penggantian Fender Diameter 8 Inch", 2800000, 1, "per unit", "-", "icon_service13.png");
            initUser(service4, "PT Cakra Muda Gemilang", "cmg@service.id");
            //
            int service5 = initVendorService("CV Sinar Madura", "muh.tajul@gmail.com", "(031) 7871416", "Jl. Jokotole No.V/2, RT.005/RW.004, Keluharan, Keraton, Kec. Bangkalan, Kabupaten Bangkalan", "", "", "icon_subkon5.png");
            //
            initService(service5, "Pembuatan dan Pemasangan railing", 1500000, 1, "per meter", "-", "icon_service14.png");
            initService(service5, "Reparasi Tiang Railing", 200000, 1, "per kg", "-", "icon_service15.png");
            initService(service5, "Reparasi Handrail", 250000, 1, "per kg", "-", "icon_service16.png");
            initUser(service5, "CV Sinar Madura", "muh.tajul@gmail.com");
            //
            int service6 = initVendorService("PT Dwi Sejahtera", "agusbudiono@dwisejahtera.co.id", "(031) 70995250", "Jl. Brigjend Katamso No.74A, Wedoro, Kec. Waru, Kabupaten Sidoarjo", "", "", "icon_subkon6.png");
            //
            initService(service6, "Ganti Engsel dan As Rampdoor", 6000000, 1, "per unit", "-", "icon_service17.png");
            initUser(service6, "PT Dwi Sejahtera", "agusbudiono@dwisejahtera.co.id");
            //
            int service7 = initVendorService("CV Widjaya Karya", "widjayakarya.cv@gmail.com", "(031) 5940732", "Jl. Manyar Kertoarjo VII No.3, Mojo, Kec. Gubeng, Kota SBY", "", "", "icon_subkon7.png");
            //
            initService(service7, "Pembersihan Tangki Ballast", 250000, 1, "per m3", "Terdiri dari Pembuangan Lumpur, penyemenan tangki, dan cleaning", "icon_service18.png");
            initService(service7, "Pembersihan Tangki Air Tawar", 50000, 1, "per m3", "Terdiri dari penyemenan tangki dan cleaning", "icon_service19.png");
            initService(service7, "Pressure Test Tangki", 100000, 1, "per m3", "Pengujian mengguanakan air tawar", "icon_service20.png");
            initUser(service7, "CV Widjaya Karya", "widjayakarya.cv@gmail.com");

            //
            int service8 = initVendorService("CV Mantika Marine Service", "-", "0811-321-367", "Jl. Ikan Mungsing VI No.35, Perak Bar., Kec. Krembangan, Kota SBY", "", "", "icon_subkon8.png");
            //
            initService(service8, "Pembersihan Jangkar", 5000000, 1, "per set", "Untuk kapal 600-900 GRT", "icon_service21.png");
            initService(service8, "Penimbangan Jangkar", 1250000, 1, "per set", "-", "icon_service22.png");
            initService(service8, "Penggantian Rantai Jangkar", 1500000, 1, "per segel", "Untuk kapal 600-900 GRT", "icon_service23.png");
            initUser(service8, "CV Mantika Marine Service", "mantika@gmail.com");
            //
            int service9 = initVendorService("PT Sabar Makmur Barokah", "ptsmb@gmail.com", "0812-7502-2229", "Jl. Bendul Merisi Gg. Besar Timur No.28-B, Bendul Merisi, Kec. Wonocolo, Kota SBY", "", "", "icon_subkon9.png");
            //
            initService(service9, "Bongkar Pasang Skerm", 1500000, 1, "per unit", "untuk diameter 5 inch", "icon_service24.png");
            initService(service9, "Ganti Remeirs Packing", 2000000, 1, "per meter", "-", "icon_service25.png");
            initService(service9, "Cabut As Propeller (Bongkar Pasang)", 10000000, 1, "per unit", "Untuk diameter as 5 inch", "icon_service26.png");
            initService(service9, "Alignment ME terhadap As Propeller", 8000000, 1, "per unit", "Untuk diameter as 5 inch", "icon_service27.png");
            initService(service9, "Cabut Propeller (Bongkar Pasang)", 9000000, 1, "per unit", "Untuk diameter propeller 2-3 meter", "icon_service28.png");
            initService(service9, "Polish Propeller", 3700000, 1, "per unit", "Untuk diameter propeller 2-3 meter", "icon_service29.png");
            initService(service9, "Balancing Propeller", 5000000, 1, "per unit", "Untuk diameter propeller 2-3 meter", "icon_service30.png");
            initUser(service9, "PT Sabar Makmur Barokah", "ptsmb@gmail.com");
            //
        }

        public int initVendor(string vendorname, string vendoremail, string vendorphone, string vendoraddress, string type, string vendorabout, string vendormission, string vendorimage)
        {

            Vendor obj = new Vendor();
            obj.VendorName = vendorname;
            obj.VendorAddress = vendoraddress;
            obj.VendorPhone = vendorphone;
            obj.VendorEmail = vendoremail;
            obj.Type = "Product";
            obj.VendorAbout = vendorabout;
            obj.VendorMission = vendormission;
            //obj.VendorImage = "https://i.ibb.co/jDfHJJq/icon-photo1.jpg";
            obj.VendorImage = vendorimage;
            vendor.InsertVendor(obj);

            return obj.ID;

        }

        public void initTrans(int id , int productid , string status)
        {
            try
            {
                if (productid != 0)
                {
                    Transaction obj = new Transaction();
                    obj.VendorID = id;
                    obj.ProductID = productid;
                    obj.UserID = 1;
                    obj.Status = status;
                    obj.BuyDate = DateTime.Now.AddMonths(-1);
                    obj.TotalItem = 1;

                    var getPrice = ps.GetProductById(productid).Result;
                    obj.Price = getPrice.FirstOrDefault().ProductPrice;
                    obj.TotalPrice = obj.TotalItem * obj.Price;

                    ts.InsertTransaction(obj);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void initNowTrans(int id, int productid, string status)
        {
            try
            {
                if (productid != 0)
                {
                    Transaction obj = new Transaction();
                    obj.VendorID = id;
                    obj.ProductID = productid;
                    obj.UserID = 1;
                    obj.Status = status;
                    obj.BuyDate = DateTime.Now;
                    obj.TotalItem = 1;

                    var getPrice = ps.GetProductById(productid).Result;
                    obj.Price = getPrice.FirstOrDefault().ProductPrice;
                    obj.TotalPrice = obj.TotalItem * obj.Price;
                    ts.InsertTransaction(obj);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int initProduct(int id, string name, int price, int sisa, string description, string spec, string image)
        {
            Products objP = new Products();
            objP.ProductName = name;
            objP.ProductPrice = price;
            objP.ProductSisa = sisa;
            objP.ProductCategory = "Product";
            objP.ProductDescription = description;
            objP.ProductSpecification = spec;
            objP.VendorID = id;
            objP.ProductSeen = 0;
            objP.ProductImage = image;
            ps.InsertProduct(objP);

            return objP.ID;
        }

        public void initUser(int id, string username, string email)
        {
            User objUser = new User();
            objUser.Username = username;
            objUser.Email = email;
            objUser.Password = "asdf";
            objUser.Role = "Vendor";
            objUser.IsVerified = true;
            objUser.VerifiedCode = "1234";
            objUser.VendorID = id;
            user.InsertUser(objUser);
        }

        public int initVendorService(string vendorname, string vendoremail, string vendorphone, string vendoraddress, string vendorabout, string vendormission, string vendorimage)
        {

            Vendor obj = new Vendor();
            obj.VendorName = vendorname;
            obj.VendorAddress = vendoraddress;
            obj.VendorPhone = vendorphone;
            obj.VendorEmail = vendoremail;
            obj.Type = "Service";
            obj.VendorAbout = vendorabout;
            obj.VendorMission = vendormission;
            obj.VendorImage = vendorimage;
            vendor.InsertVendor(obj);

            return obj.ID;

        }

        public void initService(int id, string name, int price, int sisa, string description, string spec, string image)
        {
            Products objP = new Products();
            objP.ProductName = name;
            objP.ProductPrice = price;
            objP.ProductSisa = sisa;
            objP.ProductCategory = "Service";
            objP.ProductDescription = description;
            objP.ProductSpecification = spec;
            objP.VendorID = id;
            objP.ProductSeen = 0;
            objP.ProductImage = image;
            ps.InsertProduct(objP);
        }

        public void addInitial()
        {
            //Vendor obj = new Vendor();
            //obj.VendorName = "Vendor Product A";
            //obj.VendorAddress = "Jalan A no 1";
            //obj.VendorPhone = "02112345678";
            //obj.VendorEmail = "vendorA@gmail.com";
            //obj.Type = "Product";
            //obj.VendorAbout = "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate";
            //obj.VendorMission = "Happy Live Laugh";
            ////obj.VendorImage = "https://i.ibb.co/jDfHJJq/icon-photo1.jpg";
            //obj.VendorImage = "icon_photo1.jpg";
            //vendor.InsertVendor(obj);

            ////
            //Products objP = new Products();
            //objP.ProductName = "Product 1";
            //objP.ProductPrice = 5000;
            //objP.ProductSisa = 10;
            //objP.ProductCategory = "Product";
            //objP.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            //objP.ProductSpecification = "x:10 y:5";
            //objP.VendorID = obj.ID;
            //objP.ProductSeen = 0;
            //ps.InsertProduct(objP);


            //Products objP2 = new Products();
            //objP2.ProductName = "Product 2";
            //objP2.ProductPrice = 5000;
            //objP2.ProductSisa = 10;
            //objP2.ProductCategory = "Product";
            //objP2.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            //objP2.ProductSpecification = "x:10 y:5";
            //objP2.ProductSeen = 0;
            //objP2.VendorID = obj.ID;
            //ps.InsertProduct(objP2);

            //Products objP3 = new Products();
            //objP3.ProductName = "Product 3";
            //objP3.ProductPrice = 5000;
            //objP3.ProductSisa = 10;
            //objP3.ProductCategory = "Product";
            //objP3.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            //objP3.ProductSpecification = "x:10 y:5";
            //objP3.ProductSeen = 0;
            //objP3.VendorID = obj.ID;
            //ps.InsertProduct(objP3);
            ////

            //User objUser = new User();
            //objUser.Username = "vendorp1";
            //objUser.Email = "vendorp1@gmail.com";
            //objUser.Password = "vendorp1";
            //objUser.Role = "Vendor";
            //objUser.IsVerified = true;
            //objUser.VerifiedCode = "1234";
            //objUser.VendorID = obj.ID;
            ////objUser.Vendor = obj;
            //user.InsertUser(objUser);

            //Vendor obj2 = new Vendor();
            //obj2.VendorName = "Vendor Service A";
            //obj2.VendorAddress = "Jalan A no 1";
            //obj2.VendorPhone = "02112345678";
            //obj2.VendorEmail = "vendorB@gmail.com";
            //obj2.Type = "Service";
            //obj2.VendorAbout = "Lorem ipsum dolor sit amet, consectetur adisipicing elit. Nunc vulputate";
            //obj2.VendorMission = "Happy Live Laugh";
            //vendor.InsertVendor(obj2);

            ////
            //Products objS = new Products();
            //objS.ProductName = "Service 1";
            //objS.ProductPrice = 5000;
            //objS.ProductSisa = 10;
            //objS.ProductCategory = "Service";
            //objS.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            //objS.ProductSpecification = "x:10 y:5";
            //objS.ProductSeen = 0;
            //objS.VendorID = obj2.ID;
            //ps.InsertProduct(objS);


            //Products objS2 = new Products();
            //objS2.ProductName = "Service 2";
            //objS2.ProductPrice = 5000;
            //objS2.ProductSisa = 10;
            //objS2.ProductCategory = "Service";
            //objS2.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            //objS2.ProductSpecification = "x:10 y:5";
            //objS2.ProductSeen = 0;
            //objS2.VendorID = obj2.ID;
            //ps.InsertProduct(objS2);

            //Products objS3 = new Products();
            //objS3.ProductName = "Service 3";
            //objS3.ProductPrice = 5000;
            //objS3.ProductSisa = 10;
            //objS3.ProductCategory = "Service";
            //objS3.ProductDescription = "Lorem ipsum abcdefgh ijkl mn o pq";
            //objS3.ProductSpecification = "x:10 y:5";
            //objS3.ProductSeen = 0;
            //objS3.VendorID = obj2.ID;
            //ps.InsertProduct(objS3);
            ////

            //User objUser2 = new User();
            //objUser2.Username = "vendors1";
            //objUser2.Email = "vendors1@gmail.com";
            //objUser2.Password = "vendors1";
            //objUser2.Role = "Vendor";
            //objUser2.IsVerified = true;
            //objUser2.VerifiedCode = "1234";
            ////objUser2.Vendor = obj2;
            //objUser2.VendorID = obj2.ID;
            //user.InsertUser(objUser2);

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
        private DatabaseContext getContext()
        {
            return new DatabaseContext();
        }
    }
}