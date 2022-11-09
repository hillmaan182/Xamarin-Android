using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.CardView
{
    [DesignTimeVisible(true)]
    public partial class CardVendor : ContentView
    {
        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(CardVendor), string.Empty);
        public static readonly BindableProperty CardDescriptionProperty1= BindableProperty.Create(nameof(CardDescription1), typeof(string), typeof(CardVendor), string.Empty);
        public static readonly BindableProperty CardDescriptionProperty2 = BindableProperty.Create(nameof(CardDescription2), typeof(string), typeof(CardVendor), string.Empty);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CardVendor), Color.DarkGray);
        public static readonly BindableProperty CardColorProperty = BindableProperty.Create(nameof(CardColor), typeof(Color), typeof(CardVendor), Color.White);
        //public static readonly BindableProperty IconImageSourceProperty = BindableProperty.Create(nameof(IconImageSource), typeof(ImageSource), typeof(CardViewTemplate), default(ImageSource));
        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(CardVendor), Color.LightGray);

        public string CardTitle
        {
            get => (string)GetValue(CardVendor.CardTitleProperty);
            set => SetValue(CardVendor.CardTitleProperty, value);
        }

        public string CardDescription1
        {
            get => (string)GetValue(CardVendor.CardDescriptionProperty1);
            set => SetValue(CardVendor.CardDescriptionProperty1, value);
        }

        public string CardDescription2
        {
            get => (string)GetValue(CardVendor.CardDescriptionProperty2);
            set => SetValue(CardVendor.CardDescriptionProperty2, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(CardVendor.BorderColorProperty);
            set => SetValue(CardVendor.BorderColorProperty, value);
        }

        public Color CardColor
        {
            get => (Color)GetValue(CardVendor.CardColorProperty);
            set => SetValue(CardVendor.CardColorProperty, value);
        }

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(CardVendor.IconBackgroundColorProperty);
            set => SetValue(CardVendor.IconBackgroundColorProperty, value);
        }

        public CardVendor()
        {
            InitializeComponent();
        }
    }
}