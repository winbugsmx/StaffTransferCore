using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StaffTransferCore.Common;
using StaffTransferCore.Models.Configuration;
using StaffTransferCore.Models.OperacionesService;
using StaffTransferCore.Models.Valhalla;
using StaffTransferCore.Models.Valhalla.Entities;
using StaffTransferCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StaffTransferCore.Service.Valhalla
{
    public class ValhallaService : RestApiClientBase, IValhallaService
    {
        private readonly ValhallaApiClientOptions _options;
        private readonly AgencyData _agencyData;
        private readonly ILogger<ValhallaService> _logger;

        public ValhallaService(ILoggerFactory loggerfactory, IOptions<AppSettings> configuration) : base(configuration.Value.ValhallaApiClientOptions)
        {
            _options = configuration.Value.ValhallaApiClientOptions;
            _logger = loggerfactory.CreateLogger<ValhallaService>();
            _agencyData = configuration.Value.Agencydata;
        }

        public async Task<AddServiceResponse> SendBooking(List<Booking> booking)
        {
            AddServiceResponse serializedResponse = null;
            var requestBody = JsonConvert.SerializeObject(new { credential = _agencyData, bookings = booking }, Formatting.Indented);
            try
            {
                var requestContent = new StringContent(requestBody, UnicodeEncoding.UTF8, "application/json");
                var response = await this.PostAsync(_options.BaseAddress, requestContent);
                serializedResponse = JsonConvert.DeserializeObject<AddServiceResponse>(await response.Content.ReadAsStringAsync());
            }catch (Exception ex)
            {
                _logger.LogError($"Error servicio Valhalla" + ex.Message);
            }
            return serializedResponse;
        }
    }
}
