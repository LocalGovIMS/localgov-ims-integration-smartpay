using System;
using System.Diagnostics.CodeAnalysis;

namespace Application.Models
{
    [ExcludeFromCodeCoverage]
    public class Notification
    {
        public string PaymentMethod { get; set; }
        public string Operations { get; set; }
        public bool Success { get; set; }
        public string EventCode { get; set; }
        public string Live { get; set; }
        public string PspReference { get; set; }
        public string OriginalReference { get; set; }
        public string MerchantReference { get; set; }
        public DateTime EventDate { get; set; }
        public string Reason { get; set; }
    }
}