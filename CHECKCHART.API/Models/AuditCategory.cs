using System;
using System.Collections.Generic;

namespace CHECKCHART.API.Models
{
    public partial class AuditCategory
    {
        public AuditCategory()
        {
            Audit = new HashSet<Audit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Audit> Audit { get; set; }
    }
}
