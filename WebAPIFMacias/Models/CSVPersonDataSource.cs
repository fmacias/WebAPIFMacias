using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Text.RegularExpressions;

namespace WebAPI.Models
{
    public class CSVPersonDataSource : IPersonsDataSource
    {
        const int SURNAME_COLUMN = 0;
        const int NAME = 1;
        const int ZIPCODE_CITY = 2;
        const int COLOR = 3;

        private readonly List<Person> _persons;
        public CSVPersonDataSource(string CSVFileName)
        {
            _persons = ExtractPersons(CSVFileName);
        }
        private List<Person> ExtractPersons(string CSVFileName)
        {
            List<Person> persons = new List<Person>();
            long personId = 0;
            using (StreamReader reader = File.OpenText(CSVFileName))
            {
                using (CsvReader csvReader = new CsvReader(reader,false))
                {
                    while (csvReader.ReadNextRecord())
                    {
                        Person person = new Person(personId);
                        bool errorAtCSVRow = false;
                        try
                        {
                            ExtractSurname(csvReader, person);
                            ExtractName(csvReader, person);
                            ExtractLocation(csvReader, person);
                            ExtractColor(csvReader, person);
                        }
                        catch (LumenWorks.Framework.IO.Csv.MissingFieldCsvException)
                        {
                            errorAtCSVRow = true;
                        }
                        if (!errorAtCSVRow)
                        {
                            persons.Add(person);
                            personId++;
                        }
                    }
                }
            }
            return persons;
        }
        public IEnumerable<Person> GetAll()
        {
            return _persons;
        }
        public Person GetPersonById(long id)
        {
            return _persons.FirstOrDefault(person => person.Id == id);
        }
        public IEnumerable<Person> GetPersonsByColor(int color)
        {
            return _persons.FindAll(person => person.Color == (Color) color);
        }
        #region private
        static void ExtractSurname(CsvReader csvReader, Person person)
        {
            person.Surname = csvReader[SURNAME_COLUMN].ToString();
        }
        private static void ExtractColor(CsvReader csvReader, Person person)
        {
            Color color = (Color)Enum.Parse(typeof(Color), csvReader[COLOR].ToString());

            if (!Enum.IsDefined(typeof(Color), color))
                person.Color = Color.Unbekannt;
            else
                person.Color = color;

            person.ColorName = Enum.GetName(typeof(Color), person.Color);
        }
        private static void ExtractLocation(CsvReader csvReader, Person person)
        {
            string zipcode_city = csvReader[ZIPCODE_CITY].ToString().Trim();
            Regex digitsAtTheBeginRegexAsZipCode = new Regex(@"\d*");
            Regex wordsAsCity = new Regex(@"([a-zA-Z]|\s)+");
            person.Zipcode = digitsAtTheBeginRegexAsZipCode.Match(zipcode_city).Value ?? "";
            person.City = wordsAsCity.Match(zipcode_city).Value.Trim() ?? "";
        }
        private static void ExtractName(CsvReader csvReader, Person person)
        {
            person.Name = csvReader[NAME].ToString();
        }
        #endregion
    }
}
