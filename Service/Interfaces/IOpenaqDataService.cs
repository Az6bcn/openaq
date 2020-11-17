using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Service.Interfaces
{
    public interface IOpenaqDataService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task Initialise();
    }
}
