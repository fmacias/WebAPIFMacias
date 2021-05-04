using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LumenWorks.Framework.IO.Csv;
namespace WebAPIFMacias.Models
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly IPersonsDataSourceAdapter _personDataSource;
        public PersonsRepository(IPersonsDataSourceAdapter personDataSource)
        {
            _personDataSource = personDataSource;
        }
        public IEnumerable<Person> GetAll()
        {
            return _personDataSource.GetAll();
        }

        public Person GetPerson(long id)
        {
            return _personDataSource.GetPersonById(id);
        }

        public IEnumerable<Person> GetPersonsByColor(int color)
        {
            return _personDataSource.GetPersonsByColor(color);
        }
    }
}
