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
private async void OnUserDetailsClicked(object sender, EventArgs e)
{
    string action = await DisplayActionSheet("User Details", "Cancel", null, "View Details", "Edit Details");

    if (action == "View Details")
    {
        await DisplayAlert("User Details", $"Username: {_username}", "OK");
    }
    else if (action == "Edit Details")
    {
        string newName = await DisplayPromptAsync("Edit Details", "Enter new username:");

        if (!string.IsNullOrWhiteSpace(newName))
        {
            _dbService.UpdateUserName(newName); // Update DB
            _username = newName;
            UsernameLabel.Text = $"Welcome, {_username}";
            await DisplayAlert("Success", "Username updated successfully!", "OK");
        }
    }
}
// Account Details Logic
  private async void OnAccountDetailsClicked(object sender, EventArgs e)
  {
      var user = _dbService.GetUser();
      string accountDetails = $"Account Holder: {user.Username}\nBalance: {user.Balance:C}";
      await DisplayAlert("Account Details", accountDetails, "OK");
  }
