using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Common;

namespace Dragonfly.Core.Http
{
    public class RestClient : IDisposable
    {
        #region Private Fields
        private readonly HttpClient _client;
        #endregion

        #region public Properties
        public string Url { get; set; }
        #endregion

        #region Constructors
        public RestClient()
        {
            _client = new HttpClient();
        }

        public RestClient(string url)
            : this()
        {
            Url = url;
        }
        #endregion

        #region public Methods
        public async Task<ICommandResult<string>> PostAsync<T>(T message)
        {
            return await PostAsync(message, "");
        }

        public async Task<ICommandResult<string>> PostAsync<T>(T message, string resource)
        {
            Guard.Check(!Url.IsEmptyOrNull(), $"{nameof(Url)} can not be null.");

            SetUpClient();
            var response = await _client.PostAsJsonAsync(resource, message);
            response.EnsureSuccessStatusCode();

            return new CommandResult<string>(true, "", response.Content.ToString());
        }

        public async Task<ICommandResult<T>> PostAsyncResult<T>(T message)
        {
            Guard.Check(!Url.IsEmptyOrNull(), $"{nameof(Url)} can not be null.");

            SetUpClient();
            var response = _client.PostAsJsonAsync("", message).Result;
            var returnValue = await response.Content.ReadAsAsync<T>();
            response.EnsureSuccessStatusCode();
            return new CommandResult<T>(true, "", returnValue);
        }

        #endregion

        #region
        private void SetUpClient()
        {
            _client.BaseAddress = new Uri(Url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            _client.Dispose();
        }
    }
}
