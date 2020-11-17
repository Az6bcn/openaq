using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Model;
using Service.Interfaces;

namespace Service.Implementation
{
    public class OpenaqDataService : IOpenaqDataService
    {
        private readonly OpenaqService _openaqService;
        private readonly IMemoryCache _memoryCache;
        private const string CountriesCacheKey = "countries";
        private const string CitiesCacheKey = "countries";
        private const string MeasurementslocationsCacheKey = "countries";

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
            _memoryCache.Set(CitiesCacheKey, countries, cahceOptions);

            var measurementsLocations = await _openaqService.GetMeasurementsLocations();
            _memoryCache.Set(MeasurementslocationsCacheKey, countries, cahceOptions);
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            IEnumerable<Country> response;

            var res = (_memoryCache.TryGetValue(CountriesCacheKey, out IEnumerable<Country> countries));

            if (!res)
            {
                await Initialise();
                response = _memoryCache.Get<IEnumerable<Country>>(CountriesCacheKey);

                return response;
            }

            return countries;
        }
    }
}
