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
    public class CheckchartlogController : Controller
    {
        private ICheckchartLogRepository _items { get; set; }
        public CheckchartlogController(ICheckchartLogRepository iCheckchartLogRepository)
        {
            _items = iCheckchartLogRepository;
        }

        [HttpGet("{an}", Name = "GetCheckchartLog")]
        public IActionResult GetByAN(string AN)
        {
            var item = _items.GetAll().Where(i => i.An == AN).OrderBy(i => i.Receivebydatetime);
            return new OkObjectResult(item);
        }
    }
}
