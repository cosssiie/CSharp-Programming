using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lab2
{
    class Person
    {
        private string _name { get; }
        private string _surname { get;}
        private string _email { get; }
        private DateTime _dateOfBirth { get; }


        public Person(string name, string surname, string email, DateTime dateOfBirth)
        {
            this._name = name;
            this._surname = surname;
            this._email = email;
            this._dateOfBirth = dateOfBirth;
        }

        public Person(string name, string surname, string email)
        {
            this._name = name;
            this._surname = surname;
            this._email = email;
        }

        public Person(string name, string surname, DateTime dateOfBirth)
        {
            this._name = name;
            this._surname = surname;
            this._dateOfBirth = dateOfBirth;
        }
    }
}
