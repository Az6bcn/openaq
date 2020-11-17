using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Model;
using Model.DTOs;
using Service.Interfaces;

namespace Service.Implementation
{
    public class OpenaqDataService : IOpenaqDataService
    {
        private readonly OpenaqService _openaqService;
        private readonly IMemoryCache _memoryCache;
        private const string CountriesCacheKey = "countries";
        private const string CitiesCacheKey = "cities";
        private const string MeasurementslocationsCacheKey = "locations";

        public OpenaqDataService(OpenaqService openaqService,
                                    IMemoryCache memoryCache)
        {
            _openaqService = openaqService;
            _memoryCache = memoryCache;
        }
        public async Task Initialise()
        {
            var cahceOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60 * 5));


            var countries = await _openaqService.GetCountries();
            _memoryCache.Set(CountriesCacheKey, countries, cahceOptions);

            var cities = await _openaqService.GetCities();
            _memoryCache.Set(CitiesCacheKey, cities, cahceOptions);

            var measurementsLocations = await _openaqService.GetMeasurementsLocations();
            _memoryCache.Set(MeasurementslocationsCacheKey, measurementsLocations, cahceOptions);
        }

        public async Task<IEnumerable<CountryDTO>> GetCountries()
        {
            IEnumerable<Country> countries;

            var res = (_memoryCache.TryGetValue(CountriesCacheKey, out countries));

            if (!res)
            {
                await Initialise();
                countries = _memoryCache.Get<IEnumerable<Country>>(CountriesCacheKey);

                return MapCountroDTO(countries);
            }

            return MapCountroDTO(countries);
        }

        public async Task<IEnumerable<CountriesAndCitiesDTO>> GetCountriesAndCities()
        {
            IEnumerable<Country> _countries;
            IEnumerable<City> _cities;
            IEnumerable<MeasurementLocation> _locations;

            var _hasCountries = (_memoryCache.TryGetValue(CountriesCacheKey, out _countries));
            var _hasCities = (_memoryCache.TryGetValue(CountriesCacheKey, out _cities));
            var _hasLocations = (_memoryCache.TryGetValue(CountriesCacheKey, out _locations));

            var hasAllNecessaryData = new List<bool> { _hasCountries, _hasCities, _hasLocations };

            if (hasAllNecessaryData.Any(x => !x))
            {
                await Initialise();
                _countries = _memoryCache.Get<IEnumerable<Country>>(CountriesCacheKey);
                _cities = _memoryCache.Get<IEnumerable<City>>(CitiesCacheKey);
                _locations = _memoryCache.Get<IEnumerable<MeasurementLocation>>(MeasurementslocationsCacheKey);
            }

            var response = MapCountriesCitiesAndLocation(_countries, _cities, _locations);

            return response;
        }


        private IEnumerable<CountriesAndCitiesDTO> MapCountriesCitiesAndLocation(IEnumerable<Country> countries, IEnumerable<City> cities,
        IEnumerable<MeasurementLocation> locations)
        {
            var _countries = countries.Select(x => new CountriesAndCitiesDTO
            {
                Name = x.Name,
                Code = x.Code,
                Cities = cities.Where(y => y.Country == x.Code).Select(z =>  
                {
                    var cityLocation = locations
                        .Where(l => l.City == z.Name && l.Country == x.Code)
                        .Select(n => new LocationDTO()
                        {
                            Name = n.Location
                        });

                    return new CityDTO { Name = z.Name, Locations = cityLocation };
                })
            });

            return _countries;
        }

        private IEnumerable<CountryDTO> MapCountroDTO(IEnumerable<Country> countries)
        {
            var response = countries
                .Select(x => new CountryDTO
                {
                    Name = x.Name,
                    Code = x.Code
                });

            return response;
        }
    }
}
