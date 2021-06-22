using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Project.Infrastructure.Pdf.Dtos
{
    public class ApplicationActPdfTemplateDto
    {
        public string OneCApplicationNumber { get; set; }
        public string ApplicationActDate { get; set; }
        public string WorkerName { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientAddress { get; set; }
        public string EquipmentType { get; set; }
        public string EquipmentSerialNumber { get; set; }

        public bool IsStartUsageWorkTypeDone { get; set; }
        public bool IsWarrantyWorkTypeDone { get; set; }
        public bool IsNonWarrantyWorkTypeDone { get; set; }
        public bool IsContractWorkTypeDone { get; set; }
        public bool IsTechReviewWorkTypeDone { get; set; }

        public bool IsGasLeakDetected { get; set; }

        public string ApplicationReason { get; set; }
        public string PerformedWorks { get; set; }

        public string ArrivingTime { get; set; }
        public string DepartureTime { get; set; }
        public string ApplicationSealNumber { get; set; }

        public IEnumerable<ApplicationActUsedPartDtoDrop> UsedParts { get; set; }
        public double PartsTotalPrice { get; set; }
        public string Claim { get; set; }
        public bool IsGeneralClaim { get; set; }
        public bool IsNonInTimeClaim { get; set; }
        public bool IsBadWorkClaim { get; set; }

        public bool GeneralStartAllowed { get; set; }
        public bool IsChimneyClearanceChecked { get; set; }

        public bool IsEverythingReviewed { get; set; }

    }
}