using System;
using System.Collections.Generic;

namespace CHECKCHART.API.Models
{
    public partial class Reason
    {
        public Reason()
        {
            Checkchart = new HashSet<Checkchart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Checkchart> Checkchart { get; set; }
    }
}
