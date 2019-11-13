using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StaffTransferCore.Service
{
    public abstract class RestApiClientBase : MiExtensionHttpClient
    {
        protected HttpClient _client;

        protected RestApiClientBase(RestApiClientOptionsBase options)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(options.BaseAddress ?? "");
        }
    }
}
