using DotLiquid;
using System;

namespace Project.Infrastructure.Pdf.Dtos
{
    public class ApplicationActUsedPartDtoDrop : Drop
    {
        private readonly ApplicationActUsedPartDto applicationActUsedPartDto;

        public string PartName => applicationActUsedPartDto.PartName;
        public string PartNumber => applicationActUsedPartDto.PartNumber;
        public int PartQuantity => applicationActUsedPartDto.PartQuantity;
        public double PartTotalPrice => applicationActUsedPartDto.PartTotalPrice;

        public ApplicationActUsedPartDtoDrop(ApplicationActUsedPartDto applicationActUsedPartDto)
        {
            this.applicationActUsedPartDto = applicationActUsedPartDto;
        }
    }
}