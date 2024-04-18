using System;
using System.Net.NetworkInformation;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Lidhja
{
    public partial class AddNewStudentPage : ContentPage
    {
        public AddNewStudentPage()
        {
            InitializeComponent();
        }



        private async void OnAddStudentClicked(object sender, EventArgs e)
        {
            var firstName = firstNameEntry.Text;
            var lastName = lastNameEntry.Text;
            var studentId = idEntry.Text;
            var dateOfBirth = dobPicker.Date;

            // Determine the selected gender
            string gender = null;
            if (maleRadioButton.IsChecked)
                gender = "Male";
            else if (femaleRadioButton.IsChecked)
                gender = "Female";
            else if (otherRadioButton.IsChecked)
                gender = "Other";

            var email = emailEntry.Text;
            var mobilePhone = mobilePhoneEntry.Text;
            var address = addressEntry.Text;
            var country = countryEntry.Text;

            //var nationality = nationalityPicker.SelectedItem?.ToString();

            // Determine the selected nationality
            string nationality = null;
            if (InternationalRadioButton.IsChecked)
                nationality = "International";
            else if (NationalRadioButton.IsChecked)
                nationality = "National";

            // Determine the selected Level of Study
            string levelOfStudy = null;
            if (UndergraduateRadioButton.IsChecked)
                levelOfStudy = "Undergraduate";
            else if (GraduateRadioButton.IsChecked)
                levelOfStudy = "Graduate";
            else if (PostgraduateRadioButton.IsChecked)
                levelOfStudy = "Postgraduate";
            //var levelOfStudy = levelOfStudyPicker.SelectedItem?.ToString();




            // Determine the selected Level of Study
            string programlOfStudy = null;
            if (ComputerScienceRadioButton.IsChecked)
                programlOfStudy = "ComputerScience";
            else if (CyberSecurityRadioButton.IsChecked)
                programlOfStudy = "CyberSecurity";
            else if (GamesProgrammingRadioButton.IsChecked)
                programlOfStudy = "GamesProgramming";
            else if (CreativeComputingRadioButton.IsChecked)
                programlOfStudy = "CreativeComputing";


            // Basic validation
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(studentId) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(mobilePhone) || string.IsNullOrWhiteSpace(address) ||
                gender == null || nationality == null || string.IsNullOrWhiteSpace(country) || levelOfStudy == null || programlOfStudy == null)
            {
                await DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }

            // Create a new Student object
            var newStudent = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                StudentId = studentId,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Email = email,
                MobilePhone = mobilePhone,
                Address = address,
                Nationality = nationality,
                Country = country,
                LevelOfStudy = levelOfStudy,
                ProgramOfStudy = programlOfStudy
            };

            // Add the new student to the DataManager's list
            DataManager.AddStudent(newStudent);

            // Inform the user that the student has been added
            await DisplayAlert("Success", "New student added successfully.", "OK");

            // Optionally, navigate back to the previous page
            await Navigation.PopAsync();
        }
    }
}
