using DotLiquid;
using System;

namespace Project.Infrastructure.Pdf.Dtos
{
    public class ApplicationActUsedPartDto
    {
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public int PartQuantity { get; set; }
        public double PartTotalPrice { get; set; }
    }
}