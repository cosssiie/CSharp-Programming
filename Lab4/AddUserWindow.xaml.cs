using System;
using System.Windows;

namespace Lab4
{
    public partial class AddUserWindow : Window
    {
        public User User { get; private set; }

        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(IdTextBox.Text, out int id) || id <= 0)
                    throw new Exception("ID must be a positive integer.");

                var user = new User
                {
                    ID = id,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    Email = string.IsNullOrWhiteSpace(EmailTextBox.Text) ? null : EmailTextBox.Text,
                    DateOfBirth = DateOfBirthPicker.SelectedDate
                };

                var person = user.DateOfBirth.HasValue
                    ? new Person(user.Name, user.Surname, user.Email, user.DateOfBirth.Value)
                    : new Person(user.Name, user.Surname, user.Email);

                User = user;
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = ex.Message;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}