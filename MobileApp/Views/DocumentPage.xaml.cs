using System;
using MobileApp.Models;
using MobileApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentPage : ContentPage
    {
        private string PDFPath;
        DocumentService ds;
        public DocumentPage()
        {
            InitializeComponent();
            ds = new DocumentService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            show();
        }

        private void show()
        {
            int userid = ((App)App.Current).userID;

            var res1 = ds.GetDocumentByDocId(1, userid);
            if (res1 == 0)
            {
                ok1.IsVisible = false;
                nok1.IsVisible = true;
            } else 
            {
                ok1.IsVisible = true;
                nok1.IsVisible = false;
            }

            var res2 = ds.GetDocumentByDocId(2, userid);
            if (res2 == 0)
            {
                ok2.IsVisible = false;
                nok2.IsVisible = true;
            }
            else
            {
                ok2.IsVisible = true;
                nok2.IsVisible = false;
            }
          

            var res3 = ds.GetDocumentByDocId(3, userid);
            if (res3 == 0)
            {
                ok3.IsVisible = false;
                nok3.IsVisible = true;
            }
            else
            {
                ok3.IsVisible = true;
                nok3.IsVisible = false;
            }
           

            var res4 = ds.GetDocumentByDocId(4, userid);
            if (res4 == 0)
            {
                ok4.IsVisible = false;
                nok4.IsVisible = true;
            }
            else
            {
                ok4.IsVisible = true;
                nok4.IsVisible = false;
            }
           

            var res5 = ds.GetDocumentByDocId(5, userid);
            if (res5 == 0)
            {
                ok5.IsVisible = false;
                nok5.IsVisible = true;
            }
            else
            {
                ok5.IsVisible = true;
                nok5.IsVisible = false;
            }
           

            var res6 = ds.GetDocumentByDocId(6, userid);
            if (res6 == 0)
            {
                ok6.IsVisible = false;
                nok6.IsVisible = true;
            }
            else
            {
                ok6.IsVisible = true;
                nok6.IsVisible = false;
            }
           

            var res7= ds.GetDocumentByDocId(7, userid);
            if (res7 == 0)
            {
                ok7.IsVisible = false;
                nok7.IsVisible = true;
            }
            else
            {
                ok7.IsVisible = true;
                nok7.IsVisible = false;
            }
            

            var res8 = ds.GetDocumentByDocId(8, userid);
            if (res8 == 0)
            {
                ok8.IsVisible = false;
                nok8.IsVisible = true;
            }
            else
            {
                ok8.IsVisible = true;
                nok8.IsVisible = false;
            }
            

            var res9 = ds.GetDocumentByDocId(9, userid);
            if (res9 == 0)
            {
                ok9.IsVisible = false;
                nok9.IsVisible = true;
            }
            else
            {
                ok9.IsVisible = true;
                nok9.IsVisible = false;
            }
           

            var res10 = ds.GetDocumentByDocId(10, userid);
            if (res10 == 0)
            {
                ok10.IsVisible = false;
                nok10.IsVisible = true;
            }
            else
            {
                ok10.IsVisible = true;
                nok10.IsVisible = false;
            }
           

            var res11 = ds.GetDocumentByDocId(11, userid);
            if (res11 == 0)
            {
                ok11.IsVisible = false;
                nok11.IsVisible = true;
            }
            else
            {
                ok11.IsVisible = true;
                nok11.IsVisible = false;
            }
           

            var res12 = ds.GetDocumentByDocId(12, userid);
            if (res12 == 0)
            {
                ok12.IsVisible = false;
                nok12.IsVisible = true;
            }
            else
            {
                ok12.IsVisible = true;
                nok12.IsVisible = false;
            }
            

            var res13 = ds.GetDocumentByDocId(13, userid);
            if (res13 == 0)
            {
                ok13.IsVisible = false;
                nok13.IsVisible = true;
            }
            else
            {
                ok13.IsVisible = true;
                nok13.IsVisible = false;
            }
           

            var res14 = ds.GetDocumentByDocId(14, userid);
            if (res14 == 0)
            {
                ok14.IsVisible = false;
                nok14.IsVisible = true;
            }
            else
            {
                ok14.IsVisible = true;
                nok14.IsVisible = false;
            }
            

            var res15 = ds.GetDocumentByDocId(15, userid);
            if (res15 == 0)
            {
                ok15.IsVisible = false;
                nok15.IsVisible = true;
            }
            else
            {
                ok15.IsVisible = true;
                nok15.IsVisible = false;
            }
           
        }
        private async void tap1_Tapped(object sender, EventArgs e)
        {

            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 1;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                ok1.IsVisible = true;
                nok1.IsVisible = false;
            }
        }

        private async void tap2_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 2;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }
        private async void tap3_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 3;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }
        private async void tap4_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 4;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap5_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 54;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap6_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 6;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap7_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 7;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap8_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 8;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap9_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 9;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap10_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 10;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap11_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 11;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap12_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 12;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap13_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 13;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap14_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 14;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }

        private async void tap15_Tapped(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "*/*" } },
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
            var result = await FilePicker.PickAsync(options);
            PDFPath = result.FullPath;

            Document obj = new Document();
            obj.UserId = ((App)App.Current).userID;
            obj.DocId = 15;
            obj.IsUpload = true;
            obj.FilePath = PDFPath;
            int c = ds.InsertDocument(obj);
            if (c == 1)
            {
                show();
            }
        }
    }
}