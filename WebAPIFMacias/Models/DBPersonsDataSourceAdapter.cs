using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIFMacias.Models
{
    public class DBPersonsDataSourceAdapter : IPersonsDataSourceAdapter
    {
        private readonly PersonsContext _personsContext;
        private readonly ISympleFactory<Person> _personsFactory;
        public DBPersonsDataSourceAdapter(PersonsContext personsContext, ISympleFactory<Person> personFactory)
        {
            _personsContext = personsContext;
            _personsFactory = personFactory;
        }

        public IEnumerable<Person> SelectAll()
        {
            return _personsContext.Persons;
        }

        public Person SelectPersonById(long id)
        {
            return _personsContext.Persons.Where(person => person.Id == id).FirstOrDefault<Person>() ?? _personsFactory.Create();
        }

        public IEnumerable<Person> SelectPersonsByColor(int color)
        {
            return _personsContext.Persons.Where(person => person.Color == (Color)color).ToList<Person>();
        }
        public bool InsertPerson(Person person)
        {
            _personsContext.Add(person);
            
            if (_personsContext.SaveChanges() != 1)
                return false;

            return true;
        }
    }
}
