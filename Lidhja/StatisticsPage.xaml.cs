using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class StatisticsPage : ContentPage
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void OnGenderStatisticsClicked(object sender, EventArgs e)
        {
            // Calculate and display gender statistics
            var genderStats = DataManager.GetGenderStatistics();
            statisticsLabel.Text = $" {genderStats}";
           
        }

        private void OnNationalityStatisticsClicked(object sender, EventArgs e)
        {
            // Calculate and display nationality statistics
            var nationalityStats = DataManager.GetNationalityStatistics();
            statisticsLabel.Text = $"Nationality Statistics: {nationalityStats}";
        }

        private void OnProgramOfStudyStatisticsClicked(object sender, EventArgs e)
        {
            // Calculate and display program of study statistics
            var programStats = DataManager.GetProgramOfStudyStatistics();
            statisticsLabel.Text = $"Program of Study Statistics: {programStats}";
        }
        private void OnLevelOfStudyStatisticsClicked(object sender, EventArgs e)
        {
            // Calculate and display program of study statistics
            var levelStats = DataManager.GetLevelOfStudyStatistics();
            statisticsLabel.Text = $"Level of Study Statistics: {levelStats}";
        }

        
        
    }
}
