using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StaffTransferCore.Service
{
    public class MiExtensionHttpClient : HttpClient
    {
        public MiExtensionHttpClient()
            : base()
        {

        }

        public MiExtensionHttpClient(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }
    }
}
