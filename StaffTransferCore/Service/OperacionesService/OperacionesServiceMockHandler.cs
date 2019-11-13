using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StaffTransferCore.Models.OperacionesService;
using StaffTransferCore.Service.Interfaces;

namespace StaffTransferCore.Service
{
    public class OperacionesServiceMockHandler : HttpMessageHandler
    {
        private readonly string basepath = Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(Environment.CurrentDirectory)).ToString()).ToString();

        //Implementación de respuestas Mock que simulan los objetos que devolverá OperacionesService
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.ToString().Contains("GetLlegSalSTAFF"))
            {
                
                var jsonText = File.ReadAllText($@"{basepath}\Service\JSONTests\LlegSalSTAFFMock.json");
                var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(jsonText)
                };

                string contenido = jsonText;

                return await Task.FromResult(responseMessage);
            }

            if (request.RequestUri.ToString().Contains("GetDetPaxSTAFF"))
            {
                var jsonText = File.ReadAllText($@"{basepath}\Service\JSONTests\DetPaxSTAFFMock.json");
                var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(jsonText)
                };

                string contenido = jsonText;

                return await Task.FromResult(responseMessage);
            }

            if (request.RequestUri.ToString().Contains("GetCamionSTAFF"))
            {
                var jsonText = File.ReadAllText($@"{basepath}\Service\JSONTests\CamionSTAFFMock.json");
                var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(jsonText)
                };

                string contenido = jsonText;

                return await Task.FromResult(responseMessage);
            }

            return null;
        }
    }
}
