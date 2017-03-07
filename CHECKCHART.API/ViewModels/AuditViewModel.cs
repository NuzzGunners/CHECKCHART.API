using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.ViewModels
{
    public class AuditViewModel
    {
        public Guid id { get; set; }
        public string doctor { get; set; }
        public string doctortext { get; set; }
        public int category { get; set; }
    }
}
