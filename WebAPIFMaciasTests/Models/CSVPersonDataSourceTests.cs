using WebAPI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Models.Tests
{
    [TestFixture()]
    public class CSVPersonDataSourceTests
    {
        [Test()]
        public void CSVPersonDataSourceTest()
        {
            Assert.IsInstanceOf<IPersonsDataSourceAdapter>(new CSVPersonDataSourceAdapter("persons.csv"));
        }
        [Test()]
        public void regexTEst()
        {
            string zipCityWithNonWordChars = "32132 Schweden asdf - ☀";
            string zipCityWithNonWordChars2 = "32132 Schweden - ☀";
            Regex digitsAtTheBeginRegexAsZipCode = new Regex(@"\d*");
            Regex wordsAsCity = new Regex(@"([a-zA-Z]|\s)+");
            Assert.AreEqual("32132",digitsAtTheBeginRegexAsZipCode.Match(zipCityWithNonWordChars).Value);
            Assert.AreEqual("Schweden asdf", wordsAsCity.Match(zipCityWithNonWordChars).Value.Trim());
            Assert.AreEqual("Schweden", wordsAsCity.Match(zipCityWithNonWordChars2).Value.Trim());
        }
        /// <summary>
        /// Get all rows are parseable by the CSV Parser.
        /// rows 9 and 10 are not parseable rows.
        /// 
        /// </summary>
        [Test()]
        public void GetAllTest()
        {
            CSVPersonDataSourceAdapter personDataSource = new CSVPersonDataSourceAdapter("persons.csv");
            List<Person> persons = personDataSource.GetAll() as List<Person>;
            Assert.IsTrue(persons.Count == 10, "because rows 9 and 10 are not parseable!");
            Person firstPerson = persons[0];
            Person lastPerson = persons[9];
            Assert.AreEqual(string.Format("{0} {1} {2} {3} {4} {5}", firstPerson.Id,
                firstPerson.Name, firstPerson.Surname, firstPerson.Zipcode, firstPerson.City, firstPerson.Color),
                "0 Hans Müller 67742 Lauterecken Grün");
            Assert.AreEqual(string.Format("{0} {1} {2} {3} {4} {5}", lastPerson.Id,
                lastPerson.Name, lastPerson.Surname, lastPerson.Zipcode, lastPerson.City, lastPerson.Color),
                "9 Klaus Klaussen 43246 Hierach Violett");
        }

        [Test()]
        public void GetPersonByIdTest()
        {
            CSVPersonDataSourceAdapter personDataSource = new CSVPersonDataSourceAdapter("persons.csv");
            Person person = personDataSource.GetPersonById(3);
            Assert.AreEqual(string.Format("{0} {1} {2} {3} {4} {5}", person.Id,
                person.Name, person.Surname, person.Zipcode, person.City, person.Color),
                "3 Johnny Johnson 88888 made up Rot");
        }

        [Test()]
        public void GetPersonsByColorTest()
        {
            CSVPersonDataSourceAdapter personDataSource = new CSVPersonDataSourceAdapter("persons.csv");
            List<Person> persons = personDataSource.GetPersonsByColor((int)Color.Rot) as List<Person>;
            Assert.IsTrue(persons.Count == 2, "Two persons with red favorite color");
        }
    }
}