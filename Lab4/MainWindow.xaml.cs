using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        private List<User> _users;

        public MainWindow()
        {
            InitializeComponent();
            _users = new List<User>();
            LoadJsonDataAsync();
            UserDataGrid.ItemsSource = _users;
        }

        private async void LoadJsonDataAsync()
        {
            try
            {
                string jsonFilePath = "users.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = await File.ReadAllTextAsync(jsonFilePath);
                    var loadedUsers = JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();

                    var validationErrors = new List<string>();

                    await Task.Run(() => ProcessUsersAsync(loadedUsers, validationErrors));

                    UserDataGrid.ItemsSource = null;
                    UserDataGrid.ItemsSource = _users;

                    if (validationErrors.Count > 0)
                    {
                        string errorMessage = "Found invalid data for the following users:\n\n" + string.Join("\n", validationErrors);
                        MessageBox.Show(errorMessage, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    _users = new List<User>();
                    MessageBox.Show("JSON file not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                _users = new List<User>();
                MessageBox.Show($"Error loading JSON: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProcessUsersAsync(List<User> loadedUsers, List<string> validationErrors)
        {
            var validUsers = new List<User>();

            foreach (var user in loadedUsers)
            {
                var errorMessages = new List<string>();

                try
                {
                    Person person = user.DateOfBirth.HasValue
                        ? new Person(user.Name, user.Surname, user.Email, user.DateOfBirth.Value)
                        : new Person(user.Name, user.Surname, user.Email);

                    var manager = new PersonInfoManager(person);

                    user.Age = manager.GetAge();
                    user.IsAdult = manager.IsAdult();
                    user.SunSign = manager.GetSunSign();
                    user.ChineseSign = manager.GetChineseSign();
                    user.IsBirthday = manager.IsBirthday();

                    validUsers.Add(user);
                }
                catch (FutureDateOfBirthException ex)
                {
                    errorMessages.Add($"Date of Birth is in the future: {ex.Message}");
                }
                catch (TooOldDateOfBirthException ex)
                {
                    errorMessages.Add($"Date of Birth is too old: {ex.Message}");
                }
                catch (InvalidEmailException ex)
                {
                    errorMessages.Add($"Invalid Email: {ex.Message}");
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"Unexpected error: {ex.Message}");
                }

                if (errorMessages.Count > 0)
                {
                    validationErrors.Add($"User ID {user.ID}:\n  - " + string.Join("\n  - ", errorMessages));
                }
            }
            _users.Clear();
            _users.AddRange(validUsers);
        }
    }
}