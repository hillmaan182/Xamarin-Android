using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string user = string.Empty;
        public string User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        bool isIncome = false;

        public bool IsIncome
        {
            get { return isIncome; }
            set { SetProperty(ref isIncome, value); }
        }

        bool isOutcome = false;

        public bool IsOutcome
        {
            get { return isOutcome; }
            set { SetProperty(ref isOutcome, value); }
        }

        Color borderIncome = Color.Beige;

        public Color BorderIncome
        {
            get { return borderIncome; }
            set { SetProperty(ref borderIncome, value); }
        }

        Color borderOutcome = Color.Beige;

        public Color BorderOutcome
        {
            get { return borderOutcome; }
            set { SetProperty(ref borderOutcome, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
