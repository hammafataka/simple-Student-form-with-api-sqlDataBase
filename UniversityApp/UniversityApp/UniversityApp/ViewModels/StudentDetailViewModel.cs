using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Essentials;
using Xamarin.Forms;

using UniveristyApp.Models;
using UniveristyApp.Services;
using UniveristyApp.Views;
using UniversityApp;

namespace UniveristyApp.ViewModels
{
    public class StudentDetailViewModel : BaseViewModel
    {
        private Students student;

        public Students st
        {
            get { return student; }
            set { SetProperty(ref student, value); }
        }
        public ICommand AddCommand { private set; get; }
        public ICommand UpdateCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }

        private string controller = "api/Students";
        private async Task Add()
        {
            bool result=await UniversityServiceApi.AddItem<Students>(controller, st);
            if (result)
                await App.Current.MainPage.DisplayAlert("Success!","The Student Was Added Successfuly!","Ok");
            else
                await App.Current.MainPage.DisplayAlert("Failed!", "The Student Was NOT Added!", "Ok");

        }
        private async Task Update()
        {
            bool result=await UniversityServiceApi.UpdateItem<Students>(controller, st,st.id);
            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Success!", "The student was updated successfuly!", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }

            else
                await App.Current.MainPage.DisplayAlert("Failed!", "The student was NOT updated!", "Ok");

        }
        private async Task Delete()
        {
            bool confirm = await App.Current.MainPage.DisplayAlert("Please Confirm", "Do You Really Want to delete this Item", "Yes","No");


            if (confirm)
            {
                bool result = await UniversityServiceApi.DeleteItem<Students>(controller, st.id);
                if (result)
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "The student was Deleted successfuly!", "Ok");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                    await App.Current.MainPage.DisplayAlert("Failed!", "The student was NOT Deleted!", "Ok");
            }


        }
        public StudentDetailViewModel(Students model)
        {
            st = model;
            AddCommand = new Command(async () => await Add());
            UpdateCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async()=> await Delete());
        }
    }
}
