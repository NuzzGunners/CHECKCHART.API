using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.ViewModels
{
    public class UserPositionViewModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public int? position { get; set; }
        public string positionName { get; set; }
        public string positionLocalName { get; set; }
    }
}
