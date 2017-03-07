using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.Models
{
    public class V_CheckchartLog
    {
        [Key]
        public int Id { get; set; }
        public string An { get; set; }
        public string Receivebyuser { get; set; }
        public string Receivebyusername { get; set; }
        public DateTime Receivebydatetime { get; set; }
        public string Receivebyposition { get; set; }
        public string Receivebypositionlocalname { get; set; }
        public string Sendtobyuser { get; set; }
        public string Sendtobyusername { get; set; }
        public DateTime? Sendtodatetime { get; set; }
        public string Sendtoposition { get; set; }
        public string Sendtopositionlocalname { get; set; }
        public string Sendtouser { get; set; }
        public string Sendtousername { get; set; }

    }
}
