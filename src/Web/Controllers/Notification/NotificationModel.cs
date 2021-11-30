using System;

namespace Web.Controllers
{
    public class NotificationModel
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