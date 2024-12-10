using Microsoft.Maui.Controls;
using System;

namespace bankapp
{
    public partial class MainPage : ContentPage
    {
        private SQLiteService _dbService;
        private string _username = string.Empty; // Default value to handle nullability
        private decimal _balance = 0;

        public MainPage()
        {
            InitializeComponent();
            _dbService = new SQLiteService();
        }

        // Login Logic
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please fill in both fields.", "OK");
                return;
            }

            if (_dbService.ValidateUser(username, password))
            {
                // Switch to Home layout
                LoginLayout.IsVisible = false;
                HomeLayout.IsVisible = true;

                // Set the username and balance
                var user = _dbService.GetUser();
                _username = user.Username;
                _balance = user.Balance;
                UsernameLabel.Text = $"Welcome, {_username}";
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
