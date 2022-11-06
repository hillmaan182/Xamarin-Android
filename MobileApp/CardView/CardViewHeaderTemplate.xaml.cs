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
    public partial class CardViewHeaderTemplate : ContentView
    {
        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(CardViewTemplate), string.Empty);
        public static readonly BindableProperty CardDescriptionProperty = BindableProperty.Create(nameof(CardDescription), typeof(string), typeof(CardViewTemplate), string.Empty);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CardViewTemplate), Color.DarkGray);
        public static readonly BindableProperty CardColorProperty = BindableProperty.Create(nameof(CardColor), typeof(Color), typeof(CardViewTemplate), Color.White);
        //public static readonly BindableProperty IconImageSourceProperty = BindableProperty.Create(nameof(IconImageSource), typeof(ImageSource), typeof(CardViewTemplate), default(ImageSource));
        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(nameof(IconBackgroundColor), typeof(Color), typeof(CardViewTemplate), Color.LightGray);

        public string CardTitle
        {
            get => (string)GetValue(CardViewHeaderTemplate.CardTitleProperty);
            set => SetValue(CardViewHeaderTemplate.CardTitleProperty, value);
        }

        public string CardDescription
        {
            get => (string)GetValue(CardViewHeaderTemplate.CardDescriptionProperty);
            set => SetValue(CardViewHeaderTemplate.CardDescriptionProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(CardViewHeaderTemplate.BorderColorProperty);
            set => SetValue(CardViewHeaderTemplate.BorderColorProperty, value);
        }

        public Color CardColor
        {
            get => (Color)GetValue(CardViewHeaderTemplate.CardColorProperty);
            set => SetValue(CardViewHeaderTemplate.CardColorProperty, value);
        }

        public Color IconBackgroundColor
        {
            get => (Color)GetValue(CardViewHeaderTemplate.IconBackgroundColorProperty);
            set => SetValue(CardViewHeaderTemplate.IconBackgroundColorProperty, value);
        }

        public CardViewHeaderTemplate()
        {
            InitializeComponent();
        }
    }
}