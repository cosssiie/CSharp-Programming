using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Lab2
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private string? _name;
        private string? _surname;
        private string? _email;
        private DateTime? _dateOfBirth;
        private bool _isProceedButtonEnabled;
        private bool _isBusy;

        public event PropertyChangedEventHandler? PropertyChanged;

        public PersonViewModel()
        {
            ProceedCommand = new RelayCommand(async (parameter) => await ExecuteProceed(), CanExecuteProceed);
            UpdateButtonState();
        }

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                UpdateButtonState();
            }
        }

        public string? Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
                UpdateButtonState();
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                UpdateButtonState();
            }
        }

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
                UpdateButtonState();
            }
        }

        public bool IsProceedButtonEnabled
        {
            get => _isProceedButtonEnabled;
            set
            {
                _isProceedButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                UpdateButtonState();
            }
        }

        public ICommand ProceedCommand { get; }

        private async Task ExecuteProceed()
        {
            IsBusy = true;

            try
            {
                if (DateOfBirth.HasValue)
                {
                    Person person = new Person(Name!, Surname!, Email, DateOfBirth.Value);
                    PersonInfoManager manager = new PersonInfoManager(person);

                    int? age = await Task.Run(() => manager.GetAge());
                    await Task.Run(() => EmailValidation.Validate(Email));

                    string sunSign = await Task.Run(() => manager.GetSunSign());
                    string chineseSign = await Task.Run(() => manager.GetChineseSign());
                    bool isAdult = await Task.Run(() => manager.IsAdult());
                    bool isBirthday = await Task.Run(() => manager.IsBirthday());

                    string results =
                        $"Name: {Name}\n" +
                        $"Surname: {Surname}\n" +
                        $"Email: {Email}\n" +
                        $"Date of Birth: {DateOfBirth?.ToString("yyyy-MM-dd")}\n" +
                        $"Age: {age}\n" +
                        $"Is Adult: {isAdult}\n" +
                        $"Sun Sign: {sunSign}\n" +
                        $"Chinese Sign: {chineseSign}\n" +
                        $"Is Birthday: {isBirthday}";

                    if (isBirthday)
                    {
                        MessageBox.Show($"Happy Birthday! You are {age} years old today!");
                    }

                    MessageBox.Show(results, "Person Information");
                }
                else
                {
                    MessageBox.Show("Date of birth is required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FutureDateOfBirthException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (TooOldDateOfBirthException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (InvalidEmailException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Unexpected Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanExecuteProceed(object? parameter)
        {
            return IsProceedButtonEnabled && !IsBusy;
        }

        private void UpdateButtonState()
        {
            IsProceedButtonEnabled = !string.IsNullOrWhiteSpace(Name) &&
                                      !string.IsNullOrWhiteSpace(Surname) &&
                                      !string.IsNullOrWhiteSpace(Email) &&
                                      DateOfBirth.HasValue && !IsBusy;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Func<object?, Task> _executeAsync;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Func<object?, Task> executeAsync, Func<object?, bool>? canExecute = null)
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public async void Execute(object? parameter)
        {
            await _executeAsync(parameter);
        }
    }
}
