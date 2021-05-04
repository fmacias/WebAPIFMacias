using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public interface IPersonsRepository
    {
        IEnumerable<Person> GetAll();
        Person GetPerson(long id);
        IEnumerable<Person> GetPersonsByColor(int color);
    }
}
