using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lidhja
{
    public partial class StudentsListPage : ContentPage
    {
        public ObservableCollection<Student> Students { get; private set; }

        public StudentsListPage()
        {
            InitializeComponent();

            // Fetch all students from the database
            LoadStudents();

            BindingContext = this;

            // Subscribe to the message
            WeakReferenceMessenger.Default.Register<StudentUpdatedMessage>(this, (r, m) =>
            {
                DisplayAlert("Update Successful", m.Value, "OK");
                LoadStudents();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadStudents(); // Refresh the list whenever the page appears
        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            WeakReferenceMessenger.Default.Unregister<StudentUpdatedMessage>(this);

            // Unsubscribe from the StudentUpdatedMessage
            //MessagingCenter.Unsubscribe<EditStudentPage, StudentUpdatedMessage>(this, nameof(StudentUpdatedMessage));
        }



        private void LoadStudents()
        {
            using (var context = new DatabaseContext())
            {
                // Fetch all students from the database and convert to ObservableCollection
                Students = new ObservableCollection<Student>(context.Students.ToList());
            }

            // Bind the CollectionView to the ObservableCollection of students
            studentsCollectionView.ItemsSource = Students;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var student = button.CommandParameter as Student;
                if (student != null)
                {
                    await Navigation.PushAsync(new EditStudentPage(student));
                }
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                bool answer = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this student?", "Yes", "Cancel");
                if (answer)
                {
                    using (var context = new DatabaseContext())
                    {
                        // Remove the student from the database
                        context.Students.Remove(student);
                        context.SaveChanges();
                    }

                    // Refresh the list to reflect the deletion
                    LoadStudents();
                }
            }
        }
    }
}
