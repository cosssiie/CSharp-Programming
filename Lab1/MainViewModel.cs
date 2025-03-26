using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Lab1.Models;

namespace Lab1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ZodiacModel _zodiacModel;
        private DateTime? _selectedDate;
        private string _westernZodiac;
        private string _chineseZodiac;

        public MainViewModel()
        {
            _zodiacModel = new ZodiacModel();
            ShowInfoCommand = new RelayCommand(ExecuteShowInfo);
        }

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public string WesternZodiac
        {
            get => _westernZodiac;
            set
            {
                _westernZodiac = value;
                OnPropertyChanged();
            }
        }

        public string ChineseZodiac
        {
            get => _chineseZodiac;
            set
            {
                _chineseZodiac = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowInfoCommand { get; }

        private void ExecuteShowInfo(object parameter)
        {
            WesternZodiac = string.Empty;
            ChineseZodiac = string.Empty;

            if (!SelectedDate.HasValue)
            {
                MessageBox.Show("Please enter your date of birth", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int? age = _zodiacModel.CalculateAge(SelectedDate);
            if (age.HasValue)
            {
                DateTime date = SelectedDate.Value;
                WesternZodiac = $"Western Zodiac: {_zodiacModel.GetWesternZodiac(date)}";
                ChineseZodiac = $"Chinese Zodiac: {_zodiacModel.GetChineseZodiac(date.Year)}";

                string message = $"Your age is {age}";
                if (DateTime.Today.Month == date.Month && DateTime.Today.Day == date.Day)
                {
                    message = $"Happy Birthday! You are {age} years old today!";
                }
                MessageBox.Show(message, "Age Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error: Invalid age!", "Invalid age", MessageBoxButton.OK, MessageBoxImage.Error);
                SelectedDate = null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}