using System;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class StudentLoginPage : ContentPage
    {
        public StudentLoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string studentId = studentIdEntry.Text;
            string email = emailEntry.Text;

            
            var isValidLogin = DataManager.ValidateStudentLogin(studentId, email);
            if (isValidLogin)
            {
                // Navigate to the student's details page or dashboard
                await Navigation.PushAsync(new StudentDetailsPage(studentId));
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid ID or Email", "OK");
            }
        }
    }
}
