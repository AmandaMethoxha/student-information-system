using System;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class EditStudentPage : ContentPage
    {
        private Student _student;

        public EditStudentPage(Student student)
        {
            InitializeComponent();
            _student = student;
            this.BindingContext = _student;

            SetInitialValues();
        }



        private void SetInitialValues()
        {

            // Gender
            switch (_student.Gender)
            {
                case "Male":
                    maleRadioButton.IsChecked = true;
                    break;
                case "Female":
                    femaleRadioButton.IsChecked = true;
                    break;
                case "Other":
                    otherRadioButton.IsChecked = true;
                    break;
            }

            // Nationality
            switch (_student.Nationality)
            {
                case "National":
                    nationalRadioButton.IsChecked = true;
                    break;
                case "International":
                    internationalRadioButton.IsChecked = true;
                    break;
            }

            // Level of Study
            switch (_student.LevelOfStudy)
            {
                case "Undergraduate":
                    undergraduateRadioButton.IsChecked = true;
                    break;
                case "Graduate":
                    graduateRadioButton.IsChecked = true;
                    break;
                case "Postgraduate":
                    postgraduateRadioButton.IsChecked = true;
                    break;
            }

            // Program of Study
            switch (_student.ProgramOfStudy)
            {
                case "Computer Science":
                    computerScienceRadioButton.IsChecked = true;
                    break;
                case "Cyber Security":
                    cyberSecurityRadioButton.IsChecked = true;
                    break;
                case "Games Programming":
                    gamesProgrammingRadioButton.IsChecked = true;
                    break;
                case "Creative Computing":
                    creativeComputingRadioButton.IsChecked = true;
                    break;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Basic validation for empty fields
            if (string.IsNullOrWhiteSpace(firstNameEntry.Text) ||
                string.IsNullOrWhiteSpace(lastNameEntry.Text) ||
                string.IsNullOrWhiteSpace(idEntry.Text) ||
                string.IsNullOrWhiteSpace(emailEntry.Text) ||
                string.IsNullOrWhiteSpace(mobilePhoneEntry.Text) ||
                string.IsNullOrWhiteSpace(addressEntry.Text) ||
                string.IsNullOrWhiteSpace(countryEntry.Text) ||
                !IsRadioGroupSelected(genderRadioGroup) ||
                !IsRadioGroupSelected(nationalityRadioGroup) ||
                !IsRadioGroupSelected(levelOfStudyRadioGroup) ||
                !IsRadioGroupSelected(programOfStudyRadioGroup))
            {
                await DisplayAlert("Validation Error", "All fields must be filled, and options selected.", "OK");
                return;
            }


            // Update the student's properties
            _student.FirstName = firstNameEntry.Text;
            _student.LastName = lastNameEntry.Text;
            _student.StudentId = idEntry.Text;
            _student.DateOfBirth = dobPicker.Date;
            _student.Email = emailEntry.Text;
            _student.MobilePhone = mobilePhoneEntry.Text;
            _student.Address = addressEntry.Text;
            _student.Country = countryEntry.Text;

            // Radio buttons update
            _student.Gender = maleRadioButton.IsChecked ? "Male" :
                              femaleRadioButton.IsChecked ? "Female" :
                              otherRadioButton.IsChecked ? "Other" : null;

            _student.Nationality = nationalRadioButton.IsChecked ? "National" :
                                   internationalRadioButton.IsChecked ? "International" : null;

            _student.LevelOfStudy = undergraduateRadioButton.IsChecked ? "Undergraduate" :
                                    graduateRadioButton.IsChecked ? "Graduate" :
                                    postgraduateRadioButton.IsChecked ? "Postgraduate" : null;

            _student.ProgramOfStudy = computerScienceRadioButton.IsChecked ? "Computer Science" :
                                      cyberSecurityRadioButton.IsChecked ? "Cyber Security" :
                                      gamesProgrammingRadioButton.IsChecked ? "Games Programming" :
                                      creativeComputingRadioButton.IsChecked ? "Creative Computing" : null;


            { // Update the student in the database
                DataManager.UpdateStudent(_student);

                // Inside OnSaveClicked after successful update
                //MessagingCenter.Send(this, "UpdateStudent", "Student details have been updated successfully.");

              //  WeakReferenceMessenger.Default.Send(new StudentUpdatedMessage(_student));

                // Display a confirmation message and navigate back
                await DisplayAlert("Update Successful", "Student details have been updated successfully.", "OK");


                // Send the message
                WeakReferenceMessenger.Default.Send(new StudentUpdatedMessage("Student details have been updated successfully."));

                // Inside OnSaveClicked after successful update
                //MessagingCenter.Send(this, nameof(StudentUpdatedMessage), new StudentUpdatedMessage());

                await Navigation.PopAsync();
            }
            
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private bool IsRadioGroupSelected(StackLayout radioGroup)
        {
            foreach (var child in radioGroup.Children)
            {
                if (child is RadioButton rb && rb.IsChecked)
                    return true;
            }
            return false;
        }
    }
}
