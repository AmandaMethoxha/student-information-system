using System;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class StudentProgressPage : ContentPage
    {
        public StudentProgressPage()
        {
            InitializeComponent();
        }

        private async void OnInsertStudentProgressClicked(object sender, EventArgs e)
        {
            // Navigate to the Insert Student Progress Page
             await Navigation.PushAsync(new InsertStudentProgress());
        }

        private async void OnViewStudentProgressClicked(object sender, EventArgs e)
        {
            // Navigate to the View Student Progress Page
           await Navigation.PushAsync(new ViewStudentProgress());
        }
    }
}
