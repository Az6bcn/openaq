using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model;

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

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var countries = await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(responseStream);
                return countries;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Something went wrong getting countries");
                return new List<Country>();
            }
        }

        public async Task<IEnumerable<CitiesInPlatform>> GetCities()
        {
            try
            {
                var response = await _client.GetAsync("cities/?limit=2000");
                response.EnsureSuccessStatusCode();

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var cities = await JsonSerializer.DeserializeAsync<IEnumerable<CitiesInPlatform>>(responseStream);
                return cities;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Something went wrong getting cities");
                return new List<CitiesInPlatform>();
            }
        }

        public async Task<IEnumerable<MeasurementLocation>> GetMeasurementsLocations()
        {
            try
            {
                var response = await _client.GetAsync("locations/?limit=2000");
                response.EnsureSuccessStatusCode();

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var measurementLocations = await JsonSerializer.DeserializeAsync<IEnumerable<MeasurementLocation>>(responseStream);
                return measurementLocations;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Something went wrong getting measurements locations");
                return new List<MeasurementLocation>();
            }
        }

        public async Task<IEnumerable<LocationMeasurement>> GetLocationData(int id)
        {
            try
            {
                var response = await _client.GetAsync($"locations/{id}?limit=2000");
                response.EnsureSuccessStatusCode();

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var locationData = await JsonSerializer.DeserializeAsync<IEnumerable<LocationMeasurement>>(responseStream);
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
