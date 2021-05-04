using System;
using WebAPIFMacias.Models;

namespace WebAPIFMacias
{
    public class Person
    {
        private readonly long _id;
        public Person(long id)
        {
            _id = id;
        }
        public long Id => _id;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public Color Color { get; set; }
        public string ColorName { get; set; }
    }
}
