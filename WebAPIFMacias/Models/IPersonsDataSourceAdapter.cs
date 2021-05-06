using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIFMacias.Models
{
    public interface IPersonsDataSourceAdapter
    {
        IEnumerable<Person> SelectAll();
        Person SelectPersonById(long id);
        IEnumerable<Person> SelectPersonsByColor(int color);
        bool InsertPerson(Person person);
    }
}
