using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using UniveristyApp.ViewModels;

namespace UniveristyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentListView : ContentPage
    {
        StudentListViewModel vm;
        public StudentListView()
        {
            InitializeComponent();
            vm = new StudentListViewModel();
            BindingContext = vm;
            

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
                await Task.Run(() => vm.LoadDataCommand.Execute(null));
                vm.SelectedStudent = null;

        }
    }
}