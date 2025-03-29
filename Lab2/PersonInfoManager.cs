using System;

namespace Lab2
{
    public class PersonInfoManager
    {
        private readonly Person _person;

        public PersonInfoManager(Person person)
        {
            _person = person ?? throw new ArgumentNullException(nameof(person));
        }

        public int? GetAge()
        {
            if (_person.DateOfBirth == null) return null;

            DateTime today = DateTime.Today;
            DateTime date = _person.DateOfBirth.Value;
            int age = today.Year - date.Year;

            if (date > today.AddYears(-age))
                age--;

            if (age < 0 || age > 135)
                return null;

            return age;
        }

        public bool IsAdult()
        {
            int? age = GetAge();
            return age.HasValue && age >= 18;
        }

        public string GetSunSign()
        {
            if (_person.DateOfBirth == null) return "";

            DateTime dob = _person.DateOfBirth.Value;
            int month = dob.Month;
            int day = dob.Day;

            return (month, day) switch
            {
                (1, <= 20) => "Capricorn",
                (1, _) => "Aquarius",
                (2, <= 20) => "Aquarius",
                (2, _) => "Pisces",
                (3, <= 20) => "Pisces",
                (3, _) => "Aries",
                (4, <= 19) => "Aries",
                (4, _) => "Taurus",
                (5, <= 20) => "Taurus",
                (5, _) => "Gemini",
                (6, <= 21) => "Gemini",
                (6, _) => "Cancer",
                (7, <= 22) => "Cancer",
                (7, _) => "Leo",
                (8, <= 22) => "Leo",
                (8, _) => "Virgo",
                (9, <= 22) => "Virgo",
                (9, _) => "Libra",
                (10, <= 22) => "Libra",
                (10, _) => "Scorpio",
                (11, <= 21) => "Scorpio",
                (11, _) => "Sagittarius",
                (12, <= 21) => "Sagittarius",
                _ => "Capricorn"
            };
        }

        public string GetChineseSign()
        {
            if (_person.DateOfBirth == null) return "";

            int year = _person.DateOfBirth.Value.Year;
            string[] chineseZodiac = new string[]
            {
                "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake",
                "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig"
            };

            int baseYear = 1900;
            int zodiacIndex = (year - baseYear) % 12;

            if (zodiacIndex < 0)
                zodiacIndex += 12;

            return chineseZodiac[zodiacIndex];
        }

        public bool IsBirthday()
        {
            if (_person.DateOfBirth == null) return false;

            DateTime today = DateTime.Today;
            return _person.DateOfBirth.Value.Day == today.Day &&
                   _person.DateOfBirth.Value.Month == today.Month;
        }
    }
}