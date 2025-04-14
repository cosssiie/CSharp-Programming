using System;
using Newtonsoft.Json;

namespace Lab4
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [JsonIgnore]
        public int? Age { get; set; }
        [JsonIgnore]
        public bool? IsAdult { get; set; }
        [JsonIgnore]
        public string SunSign { get; set; }
        [JsonIgnore]
        public string ChineseSign { get; set; }
        [JsonIgnore]
        public bool? IsBirthday { get; set; }
    }
}