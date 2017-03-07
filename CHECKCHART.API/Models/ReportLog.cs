using System;
using System.Collections.Generic;

namespace CHECKCHART.API.Models
{
    public partial class ReportLog
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string Msg { get; set; }
        public string Reportname { get; set; }
        public DateTime? Reportdatetime { get; set; }
        public string Userid { get; set; }
        public string Clientip { get; set; }
    }
}
