namespace Lab2
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
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public Person(string name, string surname, string? email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = null;
        }

        public Person(string name, string surname, DateTime dateOfBirth)
        {
            Name = name;
            Surname = surname;
            Email = null;
            DateOfBirth = dateOfBirth;
        }
    }
}