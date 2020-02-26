using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InnerCore.Api.Kaiterra.Exception;
using InnerCore.Api.Kaiterra.Models;
using InnerCore.Api.Kaiterra.Models.Basic;
using Newtonsoft.Json;

namespace InnerCore.Api.Kaiterra
{
    public class KaiterraBasicClient
    {
        private readonly string _accessKey;

        private HttpClient _httpClient;

        private bool _validateCertificate;

        public KaiterraBasicClient(string accessKey)
        {
            if (accessKey == null)
                throw new ArgumentNullException(nameof(accessKey));

            _accessKey = accessKey;
        }

        public bool ValidateCertificate
        {
            get => _validateCertificate;
            set
            {
                _validateCertificate = value;
                _httpClient = null;
            }
        }

        public async Task<SensorReading> GetLaserEggDetails(string deviceId)
        {
            if (deviceId == null)
            {
                throw new ArgumentNullException(nameof(deviceId));
            }

            var client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.GetAsync(new Uri($"{Constants.ENDPOINT}/lasereggs/{deviceId}?key={_accessKey}")).ConfigureAwait(false);

            var laserEggDetails = await HandleResponseAsync<LaserEgg>(response);

            return new SensorReading()
            {
                DeviceId = laserEggDetails.Id,
                DeviceType = DeviceType.LaserEgg,
                Pm10 = laserEggDetails.Aqi?.Data.Pm10,
                Pm25 = laserEggDetails.Aqi?.Data.Pm25,
                RelativeHumidity = laserEggDetails.Aqi?.Data.RelativeHumidity,
                Temperature = laserEggDetails.Aqi?.Data.Temperature,
                TotalVolatileOrganicCompounds = laserEggDetails.Aqi?.Data.TotalVolatileOrganicCompounds,
                TimeStamp = laserEggDetails.Aqi?.TimeStamp
            };
        }

        public async Task<SensorReading> GetSenseEdgeDetails(string deviceId)
        {
            if (deviceId == null)
            {
                throw new ArgumentNullException(nameof(deviceId));
            }

            var client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.GetAsync(new Uri($"{Constants.ENDPOINT}/sensedges/{deviceId}?key={_accessKey}")).ConfigureAwait(false);

            var senseEdgeDetails = await HandleResponseAsync<SenseEdge>(response);

            return new SensorReading()
            {
                DeviceId = senseEdgeDetails.Id,
                DeviceType = DeviceType.SenseEdege,
                Pm10 = senseEdgeDetails.Data?.Pm10,
                Pm25 = senseEdgeDetails.Data?.Pm25,
                CO2 = senseEdgeDetails.Data?.CO2,
                RelativeHumidity = senseEdgeDetails.Data?.RelativeHumidity,
                Temperature = senseEdgeDetails.Data?.Temperature,
                TotalVolatileOrganicCompounds = senseEdgeDetails.Data?.TotalVolatileOrganicCompounds,
                TimeStamp = senseEdgeDetails.Data?.TimeStamp
            };
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(content);
                case HttpStatusCode.BadRequest: throw new DeviceNotFoundException();
                case HttpStatusCode.Unauthorized: throw new InvalidKeyException();
                default: throw new InvalidResponseException(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        private Task<HttpClient> GetHttpClient()
        {
            // return per-thread HttpClient
            if (_httpClient == null)
            {
                var handler = new HttpClientHandler();
                if (!ValidateCertificate)
                {
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, cetChain, policyErrors) => true;
                }

                _httpClient = new HttpClient(handler);
                _httpClient.Timeout = TimeSpan.FromSeconds(Constants.BASIC_TIMEOUT_IN_SECONDS);
            }

            return Task.FromResult(_httpClient);
        }
    }
}
