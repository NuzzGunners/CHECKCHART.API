using System;
using System.Collections.Generic;

namespace CHECKCHART.API.Models
{
    public partial class Checkchart
    {
        public int Id { get; set; }
        public string An { get; set; }
        public string Receivebyuser { get; set; }
        public DateTime Receivebydatetime { get; set; }
        public int Receivebyposition { get; set; }
        public string Sendtouser { get; set; }
        public int? Sendtoposition { get; set; }
        public DateTime? Sendtodatetime { get; set; }
        public string Sendtobyuser { get; set; }
        public string Cxlbyuser { get; set; }
        public DateTime? Cxlbyuserdatetime { get; set; }
        public int? Cxlbyuserreason { get; set; }

        public virtual User CxlbyuserNavigation { get; set; }
        public virtual Reason CxlbyuserreasonNavigation { get; set; }
        public virtual Position ReceivebypositionNavigation { get; set; }
        public virtual User ReceivebyuserNavigation { get; set; }
        public virtual User SendtobyuserNavigation { get; set; }
        public virtual Position SendtopositionNavigation { get; set; }
        public virtual User SendtouserNavigation { get; set; }
    }
}
