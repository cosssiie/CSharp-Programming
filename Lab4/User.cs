using System;

namespace Lab4
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; } 
        public bool? IsAdult { get; set; } 
        public string SunSign { get; set; }
        public string ChineseSign { get; set; } 
        public bool? IsBirthday { get; set; }
    }
}