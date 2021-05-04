using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public interface IPersonsDataSource
    {
        IEnumerable<Person> GetAll();
        Person GetPersonById(long id);
        IEnumerable<Person> GetPersonsByColor(int color);
    }
}
