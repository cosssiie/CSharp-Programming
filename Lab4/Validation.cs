using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab4
{
    public static class Validation
    {
        public static void ValidateEmail(string? email)
        { 
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidEmailException("Email is required.");

            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (!Regex.IsMatch(email, pattern))
                throw new InvalidEmailException("Email format is incorrect.");
        }

        public static void ValidateDateOfBirth(DateTime dateOfBirth)
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
