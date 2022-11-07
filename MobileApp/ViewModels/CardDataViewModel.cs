using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class CardDataViewModel : BaseViewModel
    {
        public IList<Products> CardDataCollection { get; set; }

        public object SelectedItem { get; set; }

        public CardDataViewModel()
        {
            CardDataCollection = new List<Products>();
            GenerateCardModel();
            User = ((App)App.Current).userLoggedIn;
        }

        private void GenerateCardModel()
        {
            for (var i = 0; i < 3; i++)
            {


                var cardData = new Products()
                {
                    ProductImage = "company_logo.jpg",
                    ProductName = "Product" + i,
                    ProductPrice =  i,
                    ProductSisa =  i,
                };
                CardDataCollection.Add(cardData);

            }
        }
    }
}