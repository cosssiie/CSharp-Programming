using System;

namespace Lab1.Models
{
    public class ZodiacModel
    {
        public int? CalculateAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue) return null;

            DateTime today = DateTime.Today;
            DateTime date = birthDate.Value;
            int age = today.Year - date.Year;

            if (date > today.AddYears(-age))
                age--;

            if (age < 0 || age > 135)
                return null;

            return age;
        }

        public string GetWesternZodiac(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

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

        public string GetChineseZodiac(int year)
        {
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
    }
}