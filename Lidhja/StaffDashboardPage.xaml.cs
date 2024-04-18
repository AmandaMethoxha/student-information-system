using System;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class StaffDashboardPage : ContentPage
    {
        public StaffDashboardPage()
        {
            InitializeComponent();
        }

        private void OnStudentButtonClicked(object sender, EventArgs e)
        {
            // Navigate to Student Management Page
            Navigation.PushAsync(new StudentManagementPage());
        }

        private void OnStudentProgresButtonClicked(object sender, EventArgs e)
        {
            // Navigate to Course Management Page
            Navigation.PushAsync(new StudentProgressPage());
        }


        private void OnStatisticsButtonClicked(object sender, EventArgs e)
        {
            // Navigate to Statistics Page
            Navigation.PushAsync(new StatisticsPage());
        }


        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            

            // Navigate to the login or start page
            await Navigation.PushAsync(new LandingPage()); 
        }

    }
}