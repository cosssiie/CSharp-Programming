using System;

namespace Lab4
{
    public class FutureDateOfBirthException : Exception
    {
        public FutureDateOfBirthException(string message) : base(message) { }
    }

    public class TooOldDateOfBirthException : Exception
    {
        public TooOldDateOfBirthException(string message) : base(message) { }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message) { }
    }
}

