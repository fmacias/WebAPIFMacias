using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIFMacias.Models
{
    public interface ISympleFactory<T>
    {
        T Create();
    }
}
