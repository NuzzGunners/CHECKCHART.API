using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Abstract;
using CHECKCHART.API.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private ICheckchartRepository _items { get; set; }
        public ReportController(ICheckchartRepository iCheckchartRepository)
        {
            _items = iCheckchartRepository;
        }
        
        [HttpGet("receivereport/{receivebyposition}/{fromdate}/{todate}")]
        public IEnumerable<CheckchartLogViewModel> GetReceiveReport(int receivebyposition,string fromdate, string todate)
        {
            var fd = DateTime.Parse(fromdate).AddYears(543);
            var td = DateTime.Parse(todate).AddYears(543);
            var item = from a in _items.AllIncluding(i => i.ReceivebypositionNavigation, i => i.SendtopositionNavigation)
                       where a.Cxlbyuserdatetime == null
                       && a.Receivebyposition == receivebyposition
                       && (a.Receivebydatetime >= fd 
                                && a.Receivebydatetime <= td)
                       select new CheckchartLogViewModel
                       {
                           id = a.Id,
                           an = a.An,
                           receivebyuser = a.Receivebyuser,
                           receivebyposition = a.ReceivebypositionNavigation.Englishname,
                           receivebydatetime = a.Receivebydatetime,
                           sendtobyuser = a.Sendtouser,
                           sendtoposition = a.SendtopositionNavigation == null ? null : a.SendtopositionNavigation.Englishname,
                           sendtodatetime = a.Sendtodatetime                        
                       };

            return item;
        }
    }
}
