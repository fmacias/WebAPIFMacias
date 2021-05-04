using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIFMacias.Models
{
    public interface IPersonsDataSourceAdapter
    {
        IEnumerable<Person> GetAll();
        Person GetPersonById(long id);
        IEnumerable<Person> GetPersonsByColor(int color);
    }
}
