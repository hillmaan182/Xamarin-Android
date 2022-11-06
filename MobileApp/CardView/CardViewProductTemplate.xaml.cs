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
    public partial class CardViewProductTemplate : ContentView
    {
        
        public static readonly BindableProperty ProductNameProperty = BindableProperty.Create(nameof(ProductName), typeof(string), typeof(CardViewProductTemplate), string.Empty);
        public static readonly BindableProperty ProductPriceProperty = BindableProperty.Create(nameof(ProductPrice), typeof(string), typeof(CardViewProductTemplate), "Rp." );
        public static readonly BindableProperty ProductSisaProperty = BindableProperty.Create(nameof(ProductSisa), typeof(string), typeof(CardViewProductTemplate), string.Empty);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CardViewProductTemplate), Color.DarkGray);
        public static readonly BindableProperty CardColorProperty = BindableProperty.Create(nameof(CardColor), typeof(Color), typeof(CardViewProductTemplate), Color.White);
        public static readonly BindableProperty ProductImageProperty = BindableProperty.Create(nameof(ProductImage), typeof(ImageSource), typeof(CardViewProductTemplate), default(ImageSource));
        //public static readonly BindableProperty IconImageSourceProperty = BindableProperty.Create(nameof(IconImageSource), typeof(ImageSource), typeof(CardViewTemplate), default(ImageSource));
        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(CardViewProductTemplate), Color.LightGray);

        

        public string ProductName
        {
            get => (string)GetValue(CardViewProductTemplate.ProductNameProperty);
            set => SetValue(CardViewProductTemplate.ProductNameProperty, value);
        }

        public string ProductPrice
        {
            get => (string)GetValue(CardViewProductTemplate.ProductPriceProperty);
            set => SetValue(CardViewProductTemplate.ProductPriceProperty, value );
        }

        public string ProductSisa
        {
            get => (string)GetValue(CardViewProductTemplate.ProductSisaProperty);
            set => SetValue(CardViewProductTemplate.ProductSisaProperty, value );
        }

        public ImageSource ProductImage
        {
            get => (ImageSource)GetValue(CardViewProductTemplate.ProductImageProperty);
            set => SetValue(CardViewProductTemplate.ProductImageProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(CardViewProductTemplate.BorderColorProperty);
            set => SetValue(CardViewProductTemplate.BorderColorProperty, value);
        }

        public Color CardColor
        {
            get => (Color)GetValue(CardViewProductTemplate.CardColorProperty);
            set => SetValue(CardViewProductTemplate.CardColorProperty, value);
        }

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(CardViewProductTemplate.IconBackgroundColorProperty);
            set => SetValue(CardViewProductTemplate.IconBackgroundColorProperty, value);
        }

        public CardViewProductTemplate()
        {
            InitializeComponent();
        }
    }
}