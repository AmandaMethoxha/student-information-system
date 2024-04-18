using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Lidhja
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            if (Authenticate(UsernameEntry.Text, PasswordEntry.Text))
            {
                // Navigate to the StaffDashboardPage after successful login
                Application.Current.MainPage = new NavigationPage(new StaffDashboardPage());
            }
            else
            {
                DisplayAlert("Error", "Invalid credentials!", "OK");
            }
        }

        private bool Authenticate(string username, string password)
        {
            
            List<User> users = new List<User>
            {
                new User { Username = "admin", Password = "admin123", Role = UserRole.Admin },
                
            };

            return users.Any(user => user.Username == username && user.Password == password);
        }
    }
}