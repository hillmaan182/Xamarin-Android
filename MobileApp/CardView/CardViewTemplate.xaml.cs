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
    public partial class CardViewTemplate : ContentView
    {
        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(CardViewTemplate), string.Empty);
        public static readonly BindableProperty CardDescriptionProperty = BindableProperty.Create(nameof(CardDescription), typeof(string), typeof(CardViewTemplate), string.Empty);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CardViewTemplate), Color.DarkGray);
        public static readonly BindableProperty CardColorProperty = BindableProperty.Create(nameof(CardColor), typeof(Color), typeof(CardViewTemplate), Color.White);
        //public static readonly BindableProperty IconImageSourceProperty = BindableProperty.Create(nameof(IconImageSource), typeof(ImageSource), typeof(CardViewTemplate), default(ImageSource));
        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(CardViewTemplate), Color.LightGray);

        public string CardTitle
        {
            get => (string)GetValue(CardViewTemplate.CardTitleProperty);
            set => SetValue(CardViewTemplate.CardTitleProperty, value);
        }

        public string CardDescription
        {
            get => (string)GetValue(CardViewTemplate.CardDescriptionProperty);
            set => SetValue(CardViewTemplate.CardDescriptionProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(CardViewTemplate.BorderColorProperty);
            set => SetValue(CardViewTemplate.BorderColorProperty, value);
        }

        public Color CardColor
        {
            get => (Color)GetValue(CardViewTemplate.CardColorProperty);
            set => SetValue(CardViewTemplate.CardColorProperty, value);
        }

        //public ImageSource IconImageSource
        //{
        //    get => (ImageSource)GetValue(CardViewTemplate.IconImageSourceProperty);
        //    set => SetValue(CardViewTemplate.IconImageSourceProperty, value);
        //}

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(CardViewTemplate.IconBackgroundColorProperty);
            set => SetValue(CardViewTemplate.IconBackgroundColorProperty, value);
        }

        public CardViewTemplate()
        {
            InitializeComponent();
        }
    }
}