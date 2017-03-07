using System;
using System.Collections.Generic;

namespace CHECKCHART.API.Models
{
    public partial class User
    {
        public User()
        {
            AuditCxlbyuserNavigation = new HashSet<Audit>();
            AuditEntrybyuserNavigation = new HashSet<Audit>();
            AuditUpdatebyuserNavigation = new HashSet<Audit>();
            CheckchartCxlbyuserNavigation = new HashSet<Checkchart>();
            CheckchartReceivebyuserNavigation = new HashSet<Checkchart>();
            CheckchartSendtobyuserNavigation = new HashSet<Checkchart>();
            CheckchartSendtouserNavigation = new HashSet<Checkchart>();
            Userposition = new HashSet<Userposition>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int Roleuser { get; set; }

        public virtual ICollection<Audit> AuditCxlbyuserNavigation { get; set; }
        public virtual ICollection<Audit> AuditEntrybyuserNavigation { get; set; }
        public virtual ICollection<Audit> AuditUpdatebyuserNavigation { get; set; }
        public virtual ICollection<Checkchart> CheckchartCxlbyuserNavigation { get; set; }
        public virtual ICollection<Checkchart> CheckchartReceivebyuserNavigation { get; set; }
        public virtual ICollection<Checkchart> CheckchartSendtobyuserNavigation { get; set; }
        public virtual ICollection<Checkchart> CheckchartSendtouserNavigation { get; set; }
        public virtual ICollection<Userposition> Userposition { get; set; }
        public virtual Role RoleuserNavigation { get; set; }
    }
}
