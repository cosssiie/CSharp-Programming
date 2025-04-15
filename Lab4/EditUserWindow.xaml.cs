using System;
using System.Windows;

namespace Lab4
{
    public partial class EditUserWindow : Window
    {
        public User User { get; private set; }

        public EditUserWindow(User user)
        {
            InitializeComponent();
            User = new User
            {
                ID = user.ID,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            IdTextBox.Text = User.ID.ToString();
            NameTextBox.Text = User.Name;
            SurnameTextBox.Text = User.Surname;
            EmailTextBox.Text = User.Email;
            DateOfBirthPicker.SelectedDate = User.DateOfBirth;
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

                User.Name = NameTextBox.Text.Trim();
                User.Surname = SurnameTextBox.Text.Trim();
                User.Email = EmailTextBox.Text.Trim();
                User.DateOfBirth = DateOfBirthPicker.SelectedDate;

                var person = new Person(User.Name, User.Surname, User.Email, User.DateOfBirth.Value);

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