using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Text.RegularExpressions;

namespace WebAPIFMacias.Models
{
    public class CSVPersonDataSourceAdapter : IPersonsDataSourceAdapter
    {
        const int SURNAME_COLUMN = 0;
        const int NAME_COLUMN = 1;
        const int ZIPCODE_CITY_COLUMN = 2;
        const int COLOR_COLUMN = 3;

        private List<Person> _persons;
        private readonly ISympleFactory<Person> _personsFactory;
        private readonly string _csvFileName;
        public CSVPersonDataSourceAdapter(string CSVFileName, ISympleFactory<Person> personFactory)
        {
            _persons = ExtractPersons(CSVFileName);
            _personsFactory = personFactory;
            _csvFileName = CSVFileName;
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
                        Person person = new Person();
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
                            person.Id = personId;
                            persons.Add(person);
                            personId++;
                        }
                    }
                }
                reader.Close();
            }
            return persons;
        }
        public IEnumerable<Person> SelectAll()
        {
            return _persons;
        }
        public Person SelectPersonById(long id)
        {
            return _persons.FirstOrDefault(person => person.Id == id) ?? _personsFactory.Create();
        }
        public IEnumerable<Person> SelectPersonsByColor(int color)
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
            Color color = (Color)Enum.Parse(typeof(Color), csvReader[COLOR_COLUMN].ToString());

            if (!Enum.IsDefined(typeof(Color), color))
                person.Color = Color.Unbekannt;
            else
                person.Color = color;
        }
        private static void ExtractLocation(CsvReader csvReader, Person person)
        {
            string zipcode_city = csvReader[ZIPCODE_CITY_COLUMN].ToString().Trim();
            Regex digitsAtTheBeginRegexAsZipCode = new Regex(@"\d*");
            Regex wordsAsCity = new Regex(@"([a-zA-Z]|\s)+");
            person.Zipcode = digitsAtTheBeginRegexAsZipCode.Match(zipcode_city).Value;
            person.City = wordsAsCity.Match(zipcode_city).Value.Trim();
        }
        private static void ExtractName(CsvReader csvReader, Person person)
        {
            person.Name = csvReader[NAME_COLUMN].ToString();
        }
        
        public bool InsertPerson(Person person)
        {
            bool result = false;
            try
            {
                using (StreamWriter writer = File.AppendText(_csvFileName))
                {
                    writer.WriteLine("{0}{1},{2},{3} {4},{5}","\n", person.Surname, person.Name,
                        person.Zipcode, person.City, (int) person.Color);
                    writer.Flush();
                    writer.Close();
                }
                _persons = ExtractPersons(_csvFileName);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion
    }
}
