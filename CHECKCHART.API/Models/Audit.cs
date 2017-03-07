using System;
using System.Collections.Generic;

namespace CHECKCHART.API.Models
{
    public partial class Audit
    {
        public Guid Id { get; set; }
        public string An { get; set; }
        public string Doctor { get; set; }
        public string Doctormaster { get; set; }
        public string Doctorconsult { get; set; }
        public int Category { get; set; }
        public decimal? Rwbefore { get; set; }
        public decimal? Rwafter { get; set; }
        public string Coder { get; set; }
        public string Ward { get; set; }
        public decimal? Los { get; set; }
        public string Entrybyuser { get; set; }
        public DateTime Entrybydatetime { get; set; }
        public string Updatebyuser { get; set; }
        public DateTime? Updatebydatetime { get; set; }
        public string Cxlbyuser { get; set; }
        public DateTime? Cxlbydatetime { get; set; }
        public string Nurse { get; set; }

        public virtual AuditCategory CategoryNavigation { get; set; }
        public virtual User CxlbyuserNavigation { get; set; }
        public virtual User EntrybyuserNavigation { get; set; }
        public virtual User UpdatebyuserNavigation { get; set; }
    }
}
