using System;
using WebAPIFMacias.Models;

namespace WebAPIFMacias
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public Color Color { get; set; }
        public string ColorName => Enum.GetName(typeof(Color), Color);
    }
}

