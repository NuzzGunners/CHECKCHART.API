using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.ViewModels
{
    public class CheckchartLogViewModel
    {
        public int id { get; set; }
        public string an { get; set; }
        public string receivebyuser { get; set; }
        public string receivebyusername { get; set; }
        public DateTime receivebydatetime { get; set; }
        public string receivebyposition { get; set; }
        public string receivebypositionlocalname { get; set; }
        public string sendtobyuser { get; set; }
        public string sendtobyusername { get; set; }
        public DateTime? sendtodatetime { get; set; }
        public string sendtoposition { get; set; }
        public string sendtopositionlocalname { get; set; }
        public string sendtouser { get; set; }
        public string sendtousername { get; set; }
    }
}
