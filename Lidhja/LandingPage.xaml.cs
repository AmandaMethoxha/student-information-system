namespace Lidhja
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private void OnStaffButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the Staff Login Page
            Navigation.PushAsync(new LoginPage());
        }

        private void OnStudentButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the Student Dashboard or Student Login Page
            
            Navigation.PushAsync(new StudentLoginPage());
        }
    }
}