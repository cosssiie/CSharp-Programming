using System;
using System.Collections.Generic;
using System.IO;
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
            LoadJsonData();
            UserDataGrid.ItemsSource = _users;
        }

        private void LoadJsonData()
        {
            try
            {
                string jsonFilePath = "users_with_id.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);
                    _users = JsonConvert.DeserializeObject<List<User>>(jsonData);
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
    }
}