using DotLiquid;
using DotLiquid.NamingConventions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Project.Core.Domain.ExtensionMethods;
using Project.Infrastructure.Configurations.Interfaces.Base;
using Project.Infrastructure.Pdf.Dtos;
using Project.Infrastructure.Pdf.Dtos.Enums;
using Project.Infrastructure.Security.Interfaces;

namespace Project.Infrastructure.Pdf
{
    public abstract class BasePdfGeneratingService<TTemplateModel>
    {
        private readonly IPdfGeneratingConfigurationService _pdfGeneratingConfigurationService;
        private readonly ITokenService _tokenService;

        protected BasePdfGeneratingService(
            IPdfGeneratingConfigurationService pdfGeneratingConfigurationService,
            ITokenService tokenService

        )
        {
            _pdfGeneratingConfigurationService = pdfGeneratingConfigurationService;
            _tokenService = tokenService;
        }

        public async Task<Stream> GeneratePdfAsync(TTemplateModel templateModel,
            SupportedTemplateType supportedTemplateType)
        {
            var pdfTemplatesDirectory = Directory.GetDirectories(Path.Combine(Directory.GetCurrentDirectory(), @"..\"),
                "PdfTemplates", SearchOption.AllDirectories).FirstOrDefault();

            var pdfTemplateFilePath =
                Path.Combine(pdfTemplatesDirectory, supportedTemplateType.ToDescriptionString() + ".html");

            var htmlString = await File.ReadAllTextAsync(pdfTemplateFilePath);
            Template.NamingConvention = new CSharpNamingConvention();
            var bodyTemplate = Template.Parse(htmlString); // Parses and compiles the template source

            var bodyText = bodyTemplate.Render(Hash.FromAnonymousObject(templateModel));

            return await RequestPdfGeneratingServiceAsync(bodyText);
        }

        /// <summary>
        /// Sends HTTP request to 3rd party API, which will generate PDF file and send it back
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private async Task<Stream> RequestPdfGeneratingServiceAsync(string html)
        {
            var client = new HttpClient();
            var postData = new PdfGeneratingServiceHtmlToPdfViewModel
            {
                Html = html
            };
            var serializedRequest = JsonConvert.SerializeObject(postData);

            var requestBody = new StringContent(serializedRequest);
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            requestBody.Headers.Add(_pdfGeneratingConfigurationService.ProjectAuthHeader,
               "Basic " + _tokenService.GenerateToken(_pdfGeneratingConfigurationService.ServiceSecret));
            client.BaseAddress = new Uri(_pdfGeneratingConfigurationService.ServiceUrl);
            client.Timeout = TimeSpan.FromMinutes(10);
            var response = await client.PostAsync(_pdfGeneratingConfigurationService.HtmlToPdfRoute, requestBody);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStreamAsync();
        }
    }
}