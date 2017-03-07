using System;
using System.Collections.Generic;

namespace CHECKCHART.API.Models
{
    public partial class Userposition
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int? Position { get; set; }

        public virtual Position PositionNavigation { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
