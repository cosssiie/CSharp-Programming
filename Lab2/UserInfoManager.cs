using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class UserInfoManager
    {
        private readonly Person _person;


        public int GetAge()
        {
            if (!_person.DateOfBirth.HasValue) return null;

            DateTime today = DateTime.Today;
            DateTime date = birthDate.Value;
            int age = today.Year - date.Year;

            if (date > today.AddYears(-age))
                age--;

            if (age < 0 || age > 135)
                return null;

            return age;
        }

        public bool IsAdult()
        {
            return GetAge() >= 18;
        }

        public string getSunSign()
        {
            return "";
        }

        public string getChineseSign()
        {
            return "";
        }

        public bool IsBirthday()
        {
            if (!_person.DateOfBirth.HasValue)
                return false;

            DateTime today = DateTime.Today;
            return _person.DateOfBirth.Value.Day == today.Day &&
                   _person.DateOfBirth.Value.Month == today.Month;
        }
    }
}
