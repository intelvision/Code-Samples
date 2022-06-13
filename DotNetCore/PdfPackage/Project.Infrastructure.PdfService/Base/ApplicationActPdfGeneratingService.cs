using System.IO;
using System.Threading.Tasks;
using Project.Infrastructure.Configurations.Interfaces.Base;
using Project.Infrastructure.Pdf.Dtos;
using Project.Infrastructure.Pdf.Dtos.Enums;
using Project.Infrastructure.Pdf.Interfaces.Base;
using Project.Infrastructure.Security.Interfaces;

namespace Project.Infrastructure.Pdf.Base
{
    public class ApplicationActPdfGeneratingService : BasePdfGeneratingService<ApplicationActPdfTemplateDto>,
        IApplicationActPdfGeneratingService
    {
        public ApplicationActPdfGeneratingService(
            IPdfGeneratingConfigurationService pdfGeneratingConfigurationService,
            ITokenService tokenService
            ) : base(pdfGeneratingConfigurationService, tokenService)
        {
        }

        public Task<Stream> GenerateApplicationActPdf(ApplicationActPdfTemplateDto applicationActPdfTemplateDto)
        {
            return GeneratePdfAsync(applicationActPdfTemplateDto, SupportedTemplateType.ApplicationActTemplate);
        }
    }
}