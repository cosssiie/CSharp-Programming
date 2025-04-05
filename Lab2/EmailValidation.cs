using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab2
{
    public static class EmailValidation
    {
        public static void Validate(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidEmailException("Email is required.");

            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (!Regex.IsMatch(email, pattern))
                throw new InvalidEmailException("Email format is incorrect.");
        }
    }
}
