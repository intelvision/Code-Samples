using System.IO;
using System.Threading.Tasks;
using Project.Infrastructure.Pdf.Dtos;

namespace Project.Infrastructure.Pdf.Interfaces.Base
{
    public interface IApplicationActPdfGeneratingService
    {
        Task<Stream> GenerateApplicationActPdf(ApplicationActPdfTemplateDto applicationActPdfTemplateDto);
    }
}