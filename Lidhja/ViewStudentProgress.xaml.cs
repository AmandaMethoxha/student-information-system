using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Lidhja
{
    public partial class ViewStudentProgress : ContentPage
    {
        public ObservableCollection<StudentProgressViewModel> StudentProgresses { get; private set; }

        public ViewStudentProgress()
        {
            InitializeComponent();
            StudentProgresses = new ObservableCollection<StudentProgressViewModel>();
            LoadStudentProgresses();
            BindingContext = this;

            
        }

        private void LoadStudentProgresses()
        {
            try
            {
                StudentProgresses.Clear();
                var progressList = DataManager.GetCombinedStudentProgress();
                foreach (var progress in progressList)
                {
                    StudentProgresses.Add(progress);
                }
                studentprogressesCollectionView.ItemsSource = StudentProgresses;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to load student progresses: " + ex.Message);
            }
        }


        //edit student progress
        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is StudentProgressViewModel viewModel)
            {
                // Convert ViewModel to Model 
                var studentProgress = ConvertViewModelToModel(viewModel);
                await Navigation.PushAsync(new EditStudentProgressPage(studentProgress));
            }
            else
            {
                await DisplayAlert("Error", "Invalid data for editing.", "OK");
            }
        }

        private StudentProgress ConvertViewModelToModel(StudentProgressViewModel viewModel)
        {
            try
            {
                return new StudentProgress
                {
                    Id = viewModel.Id, 
                    StudentId = viewModel.StudentId,
                    FullName = viewModel.FullName,
                    Course = viewModel.Course,
                    Grade = viewModel.Grade,
                    Notes = viewModel.Notes
                };
            }
            catch (FormatException ex)
            {
                
                Debug.WriteLine($"Error converting ViewModel to Model: {ex.Message}");
                return null; 
            }
        }



        //delete student progress
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is StudentProgressViewModel studentProgressViewModel)
            {
                bool answer = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this student progress?", "Yes", "No");
                if (answer)
                {
                    try
                    {
                        DataManager.DeleteStudentProgress(studentProgressViewModel.Id); 
                        LoadStudentProgresses(); // Refresh list
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", "A problem occurred: " + ex.Message, "OK");
                    }
                }
            }
        }









    }
}
