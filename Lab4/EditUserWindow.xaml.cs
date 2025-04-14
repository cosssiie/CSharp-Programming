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
                User.Name = NameTextBox.Text;
                User.Surname = SurnameTextBox.Text;
                User.Email = string.IsNullOrWhiteSpace(EmailTextBox.Text) ? null : EmailTextBox.Text;
                User.DateOfBirth = DateOfBirthPicker.SelectedDate;

                var person = User.DateOfBirth.HasValue
                    ? new Person(User.Name, User.Surname, User.Email, User.DateOfBirth.Value)
                    : new Person(User.Name, User.Surname, User.Email);

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