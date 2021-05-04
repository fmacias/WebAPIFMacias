using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LumenWorks.Framework.IO.Csv;
namespace WebAPIFMacias.Models
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly IPersonsDataSourceAdapter _personDataSourceAdapter;
        public PersonsRepository(IPersonsDataSourceAdapter personDataSourceAdapter)
        {
            _personDataSourceAdapter = personDataSourceAdapter;
        }
        public IEnumerable<Person> GetAll()
        {
            return _personDataSourceAdapter.GetAll();
        }

        public Person GetPerson(long id)
        {
            return _personDataSourceAdapter.GetPersonById(id);
        }

        public IEnumerable<Person> GetPersonsByColor(int color)
        {
            return _personDataSourceAdapter.GetPersonsByColor(color);
        }
    }
}
