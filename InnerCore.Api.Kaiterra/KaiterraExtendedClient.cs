﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InnerCore.Api.Kaiterra.Exception;
using InnerCore.Api.Kaiterra.Models.Extended;
using Newtonsoft.Json;

namespace InnerCore.Api.Kaiterra
{
    public class KaiterraExtendedClient
    {
        private string _accessToken;

        private LoginRequest _credentials;

        private HttpClient _httpClient;

        private JsonSerializerSettings _serializerSettings =
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        private bool _validateCertificate;

        public bool ValidateCertificate
        {
            get => _validateCertificate;
            set
            {
                _validateCertificate = value;
                _httpClient = null;
            }
        }

        public async Task Login(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            _credentials = new LoginRequest { Email = email, Password = password };
            var client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.PostAsync(new Uri($"{Constants.ENDPOINT}/auth/login"), SerializeRequest(_credentials)).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidCredentialsException();
            }

            _accessToken = (await HandleResponseAsync<LoginResponse>(response)).AccessToken;
            ApplyAccesstoken();
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            CheckInitialized();
            try
            {
                return await GetDevicesInternal();
            }
            catch (InvalidKTokenException)
            {
                await Login(_credentials.Email, _credentials.Password);
                return await GetDevicesInternal();
            }
        }

        public async Task<Device> GetDevice(string deviceId)
        {
            if (deviceId == null)
            {
                throw new ArgumentNullException(nameof(deviceId));
            }

            CheckInitialized();

            try
            {
                return await GetDeviceInternal(deviceId);
            }
            catch (InvalidKTokenException)
            {
                await Login(_credentials.Email, _credentials.Password);
                return await GetDeviceInternal(deviceId);
            }
        }

        public async Task<History> GetCurrentValues(string deviceId)
        {
            if (deviceId == null)
            {
                throw new ArgumentNullException(nameof(deviceId));
            }

            CheckInitialized();

            try
            {
                return await GetCurrentValuesInternal(deviceId);
            }
            catch (InvalidKTokenException)
            {
                await Login(_credentials.Email, _credentials.Password);
                return await GetCurrentValuesInternal(deviceId);
            }
        }

        public async Task<History> GetHistoricValues(string deviceId, DateTimeOffset from, DateTimeOffset to, TimeSpan interval)
        {
            if (deviceId == null)
            {
                throw new ArgumentNullException(nameof(deviceId));
            }

            var formattedInterval = $"{(int)interval.TotalMinutes}m";
            var formattedFrom = from.ToString(Constants.DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
            var formattedTo = to.ToString(Constants.DATE_TIME_FORMAT, CultureInfo.InvariantCulture);

            CheckInitialized();

            try
            {
                return await GetHistoricValuesInternal(deviceId, formattedInterval, formattedFrom, formattedTo);
            }
            catch (InvalidKTokenException)
            {
                await Login(_credentials.Email, _credentials.Password);
                return await GetHistoricValuesInternal(deviceId, formattedInterval, formattedFrom, formattedTo);
            }
        }

        public async Task<History> GetHistoricValuesInternal(string deviceId, string from, string to, string interval)
        {
            var client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.GetAsync(new Uri($"{Constants.ENDPOINT}/account/me/device/{deviceId}/history?series={interval}&from={from}&to={to}")).ConfigureAwait(false);

            return await HandleResponseAsync<History>(response);
        }

        private async Task<IEnumerable<Device>> GetDevicesInternal()
        {
            var client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.GetAsync(new Uri($"{Constants.ENDPOINT}/account/me/device")).ConfigureAwait(false);

            return await HandleResponseAsync<IEnumerable<Device>>(response);
        }

        private async Task<Device> GetDeviceInternal(string deviceId)
        {
            var client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.GetAsync(new Uri($"{Constants.ENDPOINT}/account/me/device/{deviceId}")).ConfigureAwait(false);

            return await HandleResponseAsync<Device>(response);
        }

        private async Task<History> GetCurrentValuesInternal(string deviceId)
        {
            var client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.GetAsync(new Uri($"{Constants.ENDPOINT}/account/me/device/{deviceId}/real-time")).ConfigureAwait(false);

            return await HandleResponseAsync<History>(response);
        }

        private HttpContent SerializeRequest(Object request)
        {
            return new StringContent(JsonConvert.SerializeObject(request, _serializerSettings), Encoding.UTF8, "application/json");
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(content);
                case HttpStatusCode.Unauthorized: throw new InvalidKTokenException();
                default: throw new InvalidResponseException(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }

        private void ApplyAccesstoken()
        {
            if (!string.IsNullOrEmpty(_accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
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
                _httpClient.Timeout = TimeSpan.FromSeconds(Constants.EXTENDED_TIMEOUT_IN_SECONDS);

                ApplyAccesstoken();
            }

            return Task.FromResult(_httpClient);
        }

        private void CheckInitialized()
        {
            if (string.IsNullOrEmpty(_accessToken) || _credentials == null)
                throw new InvalidOperationException("You must initialize the client first by performing a login first");
        }
    }
}
