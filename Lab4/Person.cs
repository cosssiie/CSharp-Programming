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

            Validation.ValidateDateOfBirth(dateOfBirth);
            DateOfBirth = dateOfBirth;

            if (!string.IsNullOrWhiteSpace(email))
            {
                Validation.ValidateEmail(email);
                Email = email;
            }
        }

        public Person(string name, string surname, string? email)
        {
            Name = name;
            Surname = surname;

            if (!string.IsNullOrWhiteSpace(email))
            {
                Validation.ValidateEmail(email);
                Email = email;
            }

            DateOfBirth = null;
        }

        public Person(string name, string surname, DateTime dateOfBirth)
        {
            Name = name;
            Surname = surname;

            Validation.ValidateDateOfBirth(dateOfBirth);
            DateOfBirth = dateOfBirth;

            Email = null;
        }
    }
}