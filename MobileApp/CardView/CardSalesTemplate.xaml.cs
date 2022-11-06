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
    public partial class CardSalesTemplate : ContentView
    {
        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(CardSalesTemplate), string.Empty);
        public static readonly BindableProperty CardFillProperty = BindableProperty.Create(nameof(CardFill), typeof(string), typeof(CardSalesTemplate), string.Empty);
        public static readonly BindableProperty CardDescriptionProperty = BindableProperty.Create(nameof(CardDescription), typeof(string), typeof(CardSalesTemplate), string.Empty);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CardSalesTemplate), Color.DarkGray);
        public static readonly BindableProperty CardColorProperty = BindableProperty.Create(nameof(CardColor), typeof(Color), typeof(CardSalesTemplate), Color.White);
        //public static readonly BindableProperty IconImageSourceProperty = BindableProperty.Create(nameof(IconImageSource), typeof(ImageSource), typeof(CardViewTemplate), default(ImageSource));
        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(CardSalesTemplate), Color.LightGray);

        public string CardTitle
        {
            get => (string)GetValue(CardSalesTemplate.CardTitleProperty);
            set => SetValue(CardSalesTemplate.CardTitleProperty, value);
        }

        public string CardFill
        {
            get => (string)GetValue(CardSalesTemplate.CardFillProperty);
            set => SetValue(CardSalesTemplate.CardFillProperty, value);
        }

        public string CardDescription
        {
            get => (string)GetValue(CardSalesTemplate.CardDescriptionProperty);
            set => SetValue(CardSalesTemplate.CardDescriptionProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(CardSalesTemplate.BorderColorProperty);
            set => SetValue(CardSalesTemplate.BorderColorProperty, value);
        }

        public Color CardColor
        {
            get => (Color)GetValue(CardSalesTemplate.CardColorProperty);
            set => SetValue(CardSalesTemplate.CardColorProperty, value);
        }

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(CardSalesTemplate.IconBackgroundColorProperty);
            set => SetValue(CardSalesTemplate.IconBackgroundColorProperty, value);
        }

        public CardSalesTemplate()
        {
            InitializeComponent();
        }
    }
}