using System;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class EditStudentProgressPage : ContentPage
    {
        private StudentProgress _studentProgress;

        public EditStudentProgressPage(StudentProgress studentProgress)
        {
            InitializeComponent();
            _studentProgress = studentProgress;
            this.BindingContext = _studentProgress;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Basic validation for empty fields
            if (string.IsNullOrWhiteSpace(fullNameEntry.Text) ||
                string.IsNullOrWhiteSpace(courseEntry.Text) ||
                string.IsNullOrWhiteSpace(gradeEntry.Text) ||
                string.IsNullOrWhiteSpace(notesEditor.Text))
            {
                await DisplayAlert("Validation Error", "All fields must be filled.", "OK");
                return;
            }

            // Update the student progress properties
            _studentProgress.FullName = fullNameEntry.Text;
            _studentProgress.Course = courseEntry.Text;
            _studentProgress.Grade = gradeEntry.Text;
            _studentProgress.Notes = notesEditor.Text;

            // Update the student progress in the database
            bool updateSuccessful = DataManager.UpdateStudentProgress(_studentProgress);
            if (updateSuccessful)
            {
                await DisplayAlert("Update Successful", "Student progress has been updated successfully.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Update Failed", "There was a problem updating the student progress.", "OK");
            }

        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
