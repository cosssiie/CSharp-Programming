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
                ErrorTextBlock.Text = "";

                if (!int.TryParse(IdTextBox.Text, out int id) || id <= 0)
                    throw new Exception("ID must be a positive Integer.");

                if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                    throw new Exception("Name is required.");

                if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
                    throw new Exception("Surname is required.");

                if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                    throw new Exception("Email is required.");

                if (!DateOfBirthPicker.SelectedDate.HasValue)
                    throw new Exception("Date of Birth is required.");

                var user = new User
                {
                    ID = id,
                    Name = NameTextBox.Text.Trim(),
                    Surname = SurnameTextBox.Text.Trim(),
                    Email = EmailTextBox.Text.Trim(),
                    DateOfBirth = DateOfBirthPicker.SelectedDate
                };

                var person = new Person(user.Name, user.Surname, user.Email, user.DateOfBirth.Value);

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