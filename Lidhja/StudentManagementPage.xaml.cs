using System;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class StudentManagementPage : ContentPage
    {
        public StudentManagementPage()
        {
            InitializeComponent();
        }

        // Add New Student button click event
        private async void OnAddNewStudentClicked(object sender, EventArgs e)
        {
            // Navigate to Add New Student Page
            await Navigation.PushAsync(new AddNewStudentPage());
        }

        private async void OnStudentsListClicked(object sender, EventArgs e)
        {
            // Navigate to View Students Page
            
            await Navigation.PushAsync(new StudentsListPage());
        }


    }
}