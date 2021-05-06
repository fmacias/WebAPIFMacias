using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIFMacias.Models
{
    public class PersonsFactory : ISympleFactory<Person>
    {
        public Person Create()
        {
            return new Person();
        }
    }
}
