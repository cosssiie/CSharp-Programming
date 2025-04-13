using System;

namespace Lab4
{
    public class Person
    {
        public string Name { get; }
        public string Surname { get; }
        public string? Email { get; }
        public DateTime? DateOfBirth { get; }

        public Person(string name, string surname, string? email, DateTime dateOfBirth)
        {
            Name = name;
            Surname = surname;

            ValidateDateOfBirth(dateOfBirth);
            DateOfBirth = dateOfBirth;

            if (!string.IsNullOrWhiteSpace(email))
            {
                EmailValidation.Validate(email);
                Email = email;
            }
        }

        public Person(string name, string surname, string? email)
        {
            Name = name;
            Surname = surname;

            if (!string.IsNullOrWhiteSpace(email))
            {
                EmailValidation.Validate(email);
                Email = email;
            }

            DateOfBirth = null;
        }

        public Person(string name, string surname, DateTime dateOfBirth)
        {
            Name = name;
            Surname = surname;

            ValidateDateOfBirth(dateOfBirth);
            DateOfBirth = dateOfBirth;

            Email = null;
        }

        private void ValidateDateOfBirth(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age))
                age--;

            if (age < 0)
                throw new FutureDateOfBirthException("The date of birth cannot be in the future.");
            if (age > 135)
                throw new TooOldDateOfBirthException("The date of birth is too old.");
        }
    }
}