using Microsoft.Extensions.DependencyInjection;
using Project.Infrastructure.Pdf.Base;
using Project.Infrastructure.Pdf.Interfaces.Base;

namespace Project.Infrastructure.Pdf.IoC
{
    public static class PdfServicesContainer
    {
        public static void AddPdfGeneratingServices(this IServiceCollection services)
        {
            services.AddTransient<IApplicationActPdfGeneratingService, ApplicationActPdfGeneratingService>();
        }
    }
}