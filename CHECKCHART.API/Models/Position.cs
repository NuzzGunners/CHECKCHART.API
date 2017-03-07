using System;
using System.Collections.Generic;

namespace CHECKCHART.API.Models
{
    public partial class Position
    {
        public Position()
        {
            CheckchartReceivebypositionNavigation = new HashSet<Checkchart>();
            CheckchartSendtopositionNavigation = new HashSet<Checkchart>();
            Userposition = new HashSet<Userposition>();
        }

        public int Id { get; set; }
        public string Englishname { get; set; }
        public string Localname { get; set; }
        public int? PositionOrder { get; set; }

        public virtual ICollection<Checkchart> CheckchartReceivebypositionNavigation { get; set; }
        public virtual ICollection<Checkchart> CheckchartSendtopositionNavigation { get; set; }
        public virtual ICollection<Userposition> Userposition { get; set; }
    }
}
