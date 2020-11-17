using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model;
using Model.DTOs;
using Model.DTOs.ExternalApiResponseDTO;
using Newtonsoft.Json;

namespace Service
{
    public class OpenaqService
    {
        private readonly HttpClient _client;
        private readonly ILogger<OpenaqService> _logger;

        public OpenaqService(HttpClient client, ILogger<OpenaqService> logger)
        {
            client.BaseAddress = new Uri("https://api.openaq.org/v1/");
            _client = client;

            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            try
            {
                var response = await _client.GetAsync("countries/?limit=2000");
                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStringAsync();
              
                var result = JsonConvert.DeserializeObject<CountriesExternalAPIResponseDTO>(responseStream);

                return result.Results;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Something went wrong getting countries {ex.Message}");
                return new List<Country>();
            }
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            try
            {
                var response = await _client.GetAsync("cities/?limit=2000");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CitiesExternalAPIResponseDTO>(responseString);
                return result.Results ;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Something went wrong getting cities {ex.Message}");
                return new List<City>();
            }
        }

        public async Task<IEnumerable<MeasurementLocation>> GetMeasurementsLocations()
        {
            try
            {
                var response = await _client.GetAsync("locations/?limit=2000");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MeasurementsExternalAPIResponseDTO>(responseString);
                return result.Results;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Something went wrong getting measurements locations {ex.Message}");
                return new List<MeasurementLocation>();
            }
        }

        public async Task<IEnumerable<LocationMeasurement>> GetLocationData(int id)
        {
            try
            {
                var response = await _client.GetAsync($"locations/{id}?limit=2000");
                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStringAsync();
                var locationData = JsonConvert.DeserializeObject<IEnumerable<LocationMeasurement>>(responseStream);
                return locationData;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Something went wrong getting measurements locations");
                return new List<LocationMeasurement>();
            }
        }
    }
}
