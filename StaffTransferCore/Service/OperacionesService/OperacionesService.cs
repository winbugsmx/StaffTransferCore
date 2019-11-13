using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StaffTransferCore.Models.Configuration;
using StaffTransferCore.Models.OperacionesService;
using StaffTransferCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StaffTransferCore.Service.OperacionesService
{
    public class OperacionesService : RestApiClientBase, IOperacionesService
    {
        private readonly OperacionesServiceApiClientOptions _options;
        private readonly ILogger<OperacionesService> _logger;

        public OperacionesService(ILoggerFactory loggerFactory, IOptions<AppSettings> configuration) : base(configuration.Value.OperacionesServiceApiClientOptions)
        {
            _options = configuration.Value.OperacionesServiceApiClientOptions;
            _logger = loggerFactory.CreateLogger<OperacionesService>();
        }

        public async Task<IEnumerable<LlegSalSTAFF>> GetLlegSalSTAFF()
        {
            _logger.LogInformation("Get GetLlegSalSTAFF");

            //Eliminar este cliente
            var cliente = new HttpClient(new OperacionesServiceMockHandler());

            List<LlegSalSTAFF> llegSalSTAFFs = new List<LlegSalSTAFF>();

            //Cambiar 'cliente' por 'this' para apuntar realmente a OperacionesService
            var response = await this.GetAsync($"{_options.BaseAddress}GetLlegSalSTAFF");
            if (response.IsSuccessStatusCode)
            {
                llegSalSTAFFs = (JsonConvert.DeserializeObject<List<LlegSalSTAFF>>(await response.Content.ReadAsStringAsync()));
            }
            return llegSalSTAFFs==null ? new List<LlegSalSTAFF>() : llegSalSTAFFs ;
        }

        public async Task<IEnumerable<DetPaxSTAFF>> GetDetPaxSTAFF()
        {
            _logger.LogInformation("Get GetDetPaxSTAFF");

            //Eliminar este cliente
            var cliente = new HttpClient(new OperacionesServiceMockHandler());

            List<DetPaxSTAFF> detPaxSTAFFs = new List<DetPaxSTAFF>();

            //Cambiar 'cliente' por 'this' para apuntar realmente a OperacionesService
            var response = await this.GetAsync($"{_options.BaseAddress}GetDetPaxSTAFF");
            if (response.IsSuccessStatusCode)
            {
                detPaxSTAFFs = JsonConvert.DeserializeObject<List<DetPaxSTAFF>>(await response.Content.ReadAsStringAsync());
            }
            return detPaxSTAFFs == null ? new List<DetPaxSTAFF>() : detPaxSTAFFs;
        }

        public async Task<IEnumerable<ICamionSTAFF>> GetCamionSTAFF()
        {
            _logger.LogInformation("Get GetCamionSTAFF");

            var cliente = new HttpClient(new OperacionesServiceMockHandler());

            List<ICamionSTAFF> iCamionSTAFFs = new List<ICamionSTAFF>();

            //Cambiar 'cliente' por 'this' para apuntar realmente a OperacionesService
            var response = await cliente.GetAsync($"{_options.BaseAddress}GetCamionSTAFF");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ICamionSTAFF>>(await response.Content.ReadAsStringAsync());
            }
            return iCamionSTAFFs;
        }
    }
}
