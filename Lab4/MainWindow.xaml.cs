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

                    // Обработка пользователей
                    await Task.Run(() => ProcessUsersAsync(loadedUsers, validationErrors));

                    // Обновляем UI
                    UserDataGrid.ItemsSource = null;
                    UserDataGrid.ItemsSource = _users;

                    // Показываем ошибки, если они есть
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
            var validUsers = new List<User>(); // Временный список для валидных пользователей
            var seenIds = new HashSet<int>(); // Для отслеживания уникальных ID

            foreach (var user in loadedUsers)
            {
                var errorMessages = new List<string>();

                try
                {
                    // Проверка уникальности ID
                    if (!seenIds.Add(user.ID))
                    {
                        throw new DuplicateIdException($"ID {user.ID} is already used by another user.");
                    }

                    // Создаем объект Person
                    Person person = user.DateOfBirth.HasValue
                        ? new Person(user.Name, user.Surname, user.Email, user.DateOfBirth.Value)
                        : new Person(user.Name, user.Surname, user.Email);

                    // Создаем PersonInfoManager
                    var manager = new PersonInfoManager(person);

                    // Вычисляем значения
                    user.Age = manager.GetAge();
                    user.IsAdult = manager.IsAdult();
                    user.SunSign = manager.GetSunSign();
                    user.ChineseSign = manager.GetChineseSign();
                    user.IsBirthday = manager.IsBirthday();

                    // Если ошибок нет, добавляем пользователя в список
                    validUsers.Add(user);
                }
                catch (DuplicateIdException ex)
                {
                    errorMessages.Add(ex.Message);
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

                // Если есть ошибки, записываем их для этого пользователя
                if (errorMessages.Count > 0)
                {
                    validationErrors.Add($"User ID {user.ID}:\n  - " + string.Join("\n  - ", errorMessages));
                }
            }

            // Обновляем основной список только валидными пользователями
            _users.Clear();
            _users.AddRange(validUsers);
        }
    }
}