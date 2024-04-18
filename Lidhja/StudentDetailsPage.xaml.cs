using System.Linq;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class StudentDetailsPage : ContentPage
    {
        private readonly string _studentId;

        public StudentDetailsPage(string studentId)
        {
            InitializeComponent();
            _studentId = studentId;
            LoadStudentDetails();
        }

        private void LoadStudentDetails()
        {
            var student = DataManager.GetStudentById(_studentId);
            var progress = DataManager.GetStudentProgress(_studentId);

            // Set welcome message and personal information
            welcomeLabel.Text = $"Welcome, {student.FirstName} {student.LastName}!";
            emailLabel.Text = $"Email: {student.Email}";
            birthdayLabel.Text = $"Birthday: {student.DateOfBirth.ToShortDateString()}"; 
            genderLabel.Text = $"Gender: {student.Gender}";
            addressLabel.Text = $"Address: {student.Address}";
            countryLabel.Text = $"Country: {student.Country}";
            nationalityLabel.Text = $"Nationality: {student.Nationality}";

            // Set program information
            levelOfStudyLabel.Text = $"Level of Study: {student.LevelOfStudy}";
            programOfStudyLabel.Text = $"Program of Study: {student.ProgramOfStudy}";

            // Set progress information 
            var firstProgress = progress.FirstOrDefault();
            if (firstProgress != null)
            {
                courseLabel.Text = $"Course: {firstProgress.Course}";
                gradeLabel.Text = $"Grade: {firstProgress.Grade}";
                notesLabel.Text = $"Notes: {firstProgress.Notes}";
            }
        }
    }
}