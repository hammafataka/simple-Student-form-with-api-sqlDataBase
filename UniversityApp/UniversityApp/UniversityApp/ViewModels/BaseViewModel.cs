using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace UniveristyApp.ViewModels
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged([CallerMemberName] string propertyname="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        protected bool SetProperty<T>(ref T storage,T value,[CallerMemberName]string propertyname = "")
        {
            if (object.Equals(storage, value))
                return false;
            storage = value;
            onPropertyChanged(propertyname);
            return true;
        }
        private bool isBusy=false;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);

        }

    }
}
