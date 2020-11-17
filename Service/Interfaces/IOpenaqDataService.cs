using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using Model.DTOs;

namespace Service.Interfaces
{
    public interface IOpenaqDataService
    {
        Task<IEnumerable<CountryDTO>> GetCountries();
        Task<IEnumerable<CountriesAndCitiesDTO>> GetCountriesAndCities();
        Task Initialise();
    }
}
