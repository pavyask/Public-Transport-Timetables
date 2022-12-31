﻿using Minimal_Web_API.Repositories;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Minimal_Web_API.Services
{
    public class TransportStopService
    {
        private readonly string filePath = $"{Environment.CurrentDirectory}/Resources/stops.json";

        private readonly TransportStopRepository _transportStopRepository;

        public TransportStopService(TransportStopRepository transportStopRepository)
        {
            _transportStopRepository = transportStopRepository;
        }

        public async Task<IEnumerable<TransportStop>> GetTransportStops()
        {
            RestResponse response = await GetResponseFromAPI(
                $"https://ckan.multimediagdansk.pl",
                $"/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/4c4025f0-01bf-41f7-a39f-d156d201b82b/download/stops.json");

            if (response.IsSuccessful)
            {
                SaveResponseToFile(response);
                var stops = ConvertResponseToStopsList(response).Where(s => s.Name != null);
                _transportStopRepository.AddTransportStops(stops);
                return stops;
            }

            return new List<TransportStop>();
        }

        private async Task<RestResponse> GetResponseFromAPI(string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.Get);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        private void SaveResponseToFile(RestResponse response)
        {
            var json = response.Content;
            File.WriteAllText(filePath, json);
        }

        private static IEnumerable<TransportStop> ConvertResponseToStopsList(RestResponse response)
        {
            var stopsData = JObject.Parse(response.Content);
            var stops = stopsData[DateTime.Now.ToString("yyyy-MM-dd")]["stops"].ToObject<TransportStop[]>().ToList();
            return stops;
        }

        public async Task<IEnumerable<TransportStop>> GetTransportStopsOfUser(string login)
        {
            return await _transportStopRepository.GetTransportStopsOfUserAsync(login);
        }

        public async Task<IEnumerable<>>
    }
}