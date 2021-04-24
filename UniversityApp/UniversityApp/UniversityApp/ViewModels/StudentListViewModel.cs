using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

using UniveristyApp.Models;
using UniveristyApp.Services;
using UniversityApp.Views;
using UniversityApp;
using UniveristyApp.Views;

namespace UniveristyApp.ViewModels
{
    public class StudentListViewModel:BaseViewModel
    {
        public ObservableCollection<Students> Student { get; }
        private Students selectedStudent;

        public Students SelectedStudent
        {
            get { return selectedStudent; }
            set { SetProperty(ref selectedStudent, value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailCommand { private set; get; }
        public ICommand GoToNewCommand { private set; get; }

        public async Task LoadData()
        {
            IsBusy = true;
            Student.Clear();

            List<Students> list = await UniversityServiceApi.GetItems<Students>("api/Students");
            foreach (Students item in list)
            {
                Student.Add(item);
            }
            IsBusy = false;
        }
        private async Task GoToDetail()
        {
            if (selectedStudent != null)
                await NavigateToDetail(SelectedStudent);
            
        }
        private async Task GoToNew()
        {
            Students student = new Students();
            await NavigateToDetail(student);
        }
        private async Task NavigateToDetail(Students student)
        {
            StudentDetailViewModel vm = new StudentDetailViewModel(student);
            StudentDetailView view = new StudentDetailView();
            view.BindingContext = vm;
            await App.Current.MainPage.Navigation.PushAsync(view);
        }
        public StudentListViewModel()
        {
            Student = new ObservableCollection<Students>();
            LoadDataCommand = new Command(async () => await LoadData());
            GoToDetailCommand = new Command(async () => await GoToDetail());
            GoToNewCommand = new Command(async () => await GoToNew());
        }

    }
}
